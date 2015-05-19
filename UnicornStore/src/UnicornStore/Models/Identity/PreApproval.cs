using System;

namespace UnicornStore.AspNet.Models.Identity
{
    public class PreApproval
    {
        public string UserEmail { get; set; }
        public string Role { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
    }
}