using System;
using System.Data.Entity;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Commands
{
    public class SaveOrderProfileCommand : ICommand<C.OrderProfile>
    {
        private readonly C.OrderProfile _OrderProfile;

        public SaveOrderProfileCommand(C.OrderProfile OrderProfile)
        {
            _OrderProfile = OrderProfile;
        }

        public IResult<C.OrderProfile> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<C.OrderProfile>();

            try
            {
                if (_OrderProfile.OrderProfileId > 0)
                {
                    var ordProf = context.UserOrderProfiles.FirstOrDefault(x => x.OrderProfileId.Equals(_OrderProfile.OrderProfileId));
                    ordProf.ForUpdate(_OrderProfile);
                    dbContext.SaveChanges();

                    result.Value = ordProf.ToContract();
                }
                else
                {
                    var ordProf = _OrderProfile.ToDataModel();
                    context.UserOrderProfiles.Add(ordProf);
                    dbContext.SaveChanges();

                    ordProf = context.UserOrderProfiles.FirstOrDefault(x => x.OrderProfileId.Equals(ordProf.OrderProfileId));
                    ordProf.Organization = context.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(ordProf.OrganizationId));

                    result.Value = ordProf.ToContract();
                }

            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Messages.Add(new Message { Code = "SaveOrderProfileFailed", Text = ex.ToString() });
            }

            return result;
        }


    }
}
