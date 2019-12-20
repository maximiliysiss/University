using Childhood.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Childhood.Forms.Controls
{
    /// <summary>
    /// Interaction logic for TutorControl.xaml
    /// </summary>
    public partial class TutorControl : UserControl
    {
        public TutorControl()
        {
            InitializeComponent();
            Children.Content = new TutorChildList();
        }
    }
}
