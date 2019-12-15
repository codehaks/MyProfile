using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Common
{
    public class UnorderedCollectionBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;

            var tagsModel =
                bindingContext.ValueProvider.GetValue(modelName);

            if (tagsModel == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = tagsModel.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            throw new NotImplementedException();
        }
    }
}
