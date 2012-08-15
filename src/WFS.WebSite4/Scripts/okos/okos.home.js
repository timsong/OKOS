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

        , loadTickets: function (e) {
            var url = '/Support/Tickets/GetList';
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#ticketList', data.HtmlResult);
                }
            });
        }

        , viewTicket: function (e) {
            var id = $(this).attr('data-id');
            var url = '/Support/Tickets/Get/{ticketId}'.bind({ ticketId: id });

            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#resolveTicketModal', data.HtmlResult);
                    $('#resolveTicketModal').reveal();
                }
            });
        }

        , resolveSupportTicket: function (e) {
            var url = '/Support/Tickets/Resolve';

            var data = $(resolveSupportForm).serialize();
            ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status == 0 || data.Status == 4) {
				        ms.ml.html('#resolveTicketModal', data.HtmlResult);
				    }
				    else {
				        $('#resolveTicketModal').trigger('reveal:close');
				        home.loadTickets();
				    }
				}
            });
        }

        , createAccount: function (e) {
            var url = '/register';

            var msgs = {
		          SUCCEED: 'Your Account was created'
				, FAILED: 'Account creation failed, please file a support ticket'
            }
            var msgC = ms.message.get('save', '#registerMessagePanel', msgs);

            $('#termsModal').trigger('reveal:close');
            var data = $(newRegistrationForm).serialize();
            ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status == 0 || data.Status == 4) {
				        msgC.sendError(data.Status, data);
				    }
				    else {
				        var profUrl = '/Customer/Profile/Index';
				        window.location(profUrl);
				    }
				}
				, errorHandler: function (data) {
				    msgC.sendError(msgC.msgs.SYSTEMERROR, data.responseText);
				}
            });
        }

        , showTerms: function () {
            var url = '/TermsAndConditions';
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#termsModal', data.HtmlResult);
                    $('#termsModal').reveal();
                }
                , errorHandler: function (data) {
                    ms.ml.html('#termsModal', data.HtmlResult);
                    $('#termsModal').reveal();
                }
            });

        }


    };
})(window);