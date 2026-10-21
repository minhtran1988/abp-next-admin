namespace LINGYUN.Abp.TaskManagement
{
    public static class TaskManagementErrorCodes
    {
        public const string Namespace = "TaskManagement";
        /// <summary>
        /// There is already a job named {Name} in group {Group}!
        /// </summary>
        public const string JobNameAlreadyExists = Namespace + ":01000";
        /// <summary>
        /// The queue did not find the job named {Name} in group {Group}, please add the job to the queue first!
        /// </summary>
        public const string JobNotFoundInQueue = Namespace + ":01001";
        /// <summary>
        /// Only allowed to delete stopped jobs
        /// </summary>
        public const string OnlyDeletionOfStopJobsIsAllowed = Namespace + ":01002";
        /// <summary>
        /// Invalid CRON expression: {Cron}!
        /// </summary>
        public const string InvalidCronExpression = Namespace + ":01003";
    }
}
