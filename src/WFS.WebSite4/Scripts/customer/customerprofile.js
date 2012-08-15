(function (window) {
    window.customerprofile = {

        listProfiles: function (e) {
            var userId = $("#userId").val();
            var url = '/Customer/Profile/GetList/{userId}'.bind({ userId: userId });
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#profileListPanel', data.HtmlResult);
                }
            });
        }

        , showAddProfile: function (e) {
            var id = $(this).attr('data-id');
            var url = '/Customer/Profile/Add/{userId}'.bind({ userId: id });

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEdit', data.HtmlResult);
                    $('#modalEdit').reveal();
                }
            });
        }

        , showEditProfile: function (e) {
            var id = $(this).attr('data-id');
            var url = '/Customer/Profile/edit/{profileId}'.bind({ profileId: id });

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEdit', data.HtmlResult);
                    $('#modalEdit').reveal();
                }
            });
        }

        , loadChoiceScreen: function (e) {
            var isSchool = $("#IsSchoolRB").attr('checked');
            var isCompany = $("#IsCompanyRB").attr('checked');
            var isSchoolChecked = (isSchool == "checked");

            $("#IsSchool").val(isSchoolChecked);

            var url = '/Customer/Profile/SetInfo';
            var data = $(newOrderProfile).serialize();
            ms.ajax.send({
                url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    ms.ml.html('#infoArea', data.HtmlResult);
				    $('#ddlSchoolChoice').change(function () {
				        customerprofile.loadStudentInfo();
				    });
				}
            });
        }

        , loadStudentInfo: function (e) {
            var url = '/Customer/Profile/SetSchoolInfo';
            var data = $(newOrderProfile).serialize();

            ms.ajax.send({
                url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    ms.ml.html('#studentInfoArea', data.HtmlResult);
				}
            });
        }

        , saveProfile: function (e) {
            var url = '/Customer/Profile/Save';
            var data = $(newOrderProfile).serialize();

            ms.ajax.send({
                url: url
				        , type: 'POST'
				        , data: data
				        , successHandler: function (data) {
				            if (data.Status == 0 || data.Status == 4) {
				                ms.ml.html('#modalEdit', data.HtmlResult);
				            }
				            else {
				                $('#modalEdit').trigger('reveal:close');
				                customerprofile.listProfiles();
				            }
				        }
            });
        }

		, deleteProfile: function (e) {
		    var profileName = $(this).attr('data-name');
		    var id = $(this).attr('data-id');
		    var userId = $(this).attr('data-userId');

		    var msgs = {
		          CONFIRM: 'Are you sure that you want to delete the profile for {name}?'.bind({ name: profileName })
		        , SUCCEED: 'The profile for {name} was deleted.'.bind({ name: profileName })
				, CANCELLED: 'Delete of profile was cancelled'
		    }
		    var msgC = ms.message.get('delete', '#profileMessagePanel', msgs);

		    ms.modal.confirm(msgs.CONFIRM
				, function (value) {
				    if (value == ms.modal.confirmValues.YES) {
				        var url = '/Customer/Profile/Delete/{profileID}/{userId}'.bind({ profileID: id, userId: userId });

				        ms.ajax.send({
				            url: url
				            , type: 'POST'
				            , errorHandler: function (data) {
					            msgC.sendError(msgC.msgs.SYSTEMERROR, data.responseText);
				            }
				            , successHandler: function (data) {
				                if (data.Status == 0 || data.Status == 4) {
						            msgC.send(data.Status, data);
				                    ms.ml.html('#modalEdit', data.HtmlResult);
				                }
				                else {
						            msgC.sendInfo(data);
				                    $('#modalEdit').trigger('reveal:close');
				                    customerprofile.listProfiles();
				                }
				            }
				        });
				    }
				else {
				        msgC.sendWarning(msgC.msgs.CANCELLED);
				    }
				});

		}

    };


    $(document).ready(function () {
        customerprofile.listProfiles();
    });

})(window);
