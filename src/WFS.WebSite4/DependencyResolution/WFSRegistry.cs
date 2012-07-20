using StructureMap.Configuration.DSL;
using System.Data.Entity;

namespace WFS.WebSite4.DependencyResolution
{
    public class WFSRegistry : Registry
    {
        public WFSRegistry()
        {
            var ent = new WFS.DataContext.WFSEntities();

            For<WFS.Repository.WFSRepository>()
                .HybridHttpOrThreadLocalScoped()
                 .Use(() => new WFS.Repository.WFSRepository((DbContext)ent));
        }
    }
}