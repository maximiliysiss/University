using Chemical.Forms.Controls.Models.Model;
using Chemical.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Chemical.Forms.Controls.Models.List
{
    public class DetailMaterialList : BaseModelListControl
    {
        private readonly Detail detail;

        public DetailMaterialList(Detail detail)
        {
            this.detail = detail;
            if (detail.ID <= 0)
                Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void AddNew() => Open(new DetailMaterial { DetailId = detail.ID, Detail = detail });

        protected override List<object> Load() => App.Db.DetailMaterials.Include(x => x.Detail).Include(x => x.RawMaterial).Where(x => x.DetailId == detail.ID).Cast<object>().ToList();

        protected override void Open(object obj) => new DetailMaterialControl(obj as DetailMaterial).ShowDialog();
    }

    public class DetailMaterialReadOnlyList : DetailMaterialList
    {
        public DetailMaterialReadOnlyList(Detail detail) : base(detail)
        {
            Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void Open(object obj)
        {
        }
    }
}
