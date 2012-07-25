(function (window) {
	window.vendor = {
		edit: function (e) {
			var id = $(this).attr('data-id');
			var url = '/Admin/Vendors/EditVendor/{vendorId}'.bind({ vendorId: id });
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
		, save: function (e) {
			var data = $('#vendorEditForm').serialize();
			var url = '/Admin/Vendor/Save';
			ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
					if (data.Status == 0) {
						$('#modalEdit', data.HtmlResult);
					}
					else {
						$('#vendorListPanel', data.HtmlResult);
						$('.close-reveal-modal').click();
					}
				}
			});

		}
	};

	$(document).ready(function () {
	});

})(window);
