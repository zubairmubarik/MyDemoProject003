using MyDemoProject003.Domain.Common;

namespace MyDemoProject003.Domain.Entities
{
    public class CustomerDocument : AuditableEntity
    {           
        public string  Name { get; set; }                
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Credit { get; set; }
        public decimal  MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }

    }
}
