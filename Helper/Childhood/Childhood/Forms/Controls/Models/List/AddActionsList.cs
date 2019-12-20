﻿using Childhood.Forms.Controls.Models.Model;
using Childhood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Childhood.Forms.Controls.Models.List
{
    public class AddActionsList : BaseModelListControl
    {
        protected override void AddNew() => Open(new AddActions());

        protected override List<object> Load() => App.Db.AddActions.Cast<object>().ToList();

        protected override void Open(object obj) => new AddActionControl(obj as AddActions).ShowDialog();
    }
}
