using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BindingDemo.Binders
{
    public class TagsModelBinder : IModelBinder
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

            IEnumerable<string> tags=value.Split(",");



            bindingContext.Result = ModelBindingResult.Success(tags);
            return Task.CompletedTask;
        }
    }
}
