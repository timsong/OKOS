
namespace WFS.Framework.Responses
{
    public interface IListResponse : IResponse, IPageable
    {
        int Total { get; set; }
    }
}
