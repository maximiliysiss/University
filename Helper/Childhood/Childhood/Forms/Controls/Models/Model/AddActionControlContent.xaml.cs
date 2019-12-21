using Childhood.Models;
using System.Windows.Controls;

namespace Childhood.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма доп мероприятия
    /// </summary>
    public class AddActionControl : BaseModelControl<AddActions>
    {
        public AddActionControl(AddActions obj) : base(obj, new AddActionControlContent(obj))
        {
        }

        /// <summary>
        /// Для изменения или новый элемент
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
