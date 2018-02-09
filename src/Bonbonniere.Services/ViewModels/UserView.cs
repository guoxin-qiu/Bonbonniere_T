using Bonbonniere.Core.Enums;

namespace Bonbonniere.Services.ViewModels
{
    public class UserView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; } // TODO: divide to sigle proj
        public string Address { get; set; }
    }
}
