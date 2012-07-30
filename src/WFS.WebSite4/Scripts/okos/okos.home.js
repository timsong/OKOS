(function (window) {
	window.home = {

	    openNewSupportTicket: function (e) {
	        var id = $(this).attr('msid');
	        var url = '/Admin/FoodCategory/EditFoodCategory/{FoodCategoryID}'.bind({ FoodCategoryID: id });
	        ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEditWindow', data);
                    $('#modalEditWindow').reveal();
                }
	        });
        }

	};
})(window);