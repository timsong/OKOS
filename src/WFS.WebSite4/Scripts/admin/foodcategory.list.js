(function (window) {
    window.foodcategory = {

        openEditWindow: function (e) {
            var id = $(this).attr('msid');
            var url = '/Admin/FoodCategory/EditFoodCategory/{FoodCategoryID}'.bind({ FoodCategoryID: id });
            ms.ajax.send({ url: url
                , successHandler: function (data) {
                    ms.ml.html('#modalEditWindow', data);
                    $('#modalEditWindow').reveal();
                }
            });
        }

        , saveFoodCategory: function (e) {
            var data = $('#foodCategoryEditForm').serialize();
            var url = '/Admin/FoodCategory/Save';
            ms.ajax.send({ url: url
				, type: 'POST'
				, data: data
				, successHandler: function (data) {
				    if (data.Status == 0) {
				        $('#modalEditWindow', data.HtmlResult);
				    }
				    else {
				        $('#foodCategoryListPanel', data.HtmlResult);
				        $('.close-reveal-modal').click();
				    }
				}
            });
        }
    };


    $(document).ready(function () {
    });

})(window);
