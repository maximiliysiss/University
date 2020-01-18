using Bank.Models;
using Bank.Services;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма пользователя
    /// </summary>
    public class UsersControl : BaseModelControl<User>
    {
        public UsersControl(User obj) : base(obj, new UsersControlContent(obj))
        {
        }

        public UsersControl(User obj, UserControl content) : base(obj, content)
        {
        }

        public override bool IsEdit(User obj) => obj.Id != 0;

        /// <summary>
        /// Пред действие
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override bool PrevAction(User obj)
        {
            // Получим пароль
            var newPassword = (this.UserControl as UsersControlContent).Password.Password;
            // Если это изменение пароля
            if (obj.Id != 0)
            {
                var prevObj = App.Db.Users.Find(obj.Id);
                if (prevObj.PasswordHash != obj.PasswordHash)
                    obj.PasswordHash = CryptService.CreateMD5(newPassword);
            }
            else
                obj.PasswordHash = CryptService.CreateMD5(newPassword);
            return true;
        }
    }

    /// <summary>
    /// Interaction logic for UsersControlContent.xaml
    /// </summary>
    public partial class UsersControlContent : UserControl
    {
        public UsersControlContent(User obj)
        {
            InitializeComponent();
            this.Password.Password = obj.PasswordHash;
            this.DataContext = obj;
        }
    }
}
