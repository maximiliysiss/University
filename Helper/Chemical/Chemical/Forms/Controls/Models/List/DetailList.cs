using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Chemical.Forms.Controls.Models.List
{
    public class DetailList : BaseModelListControl
    {
        protected override void AddNew() => Open(new Detail());

        protected override List<object> Load() => App.Db.Details.Include(x => x.DetailMaterials).ThenInclude(x => x.RawMaterial).Cast<object>().ToList();

        protected override void Open(object obj) => new DetailControl(obj as Detail).ShowDialog();
    }

    public class DetailReadOnlyList : DetailList
    {
        public DetailReadOnlyList()
        {
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void Open(object obj)
        {
            var content = new DetailControlContent(obj as Detail, new DetailMaterialReadOnlyList(obj as Detail));

            Window wnd = new Window
            {
                Content = content,
                Height = content.Height + 10,
                Width = content.Width + 20
            };
            wnd.ShowDialog();
        }
    }
}
