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
    /// Список групп
    /// </summary>
    public class GroupList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Group());

        protected override List<object> Load() => App.Db.Groups.Cast<object>().ToList();

        protected override void Open(object obj) => new GroupControl(obj as Group).ShowDialog();
    }
}
