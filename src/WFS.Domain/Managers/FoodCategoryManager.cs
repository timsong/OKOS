using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;

namespace WFS.Domain.Managers
{
    public class FoodCategoryManager
    {
        private WFSRepository _repository;

        public FoodCategoryManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public CreateFoodCategoryResponse CreateFoodCategory(CreateFoodCategoryRequest request)
        {
            var resp = new CreateFoodCategoryResponse();

            //Create Vendor and VendorUser
            var faCmd = new CreateFoodCategoryCommand(request.Name, request.VendorID, request.CategoryType.ToString());
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);

            return resp;
        }
    }
}
