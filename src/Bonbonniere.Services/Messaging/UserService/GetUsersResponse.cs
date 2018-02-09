using Bonbonniere.Services.ViewModels;
using System.Collections.Generic;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class GetUsersResponse : ResponseMessage
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public IEnumerable<UserView> Users { get; set; }
    }
}
