using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Shouldly;
using Xunit;

namespace RandyRidge.Web {
    public static class IHeaderDictionaryExtensionsTester {
        public static class HasMultipartContentType {
            [Fact]
            public static void returns_false_otherwise() => new HeaderDictionary().HasMultipartContentType().ShouldBeFalse();

            [Fact]
            public static void returns_true_with_if_none_match_header() => new HeaderDictionary {
                {HeaderNames.ContentType, "multipart/form-data"}
            }.HasMultipartContentType().ShouldBeTrue();

            [Fact]
            public static void throws_on_null_header_dictionary() => Should.Throw<ArgumentNullException>(() => ((IHeaderDictionary) null!).HasMultipartContentType());
        }

        public static class IsConditionalRequest {
            [Fact]
            public static void returns_false_otherwise() => new HeaderDictionary().IsConditionalRequest().ShouldBeFalse();

            [Fact]
            public static void returns_true_with_if_match_header() => new HeaderDictionary {
                {HeaderNames.IfMatch, "any"}
            }.IsConditionalRequest().ShouldBeTrue();

            [Fact]
            public static void returns_true_with_if_none_match_header() => new HeaderDictionary {
                {HeaderNames.IfNoneMatch, "any"}
            }.IsConditionalRequest().ShouldBeTrue();

            [Fact]
            public static void throws_on_null_header_dictionary() => Should.Throw<ArgumentNullException>(() => ((IHeaderDictionary) null!).IsConditionalRequest());
        }
    }
}
