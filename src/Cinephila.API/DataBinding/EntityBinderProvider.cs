using Cinephila.Domain.DTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.DataBinding
{
    public class EntityBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(ProductionDto))
            {
                return null;
            }

            var subclasses = new[] { typeof(MovieDto), typeof(TVShowDto), };

            var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
            foreach (var type in subclasses)
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
                binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
            }

            return new ProductionModelBinder(binders);
        }
    }
}
