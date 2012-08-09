(function (window) {
    window.customerprofile = {

        listProfiles: function (e) {
            var memId = $("#membershipId").val();
            var url = '/Customer/Profile/GetList/{membershipId}'.bind({ membershipId: memId });
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#profileListPanel', data.HtmlResult);
                }
            });
        }

        , showAddProfile: function (e) {
            var memId = $("#membershipId").val();
            var url = '/Customer/Profile/Add';

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEdit', data.HtmlResult);
                    $('#modalEdit').reveal();
                }
            });
        }

    };


    $(document).ready(function () {
        customerprofile.listProfiles();
    });

})(window);
