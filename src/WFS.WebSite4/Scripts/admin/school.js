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
		, add: function (e) {
		    var url = '/admin/schools/create';
		    school.loadF(url);
		}
		, save: function (e) {
		    school.saveF('/admin/schools/create', '#schoolEditForm'
				, function (data) {
				    $('#modalEdit').trigger('reveal:close');
				}
				, function (data) {
				    ms.ml.html('#modalEdit', data.HtmlResult);
				}
			);
		}
    };

    $(document).ready(function () {
    });
})(window);