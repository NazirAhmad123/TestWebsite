using Umbraco.Web.Mvc;
using System.Web.Mvc;
using WebsiteDemo.Models;
using System.Net.Mail;
using System.Net;


namespace WebsiteDemo.Controllers
{
    // An MVC controller that interacts with the front-end rendering of an UmbracoPage.
    public class ContactSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";
        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            MailAddress to = new MailAddress(model.EmailAddress);
            MailAddress from = new MailAddress(model.EmailAddress);

            MailMessage message = new MailMessage(to, from)
            {
                Subject = string.Format("Enquiry from {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress),
                Body = model.Message
            };


            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(
                "nazirnoori111@gmail.com", "Shakokoima@123"),
                EnableSsl = true
            };

            smtp.Send(message);

            //using(var mail = new MailMessage(from, to))
            //{
                
            //    var Emaiil = new Email()
            //    {

            //    }
            //}
        }
    }
}