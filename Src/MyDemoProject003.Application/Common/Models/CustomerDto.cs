using AutoMapper;
using MyDemoProject003.Application.Common.Mappings;
using MyDemoProject003.Domain.Entities;
using Newtonsoft.Json;
using System;

namespace MyDemoProject003.Application.Common.Models
{
    public class CustomerDto : IMapFrom<CustomerDocument>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }             
        public string JsonResponded { get; set; }
        public decimal Credit { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public void Mapping(Profile profile)
        {            
            profile.CreateMap<CustomerDocument, CustomerDto>().ForMember(x=>x.JsonResponded,opt=>opt.MapFrom(c=>JsonConvert.SerializeObject(c)));
        }

    }
}
