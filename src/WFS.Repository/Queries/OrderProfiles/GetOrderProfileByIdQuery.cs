using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries
{
    public class GetOrderProfileByIdQuery : IQuery<C.OrderProfile>
    {
        public int _profileId { get; set; }

        public GetOrderProfileByIdQuery(int profileId)
        {
            _profileId = profileId;
        }


        public IResult<C.OrderProfile> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.UserOrderProfiles
                        where x.OrderProfileId == _profileId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.OrderProfile>(Status.Success, data);

            return result;
        }
    }
}

