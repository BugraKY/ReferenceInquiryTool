using ReferenceInquiryTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opus.Models.DbModels.ReferenceVerifDb
{
    public class ReferenceDefinitions
    {
        public Guid Id { get; set; }
        public Guid VerificationsId { get; set; }
        public Verifications Verifications { get; set; }
        public Guid UserId { get; set; }
    }
}
