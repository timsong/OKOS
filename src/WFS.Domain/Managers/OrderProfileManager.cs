using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;
using WFS.Contract;
using WFS.Contract.Enums;

namespace WFS.Domain.Managers
{
    public class OrderProfileManager
    {
        private readonly WFSRepository _repository;
        private readonly SchoolManager _schoolManager;

        public OrderProfileManager(WFSRepository repository, SchoolManager schoolManager)
        {
            _repository = repository;
            _schoolManager = schoolManager;
        }

        public GetOrderProfileResponse GetOrderProfileById(GetOrderProfileByIdRequest request)
        {
            var response = new GetOrderProfileResponse();

            var query = new GetOrderProfileByIdQuery(request.ProfileId);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);
            if (result.Status == Status.Success)
                response.Value = result.Value;

            return response;
        }
        public GetOrderProfileListResponse GetListOfProfiles(GetOrderProfileListRequest request)
        {
            var resp = new GetOrderProfileListResponse();

            var q = new GetOrderProfileListByUserIdQuery(request.UserId);
            var result = _repository.ExecuteQuery(q);

            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Values = result.Values;

            return resp;
        }

        public SaveOrderProfileResponse SaveOrderProfile(SaveOrderProfileRequest request)
        {
            var resp = new SaveOrderProfileResponse();

            var command = new SaveOrderProfileCommand(request.Profile);
            var result = _repository.ExecuteCommand(command);
            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Value = result.Value;

            return resp;
        }

        public GetOrderProfleSetupDataResponse GetOrderProfleSetupDataBySchool (GetOrderProfleSetupDataRequest request)
        {
            var resp = new GetOrderProfleSetupDataResponse();

            resp.Teachers = _schoolManager.GetTeachers(new GetTeachersRequest() { SchoolId = request.SchoolId, DataRequest = ActiveDataRequestEnum.ActiveOnly }).Values;
            resp.Grades = _schoolManager.GetGrades(new GetGradesRequest() { SchoolId = request.SchoolId, DataRequest = ActiveDataRequestEnum.ActiveOnly }).Values;
            resp.LunchPeriods = _schoolManager.GetLunchPeriodss(new GetLunchPeriodsRequest() { SchoolId = request.SchoolId, DataRequest = ActiveDataRequestEnum.ActiveOnly }).Values;

            return resp;
        }

        public DeleteOrderProfileResponse DeleteOrderProfile (DeleteOrderProfileRequest request)
        {
            var resp = new DeleteOrderProfileResponse();

            var command = new DeleteOrderProfileCommand(request.ProfileId);
            var result = _repository.ExecuteCommand(command);
            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Value = result.Value;

            return resp;
        }
            
    }
}
