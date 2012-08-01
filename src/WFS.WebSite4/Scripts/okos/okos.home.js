(function (window) {
    window.home = {

        openNewSupportTicket: function (e) {
            var url = '/Support/NewSupportTicket';
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalNewSupportTicketWindow', data.HtmlResult);
                    $('#modalNewSupportTicketWindow').reveal();
                }
            });
        }

        , saveSupportTicket: function (e) {
            var url = '/Support/NewSupportTicket';

            var data = $(newSupportForm).serialize();
            ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status == 0 || data.Status == 4) {
				        ms.ml.html('#modalNewSupportTicketWindow', data.HtmlResult);
				    }
				    else {
				            $('#modalNewSupportTicketWindow').trigger('reveal:close');
				    }
				}
            });
        }
    };
})(window);