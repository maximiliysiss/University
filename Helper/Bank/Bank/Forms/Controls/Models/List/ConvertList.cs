using Bank.Forms.Controls.Models.Model;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Forms.Controls.Models.List
{
    /// <summary>
    /// Список для конвертаций
    /// </summary>
    public class ConvertList : BaseModelListControl
    {
        /// <summary>
        /// Добавить новую
        /// </summary>
        protected override void AddNew() => Open(new ConvertCurrency());

        /// <summary>
        /// Загрузить
        /// </summary>
        /// <returns></returns>
        protected override List<object> Load() => App.Db.ConvertCurrencies.Cast<object>().ToList();

        /// <summary>
        /// Открыть
        /// </summary>
        /// <param name="obj"></param>
        protected override void Open(object obj) => new ConvertCurrencyControl(obj as ConvertCurrency).ShowDialog();
    }
}
