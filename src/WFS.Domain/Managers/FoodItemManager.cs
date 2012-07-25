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

        public GetFoodItemsByVendorIdResponse GetFoodItemsByVendor(GetFoodItemsByVendorIdRequest request)
        {
            var response = new GetFoodItemsByVendorIdResponse();

            var query = new GetFoodItemListQuery(request.VendorId, request.ActiveDataRequest);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.FoodItems = result.Values;

            return response;
        }

        public CreateFoodItemResponse CreateFoodItem(CreateFoodItemRequest request)
        {
            var resp = new CreateFoodItemResponse();

            var faCmd = new CreateFoodItemCommand(request.Name, request.Description, request.FoodCatId, request.IsActive, request.Cost, request.Price);
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);
            if (resp.Status == Status.Success)
                resp.FoodItem = fcRes.Value;

            return resp;
        }
        public CreateMenuResponse CreateMenuByVendor(CreateMenuRequest request)
        {
            var resp = new CreateMenuResponse();

            var faCmd = new CreateVendorMenuCommand(request.Name, request.VendorId, request.Description, request.IsActive);
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);
            if (resp.Status == Status.Success)
                resp.Menu = fcRes.Value;

            return resp;
        }
    }
}
