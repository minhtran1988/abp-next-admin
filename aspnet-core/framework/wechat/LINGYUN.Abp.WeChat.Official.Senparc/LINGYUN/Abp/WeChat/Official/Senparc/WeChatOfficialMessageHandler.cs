using Senparc.NeuChar.App.AppStore;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.IO;

namespace LINGYUN.Abp.WeChat.Official.Senparc;

public class WeChatOfficialMessageHandler : MessageHandler<WeChatOfficialMessageContext>
{
    public WeChatOfficialMessageHandler(
        Stream inputStream, 
        PostModel postModel, 
        int maxRecordCount = 0, 
        bool onlyAllowEncryptMessage = false, 
        DeveloperInfo? developerInfo = null,
        IServiceProvider? serviceProvider = null) 
        : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, developerInfo, serviceProvider)
    {
    }

    public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
    {
        var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
        responseMessage.Content = $"This message comes from DefaultResponseMessage.\r\nYou received this message, indicating that the official account did not process the [{requestMessage.MsgType}] type.";
        return responseMessage;
    }
}
