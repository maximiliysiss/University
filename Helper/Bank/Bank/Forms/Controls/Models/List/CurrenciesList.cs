using Bank.Forms.Controls.Models.Model;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Forms.Controls.Models.List
{
    /// <summary>
    /// Список валют
    /// </summary>
    public class CurrenciesList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Currency());

        protected override List<object> Load() => App.Db.Currencies.Cast<object>().ToList();

        protected override void Open(object obj) => new CurrencyControl(obj as Currency).ShowDialog();
    }
}
