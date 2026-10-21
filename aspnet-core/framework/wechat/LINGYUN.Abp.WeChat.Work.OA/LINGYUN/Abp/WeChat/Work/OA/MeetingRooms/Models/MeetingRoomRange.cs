using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Meeting room usage range
/// </summary>
public class MeetingRoomRange
{
    /// <summary>
    /// Userid list of conference room usage range
    /// </summary>
    [NotNull]
    [JsonProperty("user_list")]
    [JsonPropertyName("user_list")]
    public string[]? UserList { get; }
    /// <summary>
    /// Department ID list of conference room usage range
    /// </summary>
    [NotNull]
    [JsonProperty("department_list")]
    [JsonPropertyName("department_list")]
    public int[]? DepartmentList { get; }
    public MeetingRoomRange(string[]? userList = null, int[]? departmentList = null)
    {
        UserList = userList;
        DepartmentList = departmentList;
    }
}
