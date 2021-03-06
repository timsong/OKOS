﻿using WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;
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

		#region vendor
		public GetOrganizationListByTypeResponse GetVendorList(GetOrganizationByTypeListRequest request)
        {
            var response = new GetOrganizationListByTypeResponse();

            var query = new GetOrganizationsByTypeListQuery(request.Type, request.DataRequest);
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

		public DeleteVendorResponse DeleteVendor(DeleteVendorRequest request)
		{
			var resp = new DeleteVendorResponse();

			var command = new DeleteVendorCommand(request.VendorId);

			resp = _repository.ExecuteCommand(command) as DeleteVendorResponse;

			return resp;
		}

		public SaveVendorResponse SaveVendor(SaveVendorRequest request)
		{
			var resp = new SaveVendorResponse();

			resp.Value = request.Subject;

			var user = request.Subject.User;

			var userCommand = new SaveWFSUserCommand(request.Subject.User);
	
			var userResponse = _repository.ExecuteCommand(userCommand);

			resp.Merge<Vendor, WFSUser>((Result<WFSUser>)userResponse);

			if (resp.Status != Status.Success)
				return resp;

			request.Subject.User = userResponse.Value;

			var vendorCommand = new SaveVendorCommand(request.Subject);

			var venRes = _repository.ExecuteCommand(vendorCommand);

			((Result<Vendor>)venRes).Merge<Vendor, Vendor>(resp);

			if (resp.Status == Status.Success)
				resp.Value = (Vendor)venRes.Value;

			return resp;
		}
		#endregion

		#region food category
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

		public SaveFoodCategoryResponse SaveFoodCategory(SaveFoodCategoryRequest request)
		{
			var resp = new SaveFoodCategoryResponse();

			var command = new SaveFoodCategoryCommand(request.Subject);

			resp = _repository.ExecuteCommand(command) as SaveFoodCategoryResponse;

			return resp;
		}

		public DeleteFoodCategoryResponse DeleteFoodCategory(DeleteFoodCategoryRequest request)
		{
			var resp = new DeleteFoodCategoryResponse();

			var command = new DeleteFoodCategoryCommand(request.FoodCategoryId);

			resp = _repository.ExecuteCommand(command) as DeleteFoodCategoryResponse;

			return resp;
		}
		#endregion

		#region food option
		public GetFoodOptionsByVendorResponse GetFoodOptionByVendorId(GetFoodOptionsByVendorRequest request)
		{
			var response = new GetFoodOptionsByVendorResponse();

			var query = new GetFoodOptionsByVendorQuery(request.VendorId);

			var result = this._repository.ExecuteQuery(query);

			if (result.Status == Status.Success)
				response.FoodOptions = result.Value;

			return response;
		}
		
		public GetFoodOptionByIdResponse GetFoodOptionById(GetFoodOptionByIdRequest request)
		{
			var response = new GetFoodOptionByIdResponse();

			var query = new GetFoodOptionByIdQuery(request.FoodOptionId);

			var result = this._repository.ExecuteQuery(query);

			if (result.Status == Status.Success)
				response.FoodOption = result.Value;

			return response;
		}

		public Contract.ReqResp.SaveFoodOptionResponse SaveFoodOption(Contract.ReqResp.SaveFoodOptionRequest request)
		{
			var resp = new SaveFoodOptionResponse();

			var command = new SaveFoodOptionCommand(request.Subject);

			resp = _repository.ExecuteCommand(command) as SaveFoodOptionResponse;

			return resp;
		}

		public DeleteFoodOptionResponse DeleteFoodOption(DeleteFoodOptionRequest request)
		{
			var resp = new DeleteFoodOptionResponse();

			var command = new DeleteFoodOptionCommand(request.FoodOptionId);

			resp = _repository.ExecuteCommand(command) as DeleteFoodOptionResponse;

			return resp;
		}

		#endregion

		#region food item
		public GetFoodItemsByVendorIdResponse GetFoodItemsByVendorId(GetFoodItemsByVendorIdRequest request)
		{
			var response = new GetFoodItemsByVendorIdResponse();

			var query = new GetFoodItemsByVendorQuery(request.VendorId);
			var result = this._repository.ExecuteQuery(query);

			if (result.Status == Status.Success)
				response.FoodItems = result.Value;

			return response;
		}

		public GetFoodItemByIdResponse GetFoodItemById(GetFoodItemByIdRequest request)
		{
			var response = new GetFoodItemByIdResponse();

			var query = new GetFoodItemByIdQuery(request.FoodItemId);
			var result = this._repository.ExecuteQuery(query);

			if (result.Status == Status.Success)
				response.FoodItem = result.Value;

			return response;
		}

		public SaveFoodItemResponse SaveFoodItem(SaveFoodItemRequest request)
		{
			var resp = new SaveFoodItemResponse();

			var command = new SaveFoodItemCommand(request.Subject);

			resp = _repository.ExecuteCommand(command) as SaveFoodItemResponse;

			return resp;
		}

		public DeleteFoodItemResponse DeleteFoodItem(DeleteFoodItemRequest request)
		{
			var resp = new DeleteFoodItemResponse();

			var command = new DeleteFoodItemCommand(request.FoodItemId);

			resp = _repository.ExecuteCommand(command) as DeleteFoodItemResponse;

			return resp;
		}

		public SetFoodOptionForFoodItemResponse SetFoodOptionForFoodItem(SetFoodOptionForFoodItemRequest request)
		{
			var resp = new SetFoodOptionForFoodItemResponse();

			var command = new SetFoodOptionForFoodItemCommand(request.FoodItemId, request.FoodOptionId, request.Selected);

			resp = _repository.ExecuteCommand(command) as SetFoodOptionForFoodItemResponse;

			return resp;
		}
		#endregion
	}
}
