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
    public class BoxesList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Box());

        protected override List<object> Load() => App.Db.Boxes.Include(x => x.Rents).Cast<object>().ToList();

        protected override void Open(object obj) => new BoxesControl(obj as Box).ShowDialog();
    }

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

    public class UserOwnBoxesList : UserBoxesList
    {
        DatabaseContext db = App.Db;

        protected override List<object> Load() => db.Rents.Include(x => x.Box).Where(x => x.UserId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj)
        {
            new RentControl(obj as Rent).ShowDialog();
        }
    }
}
