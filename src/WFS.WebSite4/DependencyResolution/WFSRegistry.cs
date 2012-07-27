using StructureMap.Configuration.DSL;
using System.Data.Entity;

namespace WFS.WebSite4.DependencyResolution
{
    public class WFSRegistry : Registry
    {
        public WFSRegistry()
        {
            For<WFS.Repository.WFSRepository>()
                .HybridHttpOrThreadLocalScoped()
				 .Use(() => new WFS.Repository.WFSRepository((DbContext)new WFS.DataContext.WFSEntities()));
        }
    }
}