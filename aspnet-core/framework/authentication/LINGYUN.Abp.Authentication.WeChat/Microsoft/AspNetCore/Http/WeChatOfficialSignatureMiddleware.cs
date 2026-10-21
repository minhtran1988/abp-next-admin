using LINGYUN.Abp.WeChat.Official;
using System;
using System.Collections;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Microsoft.AspNetCore.Http
{
    public class WeChatOfficialSignatureMiddleware : IMiddleware, ITransientDependency
    {
        protected AbpWeChatOfficialOptionsFactory WeChatOfficialOptionsFactory { get; }
        public WeChatOfficialSignatureMiddleware(
            AbpWeChatOfficialOptionsFactory weChatOfficialOptionsFactory)
        {
            WeChatOfficialOptionsFactory = weChatOfficialOptionsFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.HasValue)
            {
                var options = await WeChatOfficialOptionsFactory.CreateAsync();

                var requestPath = context.Request.Path.Value;
                // Whether the access address matches the defined address
                if (requestPath.Equals(options.Url))
                {
                    var timestamp = context.Request.Query["timestamp"];
                    var nonce = context.Request.Query["nonce"];
                    var signature = context.Request.Query["signature"];
                    var echostr = context.Request.Query["echostr"];
                    // Verify the validity of the message
                    var check = CheckWeChatSignature(options.Token, timestamp, nonce, signature);
                    if (check)
                    {
                        // If the verification passes, the characters passed by the WeChat server need to be returned intact.
                        await context.Response.WriteAsync(echostr);
                        return;
                    }
                    // WeChat message verification fails
                    throw new AbpException("Invalid wechat signature");
                }
            }
            // Messages that do not belong to WeChat enter the next middleware
            await next(context);
        }

        protected bool CheckWeChatSignature(string token, string timestamp, string nonce, string signature)
        {
            var al = new ArrayList
            {
                token,
                timestamp,
                nonce
            };
            // step1 sort
            al.Sort();
            string signatureStr = string.Empty;
            // step2 splicing
            for (int i = 0; i < al.Count; i++)
            {
                signatureStr += al[i];
            }
            // step3 SHA1 encryption
            byte[] bytes_out = signatureStr.Sha1();
            string result = BitConverter.ToString(bytes_out).Replace("-", "");
            // step4 comparison
            if (result.Equals(signature, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
