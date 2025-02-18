using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Error;
using Talabat.Api.Helper;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Repository;

namespace Talabat.Api.Extention
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MapperFile));

            #region Way To Configure The ApiBehaviorOptions To Return The Validation Error
            // This is The Way To Configure The ApiBehaviorOptions To Return The Validation Error
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                              .SelectMany(x => x.Value.Errors)
                                              .Select(x => x.ErrorMessage)
                                              .ToArray();
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            #endregion
            return Services;
        }
    }
}
