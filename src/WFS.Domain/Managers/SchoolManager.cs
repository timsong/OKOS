using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Repository;
using WFS.Repository.Queries;
using WFS.Repository.Commands;
using WFS.Contract;
using WFS.Framework;
using WFS.Repository.Commands.School;

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

        public SaveSchoolResponse CreateSchool(SaveSchoolRequest request)
        {
            var response = new SaveSchoolResponse();
            response.Value = request.Subject;

            var user = request.Subject.User;
            var userResponse = _repository.ExecuteCommand(new SaveWFSUserCommand(request.Subject.User));

            response.Merge(userResponse);

            if (response.Status != Status.Success)
                return response;

            request.Subject.User = userResponse.Value;
            var schoolResponse = _repository.ExecuteCommand(new SaveSchoolCommand(request.Subject));

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
