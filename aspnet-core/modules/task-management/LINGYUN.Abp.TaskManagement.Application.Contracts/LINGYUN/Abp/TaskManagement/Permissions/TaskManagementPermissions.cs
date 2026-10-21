namespace LINGYUN.Abp.TaskManagement.Permissions;

public static class TaskManagementPermissions
{
    public const string GroupName = "TaskManagement";

    public static class BackgroundJobs
    {
        public const string Default = GroupName + ".BackgroundJobs";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Trigger = Default + ".Trigger";
        public const string Pause = Default + ".Pause";
        public const string Resume = Default + ".Resume";
        public const string Start = Default + ".Start";
        public const string Stop = Default + ".Stop";
        /// <summary>
        /// Whether to allow built-in jobs in the management system
        /// Usually built-in jobs are generated during program running and need to control user behavior.
        /// </summary>
        public const string ManageSystemJobs = Default + ".ManageSystemJobs";
        /// <summary>
        /// Manage job behavior
        /// </summary>
        public const string ManageActions = Default + ".ManageActions";
    }

    public static class BackgroundJobLogs
    {
        public const string Default = GroupName + ".BackgroundJobLogs";
        public const string Delete = Default + ".Delete";
    }
}
