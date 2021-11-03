using NUnit.Framework;
using MyDemoProject003.Application.Common.Helpers;
using MyDemoProject003.Domain.Entities;

namespace MyDemoProject003.ApplicationUnitTests.Common.Helpers
{
    using static Testing;
    public class ValidationClassTest : TestBase
    {
        [Test]
        public void ValidateEmail_EmailAddress_True()
        {
            var validationClass = GetValidationClass();
            var email = "Test123@gmail.com";        
            var result = validationClass.ValidateCustomerEmailFormat(email);

            var expectedResult =true;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void InValidateEmail_EmailAddress_False()
        {
            var validationClass = GetValidationClass();
            var email = "Test123gmail.com";
            var result = validationClass.ValidateCustomerEmailFormat(email);

            var expectedResult = false;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void InValidateEmail_ValidThrashhold_True()
        {
            var validationClass = GetValidationClass();
            var customer = new CustomerDocument()
            {
                Name = "Zubair112",
                Email = "Zubair112@gmail.com",
                 MonthlyExpenses = 5000,
                  MonthlySalary = 10000,
                  
            };
            var result = validationClass.ValidateCustomerThreshold(customer);
            var expectedResult = true;
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void InValidateEmail_ValidThrashhold_False()
        {
            var validationClass = GetValidationClass();
            var customer = new CustomerDocument()
            {
                Name = "Zubair112",
                Email = "Zubair112@gmail.com",
                MonthlyExpenses = 9500,
                MonthlySalary = 10000,

            };
            var result = validationClass.ValidateCustomerThreshold(customer);
            var expectedResult = false;
            Assert.AreEqual(expectedResult, result);
        }
    }
}
