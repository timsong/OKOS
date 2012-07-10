
namespace WFS.Framework
{
    public interface IListResponse : IResponse, IPageable
    {
        int Total { get; set; }
    }
}
