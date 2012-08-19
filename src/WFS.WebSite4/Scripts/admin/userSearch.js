(function (window) {
    window.usersearch = {

        performSearch: function (e) {
            var searchText = $('#searchProperty').val();
            var filter = $('#ddlRoleChoice').val();

            var url = '/Admin/Users/Search/{searchText}/{filter}'.bind({ searchText: searchText, filter: filter });

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#searchResults', data.HtmlResult);
                }
            });
        }

        , getDetails: function (e) {
            var id = $(this).attr('data-id');
            var url = '/Admin/Users/UserInfo/{userId}'.bind({ userId: id });

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEdit', data.HtmlResult);
                    $('#modalEdit').reveal();
                }
            });
        }

        , loginAs: function (e) {
            var id = $(this).attr('data-id');
            var url = '/Admin/LoginAs/{userId}'.bind({ userId: id });

            document.location(url);
        }

        , updateAccountInfo: function (e) {
            var msgs = { SUCCEED: 'The account updated successfully.' }
            var msgC = ms.message.get('update', '#infoMessagePanel', msgs);

            var url = '/Admin/Users/UpdateUserInfo';
            var data = $(accountInfoForm).serialize();

            ms.ajax.send({
                url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status != 2) {
				        msgC.sendError(data.Status, data);
				        ms.ml.html('#modalEdit', data.HtmlResult);
				    }
				    else {
				        msgC.sendInfo(msgC.msgs.SUCCEED);
				    }
				}
            });
        }

        , updateAccountBalance: function (e) {
            var msgs = { SUCCEED: 'The account balance has been updated.' }
            var msgC = ms.message.get('balance', '#balanceMessagePanel', msgs);

            var url = '/Admin/Users/UpdateUserBalance';
            var data = $(balanceForm).serialize();

            ms.ajax.send({
                url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status != 2) {
				        msgC.sendError(data.Status, data);
				        ms.ml.html('#modalEdit', data.HtmlResult);
				    }
				    else {
				        msgC.sendInfo(msgC.msgs.SUCCEED);
				    }
				}
            });
        }
    };


    $(document).ready(function () {
    });

})(window);
