using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using RandyRidge.Common;

namespace RandyRidge.Web {
	/// <summary>
	///     Provides handy extensions for <see cref="HttpRequest" />.
	/// </summary>
	public static class HttpRequestExtensions {
		/// <summary>
		///     Determines whether the request is a conditional request (if-match, if-none-match).
		/// </summary>
		/// <param name="request">
		///     The request to interrogate.
		/// </param>
		/// <returns>
		///     <c>true</c> if the request is conditional; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsConditional(this HttpRequest request) {
			request = Guard.NotNull(request, nameof(request));
			var requestHeaders = request.Headers;
			return requestHeaders.ContainsKey(HeaderNames.IfNoneMatch) || requestHeaders.ContainsKey(HeaderNames.IfMatch);
		}
	}
}
