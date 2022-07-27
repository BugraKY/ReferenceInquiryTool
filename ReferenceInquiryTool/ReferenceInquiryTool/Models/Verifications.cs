using System;
using System.Collections.Generic;
using System.Text;

namespace ReferenceInquiryTool.Models
{
    public class Verifications
    {
        public Guid Id { get; set; }
        public string ReferenceCode { get; set; }
        public string ReferenceNum { get; set; }
        public bool Active { get; set; }
        public string ActiveSTR { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
