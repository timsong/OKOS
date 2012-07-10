using StructureMap;
using WFS.Repository;

namespace WFS_WebSite
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                        scan.AddAllTypesOf<IRepository>();
                                        scan.LookForRegistries();
                                    });
                            //                x.For<IExample>().Use<Example>();
                        });
            return ObjectFactory.Container;
        }
    }
}