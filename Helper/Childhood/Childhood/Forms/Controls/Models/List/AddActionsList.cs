using Childhood.Forms.Controls.Models.Model;
using Childhood.Models;
using System.Collections.Generic;
using System.Linq;

namespace Childhood.Forms.Controls.Models.List
{
    /// <summary>
    /// Список доп занятий
    /// </summary>
    public class AddActionsList : BaseModelListControl
    {
        /// <summary>
        /// Добавить новый
        /// </summary>
        protected override void AddNew() => Open(new AddActions());

        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        protected override List<object> Load() => App.Db.AddActions.Cast<object>().ToList();

        /// <summary>
        /// Открыть
        /// </summary>
        /// <param name="obj"></param>
        protected override void Open(object obj) => new AddActionControl(obj as AddActions).ShowDialog();
    }

    /// <summary>
    /// Список доп занятий, но неизменяемый
    /// </summary>
    public class AddActionReadOnlyList : AddActionsList
    {
        public AddActionReadOnlyList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void Open(object obj)
        {
        }
    }
}
