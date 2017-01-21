using Newtonsoft.Json;
using SendMail.Core;
using SendMail.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SendMail.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMail()
        {
            if (Request.Files.Count == 0)
            {
                RedirectToAction("Index");
            }

            HttpPostedFileBase file = Request.Files[0];
            var emails = new ExcelReader().Read(file.FileName);

            ViewBag.Emails = JsonConvert.SerializeObject(emails);

            return View();
        }

        public FileResult ModelPlan()
        {
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        
        public JsonResult SendMail(EmailData emailData)
        {
            return Json(emailData);
        }        
    }
}