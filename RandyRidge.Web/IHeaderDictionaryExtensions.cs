using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using RandyRidge.Common;

namespace RandyRidge.Web {
    /// <summary>
    ///     Provides common extensions for <see cref="IHeaderDictionary" />.
    /// </summary>
    public static class IHeaderDictionaryExtensions {
        private const string MultipartPrefix = "multipart/";

        /// <summary>
        ///     Returns whether the request has a multipart content-type header.
        /// </summary>
        /// <param name="headers">
        ///     The headers to search.
        /// </param>
        /// <returns>
        ///     true if the request has a multipart content-type header; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="headers" /> is null.
        /// </exception>
        public static bool HasMultipartContentType(this IHeaderDictionary? headers) {
            headers = Guard.NotNull(headers, nameof(headers));
            return headers.TryGetValue(HeaderNames.ContentType, out var values) && values.Any(v => v.IndexOf(MultipartPrefix, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        /// <summary>
        ///     Returns whether the request has a conditional if-match or if-none-match header.
        /// </summary>
        /// <param name="headers">
        ///     The headers to search.
        /// </param>
        /// <returns>
        ///     true if the request has a conditional if-match or if-none-match header; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="headers" /> is null.
        /// </exception>
        public static bool IsConditionalRequest(this IHeaderDictionary? headers) {
            headers = Guard.NotNull(headers, nameof(headers));
            return headers.ContainsKey(HeaderNames.IfMatch) || headers.ContainsKey(HeaderNames.IfNoneMatch);
        }
    }
}
