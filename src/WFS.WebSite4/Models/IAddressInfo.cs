
namespace WFS.WebSite4.Models
{
    public interface IAddressInfo
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        string PhoneNumber { get; set; }
        string PhoneExt { get; set; }
    }
}