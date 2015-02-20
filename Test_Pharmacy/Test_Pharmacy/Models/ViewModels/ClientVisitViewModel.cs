using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Test_Pharmacy.Models.ViewModels
{
    public class ClientVisitViewModel
    {
        public Client client { get; set; }
        public IList<Visit> Visits { get; set; }

    }
}