using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Shouldly;
using Xunit;

namespace RandyRidge.Web; 

public static class DisableFormValueModelBindingAttributeTester {
	private static readonly DisableFormValueModelBindingAttribute Attribute = new();

	public static class OnResourceExecuted {
		[Fact]
		public static void coverage() {
			Attribute.OnResourceExecuted(new ResourceExecutedContext(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>()));
		}
	}

	public static class OnResourceExecuting {
		[Fact]
		public static void removes_form_value_provider() {
			VerifyRemoval(BuildContext(new FormValueProviderFactory()));
		}

		[Fact]
		public static void removes_jquery_form_value_provider() {
			VerifyRemoval(BuildContext(new JQueryFormValueProviderFactory()));
		}

		private static void VerifyRemoval(ResourceExecutingContext ctx) {
			ctx.ValueProviderFactories.Count.ShouldBe(1);
			Attribute.OnResourceExecuting(ctx);
			ctx.ValueProviderFactories.Count.ShouldBe(0);
		}
	}

	private static ResourceExecutingContext BuildContext(IValueProviderFactory providerFactory) =>
		new(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>(), new List<IValueProviderFactory> {
			providerFactory
		});
}
