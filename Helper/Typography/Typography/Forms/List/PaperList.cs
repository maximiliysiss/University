using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Typography.Models;
using Typography.Services;

namespace Typography.Forms.List
{
    public class PaperList : ListBaseForm<Paper>
    {
        public PaperList(IDatabaseContext databaseContext, DbSet<Paper> actionList, string name = null) : base(databaseContext, actionList, name)
        {
        }

        protected override bool Search(Paper elem, string val)
        {
            var lowVal = val.Trim().ToLower();
            return elem.EditionCode.ToLower().Contains(lowVal)
                || elem.EditorFIO.ToLower().Contains(lowVal)
                || elem.PaperName.ToLower().Contains(lowVal)
                || elem.PaperPrice.ToString().Contains(lowVal)
                || elem.PaperQuantity.ToString().Contains(lowVal);
        }
    }
}
