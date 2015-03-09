using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;

namespace UnicornStore.AspNet.Models.Identity
{
    public class IndexViewModel
    {
        public IList<UserLoginInfo> Logins { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}