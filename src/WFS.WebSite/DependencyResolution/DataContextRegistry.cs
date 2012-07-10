using StructureMap.Configuration.DSL;
using WFS.Domain.Managers;

namespace WFS_WebSite.DependencyResolution
{
    public class DataContextRegistry : Registry
    {
        public DataContextRegistry()
        {
            For<WFS.DataContext.WFSEntities>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use(() => new WFS.DataContext.WFSEntities());

            For<VendorManager>()
                .Use(() => new VendorManager(new WFS.Repository.WFSRepository(new WFS.DataContext.WFSEntities())));
        }
    }
}