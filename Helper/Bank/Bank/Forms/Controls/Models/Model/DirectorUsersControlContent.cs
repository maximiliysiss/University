using Bank.Models;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма пользователя для директора (заблокирована возможность менять роль)
    /// </summary>
    public class DirectorUsersControl : UsersControl
    {
        public DirectorUsersControl(User obj) : base(obj, new DirectorUsersControlContent(obj))
        {
        }
    }

    public class DirectorUsersControlContent : UsersControlContent
    {
        public DirectorUsersControlContent(User obj) : base(obj)
        {
            role.IsEnabled = false;
        }
    }
}
