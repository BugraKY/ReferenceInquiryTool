using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ReferenceInquiryTool.Models
{
    public class Verifications
    {
        public string Id { get; set; }
        public string CompanyReference { get; set; }
        public string Reference { get; set; }
        public bool Active { get; set; }
        public string ActiveSTR { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsException { get; set; }
        public WebException Exception { get; set; }
    }
}
