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
    public class PostOfficerList : ListBaseForm<PostOfficer>
    {
        public PostOfficerList(IDatabaseContext databaseContext, DbSet<PostOfficer> actionList, string name = null) : base(databaseContext, actionList, name)
        {
        }

        protected override bool Search(PostOfficer elem, string val)
        {
            var lowVal = val.Trim().ToLower();
            return elem.PostOfficerAdress.ToString().Contains(lowVal)
                || elem.PostOfficerName.ToLower().Contains(lowVal)
                || elem.PostOfficerNumber.ToLower().Contains(lowVal);
        }
    }
}
