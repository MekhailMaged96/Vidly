using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Helpers
{
    public class Min18YearsIfAMember :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
   
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is Required ");
            }
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            if (age > 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Customer should at least 18 years old");
            }
        }
    }
}