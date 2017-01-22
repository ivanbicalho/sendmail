using Newtonsoft.Json;
using SendMail.Core;
using SendMail.Core.Business;
using SendMail.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SendMail.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("mail/send")]
        public ActionResult SendMail()
        {
            if (Request.Files.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var emails = ReadEmailsFromFile();

            ViewBag.Emails = JsonConvert.SerializeObject(emails);
            ViewBag.CountEmails = emails.Count();

            return View();
        }

        [HttpGet]
        [Route("plan")]
        public FileResult ModelPlan()
        {
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("mail/send/execute")]
        public JsonResult SendMail(EmailData data)
        {
            return Json(new { success = true });
        }

        private IEnumerable<EmailEntity> ReadEmailsFromFile()
        {
            var file = Request.Files[0];
            var path = Path.Combine(Server.MapPath("~/PlanUploaded/"), Guid.NewGuid().ToString() + ".xls");
            file.SaveAs(path);

            var emails = new ExcelReader().Read(path);
            System.IO.File.Delete(path);

            return emails;
        }
    }
}