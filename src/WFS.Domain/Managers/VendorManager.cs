using WFS.Repository;
using WFS.Contract.ReqResp;
using WFS.Repository.Queries;
using System.Collections.Generic;
using System.Linq;

namespace WFS.Domain.Managers
{
    public class VendorManager
    {
        private WFSRepository _repository;

        public VendorManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetVendorListResponse GetVendorList(GetVendorListRequest request)
        {
            var response = new GetVendorListResponse();

            var query = new GetVendorListQuery();
            var result = this._repository.ExecuteQuery(query);

            if (result.Status ==  Status.Success)
                response.Vendors = result.Values;

            return response;
        }
        public GetVendorByIdResponse GetVendorById(GetVendorByIdRequest request)
        {
            var response = new GetVendorByIdResponse();

            var query = new GetVendorByIdQuery(request.VendorID);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Vendor = result.Value;

            return response;

        }
    }
}
