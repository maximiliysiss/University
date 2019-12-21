using Childhood.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Childhood.Forms.Controls
{
    /// <summary>
    /// Форма директора
    /// </summary>
    public partial class DirectorControl : UserControl
    {
        public DirectorControl()
        {
            InitializeComponent();
            Users.Content = new UsersList();
            Groups.Content = new GroupList();
            Child.Content = new ChildList();
            Information.Content = new InformationList();
            AddActions.Content = new AddActionsList();
        }
    }
}
