using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDemoProject003.Application.Common.Helpers
{
    public class ValidationClass : IValidationClass
    {
        public bool ValidateCustomerEmailFormat(string email)
        {
            return email.IsValidEmail();
        }

        public bool ValidateCustomerMandatoryFields(CustomerDocument customer)
        {
            return customer.Name.IsNotEmpty()
                                               && customer.Email.IsNotEmpty()
                                               && customer.UserName.IsNotEmpty();
        }

        public bool ValidateCustomerThreshold(CustomerDocument customer)
        {
            var eligibleThreshold = 1000; //TODO: Move to config 

            return (customer.MonthlySalary > eligibleThreshold)
                     && (customer.MonthlySalary > customer.MonthlyExpenses)
                     && ((customer.MonthlySalary - customer.MonthlyExpenses) > eligibleThreshold);
        }
    }
}
