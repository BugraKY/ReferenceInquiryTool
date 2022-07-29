using System;
using System.Collections.Generic;
using System.Text;

namespace ReferenceInquiryTool.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool Active { get; set; }
    }
}
