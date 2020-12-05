using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Shouldly;
using Xunit;

namespace RandyRidge.Web {
    public abstract class HttpRequestExtensionsTester {
        public sealed class IsConditional {
            private readonly HttpRequest ifMatchRequest;
            private readonly HttpRequest ifNoneMatchRequest;
            private readonly HttpRequest unconditionalRequest;

            public IsConditional() {
                var ifMatchContext = new DefaultHttpContext();
                ifMatchContext.Request.Headers.Add(HeaderNames.IfMatch, StringValues.Empty);
                ifMatchRequest = ifMatchContext.Request;

                var ifNoneMatchContext = new DefaultHttpContext();
                ifNoneMatchContext.Request.Headers.Add(HeaderNames.IfNoneMatch, StringValues.Empty);
                ifNoneMatchRequest = ifNoneMatchContext.Request;

                var unconditionalContext = new DefaultHttpContext();
                unconditionalRequest = unconditionalContext.Request;
            }

            [Fact]
            public void returns_false_otherwise() {
                unconditionalRequest.IsConditional().ShouldBeFalse();
            }

            [Fact]
            public void returns_true_on_if_match() {
                ifMatchRequest.IsConditional().ShouldBeTrue();
            }

            [Fact]
            public void returns_true_on_if_none_match() {
                ifNoneMatchRequest.IsConditional().ShouldBeTrue();
            }
        }
    }
}
