using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Application.Customers.Commands.CreateCustomer;
using MyDemoProject003.Application.Common.Models;

namespace MyDemoProject003.Application.Common.Behaviours
{
    public class CreateCustomerCommandValidator : AbstractValidator<CustomerDocument>
    {
           

       
        public CreateCustomerCommandValidator()
        {
            

            RuleFor(x => x.Name).NotEmpty().WithMessage("No");
            // RuleFor(x => x.Customer.Email).NotEmpty();
            // RuleFor(x => x.Customer.Email).EmailAddress();
            //RuleFor(x => x.Customer.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            // RuleFor(x => x.Customer.MonthlySalary).NotEmpty();
            // RuleFor(x => x.Customer.MonthlyExpenses).NotEmpty();
            //RuleFor(x => x.Customer.MonthlySalary - x.Customer.MonthlyExpenses).LessThan(1000);//TODO: Put 1000 in constants
        }
    }
}
