using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalCalendar.Controllers
{
    public class HomeController : Controller
    {
        CalendarEntities dc = new CalendarEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LoginUser(string userName, string Password)
        {
            var status = false; var Userid = 0;

            using (CalendarEntities dc = new CalendarEntities())
            {
                var v = dc.UserDetails.Where(x => x.UserName == userName && x.Password == Password).FirstOrDefault();
                if (v != null)
                {
                    Userid = v.UserId;
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status, Userid = Userid } };
        }
        public JsonResult GetCustomer()
        {
            var getcust = dc.UserDetails.ToList();
            return new JsonResult { Data = getcust, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTrainer()
        {
            var getcust = dc.Trainers.ToList();
            return new JsonResult { Data = getcust, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTrinings(int Userid)
        {
            //var events = dc.Courses.ToList();
            if (Userid == 1)
            {
                var events = (from obj in dc.Courses
                              join obj2 in dc.UserDetails on obj.UserId equals obj2.UserId
                              //where obj.StartDate >=DateTime.Now
                              select new
                              {
                                  Id = obj.Id,
                                  CourseName = obj.CourseName,
                                  PreRequisite = obj.PreRequisite,
                                  Duration = obj.Duration,
                                  StartDate = obj.StartDate,
                                  EndDate = obj.EndDate,
                                  Timings = obj.Timings,
                                  LinkToJoin = obj.LinkToJoin,
                                  UserId = obj.UserId,
                                  UserName = obj2.UserName
                              }).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var events = (from obj in dc.Courses
                              join obj2 in dc.UserDetails on obj.UserId equals obj2.UserId
                              where //obj.StartDate >=DateTime.Now &&
                              obj.UserId == Userid
                              select new
                              {
                                  Id = obj.Id,
                                  CourseName = obj.CourseName,
                                  PreRequisite = obj.PreRequisite,
                                  Duration = obj.Duration,
                                  StartDate = obj.StartDate,
                                  EndDate = obj.EndDate,
                                  Timings = obj.Timings,
                                  LinkToJoin = obj.LinkToJoin,
                                  UserId = obj.UserId,
                                  UserName = obj2.UserName
                              }).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        [HttpPost]
        public JsonResult SaveEvent(Cours e, string Cust)
        {
            var status = false;


            var UserExist = dc.UserDetails.Where(x => x.UserId == e.UserId).FirstOrDefault();
            if (UserExist != null)
            {

            }
            else
            {
                var UserExist2 = (from obj in dc.UserDetails
                                  where obj.UserName == Cust
                                  select obj).ToList();
                if (UserExist2.Count > 0)
                {
                    e.UserId = UserExist2[0].UserId;
                }
                else
                {
                    UserDetail users = new UserDetail();
                    users.UserName = Cust;
                    users.Password = Cust;
                    dc.UserDetails.Add(users);
                    dc.SaveChanges();
                    e.UserId = users.UserId;
                }
            }
            if (e.Id > 0)
            {
                var getdata = dc.Courses.Where(x => x.Id == e.Id).FirstOrDefault();
                if (getdata != null)
                {
                    getdata.CourseName = e.CourseName;
                    getdata.PreRequisite = e.PreRequisite;
                    getdata.StartDate = e.StartDate;
                    getdata.EndDate = e.EndDate;
                    getdata.Timings = e.Timings;
                    getdata.Duration = e.Duration;
                    getdata.LinkToJoin = e.LinkToJoin;
                    getdata.UserId = e.UserId;
                }
            }
            else
            {
                dc.Courses.Add(e);
            }
            dc.SaveChanges();
            status = true;


            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int CourseId)
        {
            var status = false;

            var v = dc.Courses.Where(x => x.Id == CourseId).FirstOrDefault();
            if (v != null)
            {
                dc.Courses.Remove(v);
                dc.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}