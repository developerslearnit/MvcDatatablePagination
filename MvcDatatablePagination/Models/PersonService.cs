using MvcDatatablePagination.Models.DataModel;
using MvcDatatablePagination.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDatatablePagination.Models
{
    public class PersonService
    {
        AppEntities context;
        public PersonService()
        {
            context = new AppEntities();
        }


        public IQueryable<PersonViewModel> GetPeople()
        {
            return from p in context.People join em in context.EmailAddresses
                   on p.BusinessEntityID equals em.BusinessEntityID
                   join ph in context.PersonPhones on p.BusinessEntityID
                   equals ph.BusinessEntityID
                   select new PersonViewModel
                   {
                       firstName = p.FirstName,
                       lastName =p.LastName,
                       emailAddress = em.EmailAddress1,
                       middleName =p.MiddleName,
                       phone =ph.PhoneNumber
                   };
        }


    }
}