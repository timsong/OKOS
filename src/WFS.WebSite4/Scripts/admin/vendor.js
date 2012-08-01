(function (window) {
    window.vendor = {
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
		, list: function (e, callback) {

		}
		, mode: null
		, add: function (e) {
		    vendor.mode = 'add';
		    var url = '/Admin/Vendors/Add';
		    vendor.loadF(url);
		}
		, edit: function (e) {
		    vendor.mode = 'edit';
		    var id = $(this).attr('data-id');
		    var url = '/Admin/Vendors/EditVendor/{vendorId}'.bind({ vendorId: id });
		    vendor.loadF(url);
		}
		, save: function (e) {
		    vendor.saveF('/Admin/Vendor/Save', '#vendorEditForm'
				, function (data) {
				    if (vendor.mode == 'edit') {
				        ms.ml.html('#displayAddressPanel', data.AdditionalPayload.addressHtml);
				        ms.ml.html('#displayContactPanel', data.AdditionalPayload.contactHtml);
				        $('#modalEdit').trigger('reveal:close');
				    }
				    else {
				        document.location = '/Admin/Vendors/DisplayVendor/{id}'.bind({ id: data.Subject.Vendor.OrganizationId });
				    }
				}
				, function (data) {
				    ms.ml.html('#modalEdit', data.HtmlResult);
				}
			);
		}
    };

    window.category = {
        add: function (e) {
            var id = $(this).attr('data-vendor-id');
            var url = '/Admin/FoodCategory/AddFoodCategory/{vendorId}'.bind({ vendorId: id });
            vendor.loadF(url);
        }
		, edit: function (e) {
		    var id = $(this).attr('data-id');
		    var url = '/Admin/FoodCategory/EditFoodCategory/{foodCategoryId}'.bind({ foodCategoryId: id });
		    vendor.loadF(url);
		}
		, save: function () {
		    vendor.saveF('/Admin/FoodCategory/Save', '#foodCategoryEditForm'
				, function (data) {
				    ms.ml.html('#foodCategoryListPanel', data.HtmlResult);
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
