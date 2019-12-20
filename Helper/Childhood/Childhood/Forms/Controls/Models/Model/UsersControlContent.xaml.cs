using Childhood.Models;
using Childhood.Services;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    public class UsersControl : BaseModelControl<User>
    {
        public UsersControl(User obj) : base(obj, new UsersControlContent(obj))
        {
        }

        public override bool IsEdit(User obj) => obj.ID != 0;

        protected override bool PrevAction(User obj)
        {
            UsersControlContent content = (UsersControlContent)this.UserControl;
            obj.PasswordHash = content.Password.Password;

            if (obj.ID != 0)
            {
                var prev = App.Db.Users.Find(obj.ID);
                if (prev.PasswordHash != obj.PasswordHash)
                    obj.PasswordHash = CryptService.CreateMD5(obj.PasswordHash);
            }
            else
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
