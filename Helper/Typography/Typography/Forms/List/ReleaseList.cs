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
    public class ReleaseList : ListBaseForm<Release>
    {
        public ReleaseList(IDatabaseContext databaseContext, DbSet<Release> actionList, string name = null) : base(databaseContext, actionList, name)
        {
        }

        protected override bool Search(Release elem, string val)
        {
            var lowVal = val.Trim().ToLower();
            return elem.Paper.EditionCode.ToLower().Contains(lowVal)
                || elem.Paper.EditorFIO.ToLower().Contains(lowVal)
                || elem.Paper.PaperName.ToLower().Contains(lowVal)
                || elem.Paper.PaperPrice.ToString().Contains(lowVal)
                || elem.Paper.PaperQuantity.ToString().Contains(lowVal)
                || elem.Typography.TypographyAdress.ToLower().Contains(lowVal)
                || elem.Typography.TypographyName.ToLower().Contains(lowVal)
                || elem.Typography.TypographyNumber.ToLower().Contains(lowVal);
        }
    }
}
