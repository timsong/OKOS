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

        , loadEntryScreen: function (e) {
            var isSelf = $("#IsSelfRB").attr('checked');
            var isSomone = $("#IsSomeoneElseRB").attr('checked');
            var isSchool = $("#IsSchoolRB").attr('checked');
            var isCompany = $("#IsCompanyRB").attr('checked');

            var personChecked = false;
            var schoolChecked = false;

            if (isSelf == 'checked' || isSomone == 'checked') {
                personChecked = true;
            }
            if (isSchool == 'checked' || isCompany == 'checked') {
                schoolChecked = true;
            }

            if (personChecked && schoolChecked) {

                var isSelfChecked = (isSelf == "checked");
                var isSchoolChecked = (isSchool == "checked");

                var url = '/Customer/Profile/SetInfo';

                $("#IsSelf").val(isSelfChecked);
                $("#IsSchool").val(isSchoolChecked);

                var data = $(newOrderProfile).serialize();
                ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    ms.ml.html('#infoArea', data.HtmlResult);
				}
                });
            }
        }
    };


    $(document).ready(function () {
        customerprofile.listProfiles();
    });

})(window);
