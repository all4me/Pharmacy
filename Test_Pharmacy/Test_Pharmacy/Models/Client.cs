using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Test_Pharmacy.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Adress")]
        public string Adress { get; set; }
        [Display(Name = "Phone")]
        public int Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Right Eye")]
        [Range(-10, 10)]
        public int R_Eye { get; set; }
        [Display(Name = "Left Eye")]
        [Range(-10, 10)]
        public int L_Eye { get; set; }
        ICollection<Visit> Visits { get; set; }

        public Client()
        {
            Visits = new List<Visit>();
        }
    }

    public class Visit
    {
        public enum OrderStatus
        {
            Completed, In_Progress, Cancelled
        }
        public int Id { get; set; }
        [Display(Name = "Order Amount")]
        [Range(0, 10.00)]
        public float OrderAmount { get; set; }
        [Display(Name = "Order Status")]
        public OrderStatus Status { get; set; }
        [Display(Name = "Visit Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }


        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }

    public class Context : DbContext
    {
        public Context()
            : base("DbConnect")
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}