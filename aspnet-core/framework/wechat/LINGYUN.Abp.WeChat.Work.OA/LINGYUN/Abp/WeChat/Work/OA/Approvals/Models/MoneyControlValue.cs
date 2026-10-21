using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Amount control value
/// </summary>
public class MoneyControlValue : ControlValue
{
    /// <summary>
    /// Amount content, fill in the input value of the amount control here
    /// </summary>
    [NotNull]
    [JsonProperty("new_money")]
    [JsonPropertyName("new_money")]
    public string NewMoney { get; set; } = default!;
    public MoneyControlValue()
    {

    }

    public MoneyControlValue(decimal newMoney)
    {
        NewMoney = newMoney.ToString();
    }
}
