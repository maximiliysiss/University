using Chemical.Forms.Controls.Models.List;
using System.Windows.Controls;

namespace Chemical.Forms.Controls
{
    /// <summary>
    /// Форма для хранения
    /// </summary>
    public partial class StorageControl : UserControl
    {
        public StorageControl()
        {
            InitializeComponent();
            Storages.Content = new StockList();
        }
    }
}
