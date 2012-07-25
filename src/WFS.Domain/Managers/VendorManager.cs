using System;
using System.Web.Security;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;
using WFS.Contract;
using WFS.Contract.ReqResp.Creates;
using WFS.Repository.Commands.Vendor;

namespace WFS.Domain.Managers
{
    public class VendorManager
    {
        private WFSRepository _repository;

        public VendorManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetOrganizationListByTypeResponse GetVendorList(GetOrganizationByTypeListRequest request)
        {
            var response = new GetOrganizationListByTypeResponse();

            var query = new GetOrganizationsByTypeListQuery(request.Type);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Organizations = result.Values;

            return response;
        }
        public GetOrganizationByIdResponse GetVendorById(GetOrganizationByIdRequest request)
        {
            var response = new GetOrganizationByIdResponse();

            var query = new GetOrganizationByIdQuery(request.OrganizationID);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Organization = result.Value;

            return response;
        }

        public ChangeOrganizationActiveStatusResponse ChangeVendorActiveStatus(ChangeOrganizationActiveStatusRequest request)
        {
            var resp = new ChangeOrganizationActiveStatusResponse();

            var command = new ChangeOrganizationActivateStatusCommand(request.OrganizationID, request.NewActiveStatus);
            var result = _repository.ExecuteCommand(command);

            if (result.Status == Status.Success)
                resp.Organization = (Vendor)result.Value;

            return resp;
        }

		public SaveVendorResponse SaveVendor(SaveVendorRequest request)
		{
			var command = new SaveVendorCommand(request.Subject);

			var resp = (SaveVendorResponse)_repository.ExecuteCommand<Vendor>(command);

			return resp;
		}

        public CreateFoodOptionResponse CreateFoodOption(CreateFoodOptionRequest request)
        {
            var resp = new CreateFoodOptionResponse();

            //Create Vendor and VendorUser
            var faCmd = new CreateFoodOptionCommand(request.Name, request.VendorId, request.Description, request.Cost, request.Price);
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);
            if (resp.Status == Status.Success)
                resp.FoodOption = fcRes.Value;

            return resp;
        }
        public CreateFoodCategoryResponse CreateFoodCategory(CreateFoodCategoryRequest request)
        {
            var resp = new CreateFoodCategoryResponse();

            //Create Vendor and VendorUser
            var faCmd = new CreateFoodCategoryCommand(request.Name, request.VendorID, request.CategoryType.ToString());
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);
            if (resp.Status == Status.Success)
                resp.FoodCategory = fcRes.Value;

            return resp;
        }
       
    }
}
