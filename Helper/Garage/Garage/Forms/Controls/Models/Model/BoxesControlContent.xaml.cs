using Garage.Models;
using System.Windows.Controls;

namespace Garage.Forms.Controls.Models.Model
{
    public class BoxesControl : BaseModelControl<Box>
    {
        public BoxesControl(Box obj) : base(obj, new BoxesControlContent(obj))
        {
        }

        public override bool IsEdit(Box obj) => obj.ID != 0;
    }


    /// <summary>
    /// Interaction logic for BoxesControlContent.xaml
    /// </summary>
    public partial class BoxesControlContent : UserControl
    {
        public BoxesControlContent(Box obj)
        {
            InitializeComponent();
            this.DataContext = obj;
        }
    }
}
