using Bank.Models;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма для валюты
    /// </summary>
    public class CurrencyControl : BaseModelControl<Currency>
    {
        public CurrencyControl(Currency obj) : base(obj, new CurrencyControlContent(obj))
        {
        }

        public override bool IsEdit(Currency obj) => obj.Id != 0;
    }

    /// <summary>
    /// Interaction logic for CurrencyControlContent.xaml
    /// </summary>
    public partial class CurrencyControlContent : UserControl
    {
        public CurrencyControlContent(Currency obj)
        {
            InitializeComponent();
            this.DataContext = obj;
        }
    }
}
