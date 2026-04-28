using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Mapping
{
    public static class MapesterConfig
    {

        public static void MapesterConfigRegister() {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                      .Map(dest => dest.cat_id, src => src.Id)
                      .Map(dest => dest.userCreated, src => src.CreatedBy.UserName)
                      .Map(dest => dest.Name, src => src.Translations.Where(
                      t => t.Language == CultureInfo.CurrentCulture.Name)
                      .Select(t => t.Name).FirstOrDefault());

               TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                    .Map(des => des.UserCreated, src => src.CreatedBy.UserName)
                    .Map(dest => dest.Name, src => src.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name)

                    .Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(des => des.UserCreated, src => src.CreatedBy.UserName)
                .Map(dest => dest.Name, src => src.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name) // "ar-JO" => "ar"
                    .Select(t => t.Name)
                    .FirstOrDefault())
                .Map(des=>des.MainImage, src=>$"https://localhost:7021/images/{src.MainImage}");


        }
    }
}
