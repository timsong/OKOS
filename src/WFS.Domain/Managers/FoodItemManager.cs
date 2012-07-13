using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;

namespace WFS.Domain.Managers
{
    public class FoodItemManager
    {
        private WFSRepository _repository;

        public FoodItemManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetFoodOptionsByVendorResponse GetFoodOptionsByVendorId(GetFoodOptionsByVendorRequest request)
        {
            var response = new GetFoodOptionsByVendorResponse();

            var query = new GetFoodOptionListQuery(request.VendorId);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.FoodOptions = result.Values;

            return response;

        }
        public GetFoodItemsByVendorIdResponse GetFoodItemsByVendor(GetFoodItemsByVendorIdRequest request)
        {
            var response = new GetFoodItemsByVendorIdResponse();

            var query = new GetFoodItemListQuery(request.VendorId, request.ActiveDataRequest);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.FoodItems = result.Values;

            return response;
        }

        public CreateMenuResponse CreateMenuByVendor(CreateMenuRequest request)
        {
            var resp = new CreateMenuResponse();

            var faCmd = new CreateVendorMenuCommand(request.Name, request.VendorId, request.Description, request.IsActive);
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);

            return resp;
        }
    }
}
