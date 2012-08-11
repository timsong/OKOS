
namespace WFS.Contract
{
    public class CustomerAccount
    {
        public CustomerAccount()
        {
            AddressInfo = new PhoneAddress();
            User = new WFSUser();
        }

        public PhoneAddress AddressInfo { get; set; }
        public WFSUser User { get; set; }
    }
}
