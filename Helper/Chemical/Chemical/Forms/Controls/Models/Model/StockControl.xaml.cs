using Chemical.Forms.Controls.Models.List;
using Chemical.Models;
using System.Windows.Controls;

namespace Chemical.Forms.Controls.Models.Model
{
    public class StockControl : BaseModelControl<Stock>
    {
        public StockControl(Stock obj) : base(obj, new StockControlContent(obj))
        {
            Title = "Склад";
        }

        public override bool IsEdit(Stock obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for StockControl.xaml
    /// </summary>
    public partial class StockControlContent : UserControl
    {
        public StockControlContent(Stock stock)
        {
            InitializeComponent();
            this.DataContext = stock;
            if (stock.ID != 0)
                this.MaterialsInStocks.Children.Add(new MaterialInStockList(stock.ID));
        }
    }
}
