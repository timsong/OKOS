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
            alert('Get details Clicked');
            // pop window with user info, and actions, like reset password, set credits, disable account etc.
        }

        , loginAs: function (e) {
            alert('Login As Clicked');
            //set cookies, roles, and login as person and redirect to dashbaord
        }
    };


    $(document).foundationButtons();
    $(document).ready(function () {
    });

})(window);
