using Childhood.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Childhood.Forms.Controls
{
    /// <summary>
    /// Interaction logic for ParentControl.xaml
    /// </summary>
    public partial class ParentControl : UserControl
    {
        public ParentControl()
        {
            InitializeComponent();
            Children.Content = new ParentChildList();
            AddActions.Content = new AddActionReadOnlyList();
            Information.Content = new InformationReadOnlyList();
        }
    }
}
