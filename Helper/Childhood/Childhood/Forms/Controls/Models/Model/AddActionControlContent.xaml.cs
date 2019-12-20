using Childhood.Models;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    public class AddActionControl : BaseModelControl<AddActions>
    {
        public AddActionControl(AddActions obj) : base(obj, new AddActionControlContent(obj))
        {
        }

        public override bool IsEdit(AddActions obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for AddActionControlContent.xaml
    /// </summary>
    public partial class AddActionControlContent : UserControl
    {
        public AddActionControlContent(AddActions obj)
        {
            InitializeComponent();
            this.DataContext = obj;
        }
    }
}
