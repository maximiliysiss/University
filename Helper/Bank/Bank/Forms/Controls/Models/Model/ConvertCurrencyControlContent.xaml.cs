using Bank.Extensions;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма конвертации
    /// </summary>
    public class ConvertCurrencyControl : BaseModelControl<ConvertCurrency>
    {
        public ConvertCurrencyControl(ConvertCurrency obj) : base(obj, new ConvertCurrencyControlContent(obj))
        {
        }

        /// <summary>
        /// Форма для редактирования
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool IsEdit(ConvertCurrency obj) => obj.Id != 0;

        /// <summary>
        /// Действие перед действием (Добавить / Изменить)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override bool PrevAction(ConvertCurrency obj)
        {
            obj.CurrencyFrom = obj.CurrencyTo = null;
            /// Если USD -> USD
            if (obj.CurrencyFromId == obj.CurrencyToId)
                return false;
            var findCopy = databaseContext.ConvertCurrencies.AsNoTracking().FirstOrDefault(x => (x.CurrencyFromId == obj.CurrencyToId && x.CurrencyToId == obj.CurrencyFromId)
                                                                    || (x.CurrencyToId == obj.CurrencyToId && x.CurrencyFromId == obj.CurrencyFromId));
            // Если уже есть USD -> RUR or RUR -> USD, то просто подменим
            if (findCopy != null)
            {
                obj.Id = findCopy.Id;
                databaseContext.Update(obj);
                databaseContext.SaveChanges();
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Interaction logic for ConvertCurrencyControlContent.xaml
    /// </summary>
    public partial class ConvertCurrencyControlContent : UserControl
    {
        public ConvertCurrencyControlContent(ConvertCurrency obj)
        {
            InitializeComponent();
            var db = App.Db;
            this.From.ItemsSource = db.Currencies.ToList();
            this.To.ItemsSource = db.Currencies.ToList();
            this.DataContext = obj;
        }
    }
}
