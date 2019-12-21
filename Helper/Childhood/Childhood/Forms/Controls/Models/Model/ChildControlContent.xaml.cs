using Childhood.Models;
using System.Linq;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма ребенка
    /// </summary>
    public class ChildControl : BaseModelControl<Child>
    {
        public ChildControl(Child obj) : base(obj, new ChildControlContent(obj))
        {
        }

        public override bool IsEdit(Child obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for ChildControlContent.xaml
    /// </summary>
    public partial class ChildControlContent : UserControl
    {
        public ChildControlContent(Child obj)
        {
            InitializeComponent();

            var db = App.Db;
            // Получим список групп и родителей
            Groups.ItemsSource = db.Groups.ToList();
            Mom.ItemsSource = Daddy.ItemsSource = db.Users.Where(x => x.UserType == UserType.Parent).ToList();

            this.DataContext = obj;
        }
    }
}
