using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C = WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Repository.Conversions;
using System.Data.Objects;
using System.Data.Entity;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands.Vendor
{
	public class DeleteVendorCommand : ICommand<bool>
	{
		private readonly int _vendorId;

		public DeleteVendorCommand(int vendorId)
		{
			_vendorId = vendorId;
		}

		public IResult<bool> Execute(DbContext dbContext)
		{
			var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new DeleteVendorResponse ();

			try
			{
				var org = context.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(_vendorId));

				org.IsDeleted = true;

				context.SaveChanges();

				result.Status = Status.Success;

				result.Messages.Add(new Message { Code = "SUCCEEDEDDELETEVENDOR", Text = "Vendor was deleted successfully" });
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "FAILEDTODELETEVENDOR", Text = ex.ToString() });
			}

			return result;
		}
	}
}
