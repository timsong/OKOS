using System;
using System.Linq;
using System.Data.Entity;
using C = WFS.Contract;
using WFS.Repository.Conversions;


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
                }
                else
                {
                    context.UserOrderProfiles.Add(_OrderProfile.ToDataModel());
                }

                dbContext.SaveChanges();
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
