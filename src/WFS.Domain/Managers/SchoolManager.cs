using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Repository;
using WFS.Repository.Queries;
using WFS.Repository.Commands;
using WFS.Contract;
using WFS.Framework;

namespace WFS.Domain.Managers
{
    public class SchoolManager
    {
        private WFSRepository _repository;

        public SchoolManager(WFSRepository repository)
        {
            this._repository = repository;
        }

        public GetSchoolsResponse GetSchoolList(GetSchoolsRequest request)
        {
            var response = new GetSchoolsResponse();

            var query = new GetOrganizationsByTypeListQuery(OrganizationTypeEnum.School, request.DataRequest);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Schools = result.Values;

            return response;
        }

        public GetSchoolResponse GetSchool(GetSchoolRequest request)
        {
            var response = new GetSchoolResponse();

            var query = new GetOrganizationByIdQuery(request.SchoolID);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Value = (School)result.Value;

            return response;
        }

        public CreateSchoolResponse CreateSchool(CreateSchoolRequest request)
        {
            var response = new CreateSchoolResponse();
            response.Value = request.School;

            var user = request.School.User;
            var userResponse = _repository.ExecuteCommand(new SaveWFSUserCommand(request.School.User));

            response.Merge(userResponse);

            if (response.Status != Status.Success)
                return response;

            request.School.User = userResponse.Value;
            var schoolResponse = _repository.ExecuteCommand(new CreateOrganizationCommand(request.School.AddressInfo, request.School.Name, request.School.User.UserId, null, OrganizationTypeEnum.School));

            schoolResponse.Merge(response);

            if (response.Status == Status.Success)
                response.Value = (School)schoolResponse.Value;

            return response;
        }

        public GetGradesResponse GetGrades(GetGradesRequest request)
        {
            var response = new GetGradesResponse();

            //var query = new GetOrganizationByIdQuery(request.SchoolID);
            //var result = this._repository.ExecuteQuery(query);

            //if (result.Status == Status.Success)
            //    response.Value = (School)result.Value;

            return response;
        }

        public GetTeachersResponse GetTeachers(GetTeachersRequest request)
        {
            var response = new GetTeachersResponse();

            //var query = new GetOrganizationByIdQuery(request.SchoolID);
            //var result = this._repository.ExecuteQuery(query);

            //if (result.Status == Status.Success)
            //    response.Value = (School)result.Value;

            return response;
        }

        public GetLunchPeriodsResponse GetLunchPeriodss(GetLunchPeriodsRequest request)
        {
            var response = new GetLunchPeriodsResponse();

            //var query = new GetOrganizationByIdQuery(request.SchoolID);
            //var result = this._repository.ExecuteQuery(query);

            //if (result.Status == Status.Success)
            //    response.Value = (School)result.Value;

            return response;
        }

    }
}
