using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IExistsQuery
    {
        bool Execute();//DataContext dataContext);
    }
}
