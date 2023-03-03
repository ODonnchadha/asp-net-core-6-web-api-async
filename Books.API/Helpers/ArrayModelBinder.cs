using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Books.API.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext context)
        {
            if (!context.ModelMetadata.IsEnumerableType)
            {
                context.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Get the input value via value provider.
            var value = context.ValueProvider.GetValue(context.ModelName).ToString();

            // If the value is null || whitespace, return null.
            if (string.IsNullOrWhiteSpace(value))
            {
                context.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // The value is not null or whitespace. The model is type of IEnumerable.
            // Obtain the enumerable's type and a conmverter.
            var elementType = context.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);

            // COnvert each item in the value list to the enumerable type
            var values = 
                value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => converter.ConvertFromString(t.Trim())).ToArray();

            // Create an array of that type and set it as the model value.
            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);
            context.Model = typedValues;

            // Return a successful result, passing the the model.
            context.Result = ModelBindingResult.Success(context.Model);
            return Task.CompletedTask;
        }
    }
}
