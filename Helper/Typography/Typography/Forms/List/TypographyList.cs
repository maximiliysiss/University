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
    public class TypographyList : ListBaseForm<Models.Typography>
    {
        public TypographyList(IDatabaseContext databaseContext, DbSet<Models.Typography> actionList, string name = null) : base(databaseContext, actionList, name)
        {
        }

        protected override bool Search(Models.Typography elem, string val)
        {
            var lowVal = val.Trim().ToLower();
            return elem.TypographyAdress.ToLower().Contains(lowVal)
                || elem.TypographyName.ToLower().Contains(lowVal)
                || elem.TypographyNumber.ToLower().Contains(lowVal);
        }
    }
}
