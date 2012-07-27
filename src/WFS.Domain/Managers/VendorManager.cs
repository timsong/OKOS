﻿using WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;
using WFS.Contract.ReqResp.Creates;
using WFS.Repository.Commands.Vendor;
using System.Web.Security;
using WFS.Framework.Extensions;
using WFS.Framework;
using System;
using WFS.Contract.Enums;

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
        public GetFoodCategoriesByVendorResponse GetFoodCategoriesByVendor(GetFoodCategoriesByVendorRequest request)
        {
            var response = new GetFoodCategoriesByVendorResponse();

            var query = new GetFoodCategoryListQuery(request.VendorId);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.FoodCategories = result.Values;

            return response;
        }
        public GetFoodCategoryByIdResponse GetFoodCategoryById(GetFoodCategoryByIdRequest request)
        {
            var response = new GetFoodCategoryByIdResponse();

            var query = new GetFoodCategoryByIdQuery(request.FoodCategoryID);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.FoodCategory = result.Value;

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
			var resp = new SaveVendorResponse();

			var user = request.Subject.User;

			var userCommand = new SaveWFSUserCommand(request.Subject.User);
	
			var userResponse = _repository.ExecuteCommand(userCommand);

			resp.Merge<Vendor, WFSUser>((Result<WFSUser>)userResponse);

			if (resp.Status != Status.Success)
				return resp;

			var vendorCommand = new SaveVendorCommand(request.Subject);

			var venRes = _repository.ExecuteCommand(vendorCommand);

			((Result<Vendor>)venRes).Merge<Vendor, Vendor>(resp);

			if (resp.Status == Status.Success)
				resp.Value = (Vendor)venRes.Value;

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
