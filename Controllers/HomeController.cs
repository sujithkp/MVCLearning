using MVCLearning.Models.ValidationExample;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCLearning.Controllers
{
    public class HomeController : Controller
    {
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


        public ActionResult DefaultValidationEx()
        {
            var model = new IDentityModelDto();

            ViewBag.LanguageList = new String[] { "English", "French", "Dutch", "Espanol" };
            ViewBag.GenderList = new String[] { "Male", "Female" };

            ViewBag.NationalityList = new String[] { "India", "Belgium", "France", "Chinese", "Brazil" };
            ViewBag.CivilStateList = new String[5];
            ViewBag.MarriageContractList = new String[5];
            ViewBag.ProfessionCategoryList = new String[5];
            ViewBag.ProfessionList = new String[5];

            return View(model);
        }


        public ActionResult SubmitRequest(IDentityModelDto model)
        {
            string validationMessage = string.Empty;

            if (!ModelState.IsValid)
            {
                validationMessage = ValidateModel(model.ActiveParty);
            }

            if (validationMessage.Length != 0)
            {
                model.ValidationMessage = validationMessage;

                return View("DefaultValidationEx", model);
            }


            return Content("Saved") ;
        }


        private String ValidateModel(object model)
        {
            int errorCounter = 0;
            var objectToValidate = model;
            ValidationContext ctx = new ValidationContext(objectToValidate, null, null);
            //Validator.ValidateObject(objectToValidate, ctx, true);
            var errorList = new List<ValidationResult>();
            Validator.TryValidateObject(objectToValidate, ctx, errorList, true);

            StringBuilder sb = new StringBuilder();
            if (errorList != null)
            {
                errorCounter += errorList.Count;
                foreach (var item in errorList)
                {
                    sb.AppendLine(item.ErrorMessage);
                }
            }

            return sb.ToString();
        }

    }
}