using MyDemoProject003.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDemoProject003.Application.Common.Interfaces
{
    public interface IValidationClass
    {
        bool ValidateCustomerMandatoryFields(CustomerDocument customer);
        bool ValidateCustomerEmailFormat(string email);
        bool ValidateCustomerThreshold(CustomerDocument customer);
    }
}
