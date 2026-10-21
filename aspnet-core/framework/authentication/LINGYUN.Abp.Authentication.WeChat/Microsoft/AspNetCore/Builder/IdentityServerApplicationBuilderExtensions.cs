using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    public static class IdentityServerApplicationBuilderExtensions
    {
        /// <summary>
        /// Enable middleware to process WeChat server messages
        /// Used to verify whether the message comes from the WeChat server
        /// </summary>
        /// <param name="builder"></param>
        /// <remarks>
        /// It can also be implemented in the form of Controller
        /// </remarks>
        /// <returns></returns>
        public static IApplicationBuilder UseWeChatSignature(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<WeChatOfficialSignatureMiddleware>();
            return builder;
        }
    }
}
