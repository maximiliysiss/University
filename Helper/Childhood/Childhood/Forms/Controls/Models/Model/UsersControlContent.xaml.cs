using Childhood.Models;
using Childhood.Services;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма пользователя
    /// </summary>
    public class UsersControl : BaseModelControl<User>
    {
        public UsersControl(User obj) : base(obj, new UsersControlContent(obj))
        {
        }

        /// <summary>
        /// Для изменения
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool IsEdit(User obj) => obj.ID != 0;

        /// <summary>
        /// Действие перед внесение в БД
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override bool PrevAction(User obj)
        {
            // Получим пароль из формы
            UsersControlContent content = (UsersControlContent)this.UserControl;
            obj.PasswordHash = content.Password.Password;

            // Если не новый
            if (obj.ID != 0)
            {
                // Получить предыдущий
                var prev = App.Db.Users.Find(obj.ID);
                // Если пароль поменялся, то надо его изменить
                if (prev.PasswordHash != obj.PasswordHash)
                    obj.PasswordHash = CryptService.CreateMD5(obj.PasswordHash);
            }
            else
                // Новый пользователь
                obj.PasswordHash = CryptService.CreateMD5(obj.PasswordHash);
            return base.PrevAction(obj);
        }
    }

    /// <summary>
    /// Interaction logic for UsersControl.xaml
    /// </summary>
    public partial class UsersControlContent : UserControl
    {
        public UsersControlContent(User obj)
        {
            InitializeComponent();
            this.DataContext = obj;
            this.Password.Password = obj.PasswordHash;
        }
    }
}
