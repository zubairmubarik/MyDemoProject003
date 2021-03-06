using MyDemoProject003.Common.Utilities;
using System.Collections.Generic;

namespace MyDemoProject003.Application.Common.Models
{
    public class ResponseListDto<T>
    {
        public IEnumerable<T> Value { get; set; }

        public bool IsError { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public string JsonResponded { get; set; }

        public string Description { get; set; }
    }
}