(function (window) {
	window.home = {

	    openNewSupportTicket: function (e) {
	        var url = '/Support/NewSupportTicket';
	        ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalNewSupportTicketWindow', data);
                    $('#modalNewSupportTicketWindow').reveal();
                }
	        });
        }

	};
})(window);