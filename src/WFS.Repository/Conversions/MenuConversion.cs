using System;
using System.Linq;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class MenuConversion
    {
        public static C.Menu ToContract(this Menu data)
        {
            if (data == null)
                return null;

            var model = new C.Menu()
            {
                Name = data.Name,
                Description = data.Description,
                MenuId = data.MenuId,
                VendorId = data.VendorId
            };


            if (data.MenuItems.Count > 0)
                model.FoodItems.AddRange(data.MenuItems.Select(x => x.FoodItem.ToContract()).ToList());

            return model;
        }
    }
}

