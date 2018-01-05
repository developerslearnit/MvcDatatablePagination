using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDatatablePagination.Models.Viewmodels
{
    public class PersonViewModel
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
    }
}