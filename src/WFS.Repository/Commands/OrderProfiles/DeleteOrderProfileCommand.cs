using System;
using System.Data.Entity;
using System.Linq;


namespace WFS.Repository.Commands
{
    public class DeleteOrderProfileCommand : ICommand<bool>
    {
        private readonly int _profileId;

        public DeleteOrderProfileCommand(int profileId)
        {
            _profileId = profileId;
        }

        public IResult<bool> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<bool>();

            try
            {
                var ordProf = context.UserOrderProfiles.FirstOrDefault(x => x.OrderProfileId.Equals(_profileId));
                ordProf.IsDeleted = true;

                dbContext.SaveChanges();

                result.Status = Status.Success;
                result.Messages.Add(new Message { Code = "DeleteProfileSuccess", Text = "Profile was deleted successfully" });
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Messages.Add(new Message { Code = "DeleteProfileFailed", Text = ex.ToString() });
            }

            return result;
        }
    }
}
