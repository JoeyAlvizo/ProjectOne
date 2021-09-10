using ProjectOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectOne.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            return View();
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
        // GET: Home/register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,UserEmail,UserPassword,UserIsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Home/login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/login
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserId,UserEmail,UserPassword,UserIsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                var target = db.Users.Where(t => t.UserEmail.Contains(user.UserEmail)).ToList();

                if (target.Count == 0)
                    return RedirectToAction("Index");

                User targetUser = (User)target[0];

                if (targetUser.UserPassword == user.UserPassword)
                {
                    //set session to user and return to home
                    Session["UserEmail"] = targetUser.UserEmail;
                    Session["UserId"] = targetUser.UserId;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Home/classlist
        public ActionResult Classlist()
        {
            return View(db.Classes.ToList());
        }

        // GET: Home/classlist
        /*public ActionResult Classlist()
        {
            List<Class> myClasses;
            if(Session["UserEmail"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var me = db.Users.Where(t => t.UserEmail.Contains((string)Session["UserEmail"])).ToList()[0];




                myClassesIds = db.Classes.Where(t => t.)
            }
            return RedirectToAction("Index");
        }*/
    }
}