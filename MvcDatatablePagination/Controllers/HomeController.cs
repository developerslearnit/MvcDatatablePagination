using MvcDatatablePagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDatatablePagination.Controllers
{
    public class HomeController : Controller
    {
        PersonService personService = new PersonService();

        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult getPeople()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            
            //Global search field
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();

            //Custom column search fields
            var firstName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var middleName = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var lastName = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var email = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
            var phone = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var people = personService.GetPeople(); // Get People IQueryble


            //Start search
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                people = people.Where(x => x.firstName.ToLower().Contains(firstName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(middleName))
            {
                people = people.Where(x => x.middleName.ToLower().Contains(middleName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                people = people.Where(x => x.lastName.ToLower().Contains(lastName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                people = people.Where(x => x.emailAddress.ToLower().Contains(email.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                people = people.Where(x => x.phone.ToLower().Contains(phone.ToLower()));
            }

            if (!string.IsNullOrEmpty(search))
            {
                people = people.Where(x => x.phone.ToLower().Contains(search.ToLower())
                    || x.firstName.ToLower().Contains(search.ToLower())
                    || x.middleName.ToLower().Contains(search.ToLower())
                    || x.lastName.ToLower().Contains(search.ToLower())
                    || x.emailAddress.ToLower().Contains(search.ToLower()));
            }

            recordsTotal = people.Count();

         
            var data = people.OrderBy(x => x.firstName).Skip(skip).Take(pageSize);
            
            return Json(new {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            },JsonRequestBehavior.AllowGet);
        }

   
    }
}