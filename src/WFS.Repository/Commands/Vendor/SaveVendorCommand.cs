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
	public class SaveVendorCommand : ICommand<C.Vendor>
	{
		private readonly C.Vendor _vendor;

		public SaveVendorCommand(C.Vendor vendor)
		{
			_vendor = vendor;
		}

		public IResult<C.Vendor> Execute(DbContext dbContext)
		{
			var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new SaveVendorResponse ();

			result.Subject = _vendor;

			try
			{
				if (_vendor.OrganizationId > 0)
				{
					var org = context.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(_vendor.OrganizationId));

					org.ForUpdate(_vendor);

					dbContext.SaveChanges();

					result.Value = (C.Vendor)org.ToContract();
				}
				else
				{
					var org = _vendor.ToDataModel();

					context.Organizations.Add(org);

					dbContext.SaveChanges();

					result.Value = (C.Vendor)org.ToContract();
				}
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "DIE!", Text = ex.ToString() });
			}

			return result;
		}
	}
}
