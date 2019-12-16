using Garage.Forms.Controls.Models.Model;
using Garage.Models;
using Garage.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Forms.Controls.Models.List
{
    /// <summary>
    /// Список боксов
    /// </summary>
    public class BoxesList : BaseModelListControl
    {
        /// <summary>
        /// Добавить
        /// </summary>
        protected override void AddNew() => Open(new Box());
        /// <summary>
        /// Загрзить из БД
        /// </summary>
        /// <returns></returns>
        protected override List<object> Load() => App.Db.Boxes.Include(x => x.Rents).Cast<object>().ToList();
        /// <summary>
        /// Открыть
        /// </summary>
        /// <param name="obj"></param>
        protected override void Open(object obj) => new BoxesControl(obj as Box).ShowDialog();
    }

    /// <summary>
    /// Боксы для пользователя
    /// </summary>
    public class UserBoxesList : BoxesList
    {
        public UserBoxesList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override List<object> Load() => App.Db.Boxes.Include(x => x.Rents).Where(x => x.Rents.All(x => x.EndDate != null)).Cast<object>().ToList();

        protected override void Open(object obj)
        {
            var box = obj as Box;
            var rent = new Rent { Box = box, UserId = App.user.ID };
            new RentControl(rent).ShowDialog();
        }
    }

    /// <summary>
    /// Арендованные боксы пользователя
    /// </summary>
    public class UserOwnBoxesList : UserBoxesList
    {
        /// <summary>
        /// БД
        /// </summary>
        DatabaseContext db = App.Db;

        protected override List<object> Load() => db.Rents.Include(x => x.Box).Where(x => x.UserId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj)
        {
            new RentControl(obj as Rent).ShowDialog();
        }
    }
}
