using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace WFS.Repository
{
    public class WFSRepository : EFRepository
    {
        public WFSRepository(DbContext context)
            : base(context)
        {

        }
    }
}
