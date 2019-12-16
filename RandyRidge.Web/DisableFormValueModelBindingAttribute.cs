using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RandyRidge.Common;

namespace RandyRidge.Web {
    /// <summary>
    ///     Attribute to disable form value model binding.
    /// </summary>
    /// <remarks>
    ///     Used to more efficiently stream file content instead of the
    ///     default model binding that loads the body into memory.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter {
        /// <inheritdoc />
        public void OnResourceExecuted(ResourceExecutedContext context) {
        }

        /// <inheritdoc />
        public void OnResourceExecuting(ResourceExecutingContext context) {
            context = Guard.NotNull(context, nameof(context));
            RemoveFormValueProvider<FormValueProviderFactory>(context);
            RemoveFormValueProvider<JQueryFormValueProviderFactory>(context);
        }

        private static void RemoveFormValueProvider<T>(ResourceExecutingContext context) where T : IValueProviderFactory {
            var valueProviderFactory = context.ValueProviderFactories
                .OfType<T>()
                .FirstOrDefault();

            if(valueProviderFactory != null) {
                context.ValueProviderFactories.Remove(valueProviderFactory);
            }
        }
    }
}
