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
            $('#foodCategoryEditForm').submit();
        }
    };


    $(document).ready(function () {
    });

})(window);
