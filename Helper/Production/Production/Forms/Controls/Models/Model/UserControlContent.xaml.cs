using Production.Models;
using Production.Services;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Production.Forms.Controls.Models.Model
{
    public class UsersControl : BaseModelControl<User>
    {
        public UsersControl(User obj) : base(obj, new UserControlContent(obj as User))
        {
        }

        public override bool IsEdit(User obj) => obj.ID != 0;

        protected override bool PrevAction(User obj)
        {
            base.PrevAction(obj);
            var userControl = this.InnerContent.Children[0] as UserControl;
            ((User)userControl.DataContext).PasswordHash = CryptService.CreateMD5(((User)userControl.DataContext).PasswordHash);
            return true;
        }
    }


    /// <summary>
    /// Interaction logic for UserControlContent.xaml
    /// </summary>
    public partial class UserControlContent : UserControl
    {
        public UserControlContent(User user)
        {
            InitializeComponent();
            this.DataContext = user;
        }
    }
}
