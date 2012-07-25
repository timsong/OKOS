using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Repository;
using WFS.Repository.Queries;

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

            var query = new GetOrganizationsByTypeListQuery(OrganizationTypeEnum.School);
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
                response.School = result.Value;

            return response;
        }
    }
}
