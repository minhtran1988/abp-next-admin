using Microsoft.AspNetCore.Mvc;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LINGYUN.Abp.WeChat.Official.Senparc;

[Area("wechat-official")]
[Route("api/wechat/official")]
public class WeChatOfficialController : AbpControllerBase
{
    private readonly AbpWeChatOfficialOptionsFactory _optionsFactory;

    public WeChatOfficialController(AbpWeChatOfficialOptionsFactory optionsFactory)
    {
        _optionsFactory = optionsFactory;
    }



    /// <summary>
    /// WeChat backend verification address (use Get), fill in the Url of "Interface Configuration Information" of WeChat backend like: http://weixin.senparc.com/weixin
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(PostModel postModel, string echostr)
    {
        var options = await _optionsFactory.CreateAsync();
        if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, options.Token))
        {
            return Content(echostr); //Returning a random string indicates that the verification is passed
        }
        else
        {
            return Content(
                "failed:" + postModel.Signature + "," + 
                CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, options.Token) + "。" +
                "If you see this sentence in the browser, it means that this address can be used as the Url in the backend of the WeChat public account. Please pay attention to keeping the Token consistent.");
        }
    }

    /// <summary>
    /// After the user sends a message, the WeChat platform automatically posts a request here and waits for the response XML.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post(PostModel postModel)
    {
        var options = await _optionsFactory.CreateAsync();
        if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, options.Token))
        {
            return Content("Parameter error!");
        }

        postModel.Token = options.Token;//Remain consistent according to your own background settings
        postModel.EncodingAESKey = options.EncodingAESKey;//Stay consistent according to your own background settings
        postModel.AppId = options.AppId;//Keep it consistent according to your own background settings

        // Customized MessageHandler, detailed judgment operations on WeChat requests are all here.
        var messageHandler = new WeChatOfficialMessageHandler(Request.Body, postModel);//Receive messages

        #region Set message deduplication settings + Prioritize calling synchronous and asynchronous method settings

        /* If you need to add message deduplication function, just turn on the OmitRepeatedMessage function and the SDK will automatically handle it.
         * Receiving duplicate messages is usually because the WeChat server does not receive a response in time and will continue to send 2-5 RequestMessages with the same content */
        messageHandler.OmitRepeatedMessage = true;//It is already enabled by default. This is only for demonstration. It can also be set to false to disable this function in this request.

        // When the synchronous method is overridden and the asynchronous method is not overridden, try to call the synchronous method
        messageHandler.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod;

        #endregion

        messageHandler.SaveRequestMessageLog();//Record Request log (optional)

        await messageHandler.ExecuteAsync(HttpContext.RequestAborted);//Execute WeChat processing (key, second step)

        messageHandler.SaveResponseMessageLog();//Record Response log (optional)

        return Ok();
    }
}
