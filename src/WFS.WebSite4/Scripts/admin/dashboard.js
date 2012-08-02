(function (window) {
    window.dashboard = {

        loadTickets: function (e) {
            var url = '/Support/Tickets/GetList';
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#ticketList', data.HtmlResult);
                }
            });
        }
    };


    $(document).ready(function () {
        dashboard.loadTickets();
    });

})(window);
