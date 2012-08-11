﻿/// <reference path="/Scripts/okos/okos.ms.js" />
/// <reference path="/Scripts/okos/okos.mikerowsoft.js" />
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
		, deleteF: function (url, type, name, onfin, onerr) {
			var msgs = {
				CONFIRM: 'Are you sure that you want to delete {type}: {name}?'.bind({ type: type, name: name })
				, CANCELLED: 'Delete of {type} was cancelled'.bind({ type: type })
			}
			var msgC = ms.message.get('delete', '#vendorMessagePanel', msgs);
			var deleteF = function () {
				ms.ajax.send({ url: url
				, type: 'POST'
				, errorHandler: function (data) {
					msgC.sendError(msgC.msgs.SYSTEMERROR);
				}
				, successHandler: function (data) {
					if (data.Status != 2) {
						msgC.send(data.Status, data);
						if ($.isFunction(onerr)) {
							onerr(data);
						}
					}
					else {
						msgC.sendInfo(data);
						if ($.isFunction(onfin)) {
							onfin(data);
						};
					}
				}
				});
			};
			ms.modal.confirm(msgs.CONFIRM
				, function (value) {
					if (value == ms.modal.confirmValues.YES) {
						deleteF();
					}
					else {
						msgC.sendWarning(msgC.msgs.CANCELLED);
					}
				});
		}
		, saveF: function (url, formSel, onfin, onerr) {
			var data = $(formSel).serialize();
			ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
					if (data.Status != 2) {
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
		, del: function () {
			var id = $(this).attr('msid');
			var name = $(this).attr('msname');
			var url = '/Admin/Vendors/Delete/{id}'.bind({ id: id });
			vendor.deleteF(url, 'Vendor', name, function (data) {
				ms.ml.html('#vendorListPanel', data.HtmlResult);
			});
		}
		, loadList: function (e) {
			alert('reloading the list');
		}
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

	var childControllerF = function (domain) {
		var lDomain = domain.substring(0, 1).toLowerCase() + domain.substring(1);
		var addU = '/Admin/{dom}/Add{dom}/'.bind({ dom: domain }) + '{vendorId}';
		var edU = '/Admin/{dom}/Edit{dom}/'.bind({ dom: domain }) + '{id}';
		var sel = '#{l}EditForm'.bind({ l: lDomain });
		var saveU = '/Admin/{dom}/Save'.bind({ dom: domain });
		var listSel = '#{l}ListPanel'.bind({ l: lDomain });
		var c = {
			add: function (e) {
				var id = $(this).attr('data-vendor-id');
				var url = addU.bind({ vendorId: id });
				vendor.loadF(url);
			}
		, edit: function (e) {
			var id = $(this).attr('data-id');
			var url = edU.bind({ id: id });
			vendor.loadF(url);
		}
		, save: function () {
			vendor.saveF(saveU, sel
				, function (data) {
					ms.ml.html(listSel, data.HtmlResult);
					$('#modalEdit').trigger('reveal:close');
				}
				, function (data) {
					ms.ml.html('#modalEdit', data.HtmlResult);
				}
			);
		}
		};
		return c;
	}

	window.category = childControllerF('FoodCategory');

	window.foodOption = childControllerF('FoodOption');

	$(document).ready(function () { });

})(window);
