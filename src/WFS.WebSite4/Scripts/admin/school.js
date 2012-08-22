(function (window) {
    window.school = {
        loadF: function (url) {
            ms.ajax.send({ url: url
				, successHandler: function (data) {
				    ms.ml.html('#modalEdit', data.HtmlResult);
				    $('#modalEdit').reveal({
				        animation: 'fadeAndPop'
						, animationspeed: 300
						, closeOnBackgroundClick: true
						, dismissModalClass: 'close-reveal-modal'
				    });
				    $('#deliveryTime').timepicker({
                        hours: { starts: 6, ends: 19 },
                        minutes: { interval: 15 },
                        rows: 3,
                        showPeriod: true,
                        showPeriodLabels: true,
                        minuteText: 'Min'
                    });
				}
            });
        }
        , saveF: function (url, formSel, onfin, onerr) {
            var data = $(formSel).serialize();
            ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status == 0 || data.Status == 4) {
				        ms.ml.html('#modalEdit', data.HtmlResult);
				        if ($.isFunction(onerr)) {
				            onerr(data);
				        };
				    }
				    else {
				        if ($.isFunction(onerr)) {
				            onfin(data);
				        };
				    }
				}
            });
        }

        , getList: function (e) {
            var url = '/admin/Schools/GetList';
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#schoolList', data.HtmlResult);
                }
            });
        }
		, add: function (e) {
		    var url = '/admin/schools/create';
		    school.loadF(url);
		}
        , edit: function (e) {
            var id = $(this).attr('data-id');
            var url = '/admin/schools/edit/{schoolId}'.bind({ schoolId: id });
            school.loadF(url);
        }
		, save: function (e) {
		    var msgs = { SUCCEED: 'The school was created.'}

		    school.saveF('/admin/schools/create', '#schoolEditForm'
				, function (data) {
				    var url = '/admin/schools/{schoolId}'.bind({ schoolId: data.Subject.School.OrganizationId });
				    document.location(url);
				}
				, function (data) {
				    var msgC = ms.message.get('save', '#schoolMessagePanel', msgs);
				    msgC.sendError(data.Status, data);
				    ms.ml.html('#modalEdit', data.HtmlResult);
				}
			);
		}
    };

    $(document).ready(function () {
        school.getList();
    });
})(window);