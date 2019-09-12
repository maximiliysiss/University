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
    public class DistributionList : ListBaseForm<Distribution>
    {
        public DistributionList(IDatabaseContext databaseContext, DbSet<Distribution> actionList, string name = null) : base(databaseContext, actionList, name)
        {
        }

        protected override bool Search(Distribution elem, string val)
        {
            var lowVal = val.Trim().ToLower();
            return elem.Paper.EditionCode.ToLower().Contains(lowVal)
                || elem.Paper.EditorFIO.ToLower().Contains(lowVal)
                || elem.Paper.PaperName.ToLower().Contains(lowVal)
                || elem.Paper.PaperPrice.ToString().Contains(lowVal)
                || elem.Paper.PaperQuantity.ToString().Contains(lowVal)
                || elem.PostOfficer.PostOfficerAdress.ToString().Contains(lowVal)
                || elem.PostOfficer.PostOfficerName.ToLower().Contains(lowVal)
                || elem.PostOfficer.PostOfficerNumber.ToLower().Contains(lowVal);
        }
    }
}
