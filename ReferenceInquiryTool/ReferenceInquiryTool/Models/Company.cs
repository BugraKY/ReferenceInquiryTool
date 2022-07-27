using System;
using System.Collections.Generic;
using System.Text;

namespace ReferenceInquiryTool.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
