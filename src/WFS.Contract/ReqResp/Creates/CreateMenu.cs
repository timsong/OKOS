using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class CreateMenuRequest
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateMenuResponse : BaseResponse
    {
        public Menu Menu { get; set; }
    }
}
