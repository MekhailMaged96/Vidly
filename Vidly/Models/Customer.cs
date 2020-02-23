using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Helpers;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer`s Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:MM / dd / yyyy}")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool  IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name ="Membership Type")]
        public byte MembershipTypeId { get; set; }


 
    }
}