using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.API.DataBinding
{
    public class ProductionModelBinder : IModelBinder
    {
        private readonly Dictionary<Type, (ModelMetadata, IModelBinder)> binders;

        public ProductionModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
        {
            this.binders = binders;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelKindName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, nameof(ProductionDto.Type));
            var modelTypeValue = Convert.ToInt16(bindingContext.ValueProvider.GetValue(modelKindName).FirstValue);

            IModelBinder modelBinder;
            ModelMetadata modelMetadata;
            if (modelTypeValue == (int)ProductionType.Movie)
            {
                (modelMetadata, modelBinder) = binders[typeof(MovieDto)];
            }
            else if (modelTypeValue == (int)ProductionType.TVShow)
            {
                (modelMetadata, modelBinder) = binders[typeof(TVShowDto)];
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
            bindingContext.ActionContext,
            bindingContext.ValueProvider,
            modelMetadata,
            bindingInfo: null,
            bindingContext.ModelName);

            await modelBinder.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;

            if (newBindingContext.Result.IsModelSet)
            {
                // Setting the ValidationState ensures properties on derived types are correctly 
                bindingContext.ValidationState[newBindingContext.Result.Model] = new ValidationStateEntry
                {
                    Metadata = modelMetadata,
                };
            }
        }
    }
}
