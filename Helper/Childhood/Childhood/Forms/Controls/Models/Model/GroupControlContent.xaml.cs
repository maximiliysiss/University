using Childhood.Models;
using System.Linq;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    public class GroupControl : BaseModelControl<Group>
    {
        public GroupControl(Group obj) : base(obj, new GroupControlContent(obj))
        {
        }

        public override bool IsEdit(Group obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for GroupControlContent.xaml
    /// </summary>
    public partial class GroupControlContent : UserControl
    {
        public GroupControlContent(Group obj)
        {
            InitializeComponent();
            this.Tutor.ItemsSource = App.Db.Users.Where(x => x.UserType == UserType.Tutor).ToList(); ;
            this.DataContext = obj;
        }
    }
}
