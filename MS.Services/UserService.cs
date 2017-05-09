namespace MS.Services
{
    using Data.UnitOfWork;
    using System.Linq;
    using System.Web.Mvc;

    public class UserService
    {
        public UserService(IManagementSystemData data)
        {
            this.Data = data;
        }

        private IManagementSystemData Data { get; set; }

        public SelectList GetUsersUsernames()
        {
            var users = this.Data.Users.All()
                .Select(x =>
                        new SelectListItem
                        {
                            Value = x.Id,
                            Text = x.UserName
                        });

            return new SelectList(users, "Value", "Text");
        }
    }
}
