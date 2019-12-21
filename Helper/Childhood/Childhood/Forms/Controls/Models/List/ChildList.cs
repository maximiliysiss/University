using Childhood.Forms.Controls.Models.Model;
using Childhood.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Childhood.Forms.Controls.Models.List
{
    /// <summary>
    /// Список детей
    /// </summary>
    public class ChildList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Child());

        protected override List<object> Load() => App.Db.Children.Cast<object>().ToList();

        protected override void Open(object obj) => new ChildControl(obj as Child).ShowDialog();
    }

    /// <summary>
    /// Список детей для воспитателя
    /// </summary>
    public class TutorChildList : ChildList
    {
        protected override List<object> Load() => App.Db.Children.Where(x => x.Group != null && x.Group.TutorId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new TutorChildControl(obj as Child).ShowDialog();
    }

    /// <summary>
    /// Список детей для родителя
    /// </summary>
    public class ParentChildList : ChildList
    {
        protected override List<object> Load() => App.Db.Children.Where(x => x.DaddyId == App.user.ID || x.MomId == App.user.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new ParentChildControl(obj as Child).ShowDialog();
    }
}
