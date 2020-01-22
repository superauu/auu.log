//Author: Nathan Li
//Create Time: Tuesday, 21 January 2020

using System;

namespace auu.log
{
    public class MaintainLogs
    {
        public void InitDb(AuuLogOptions options)
        {
            options.DbLogger.CreateTable(options.LogTableName);
        }

        public void Maintain(AuuLogOptions options)
        {
            if (options.KeepLogNumber > 0)
            {
                options.DbLogger.DeleteLogs(options.KeepLogNumber);
            }

            if (options.KeepLogAfter > new DateTime())
            {
                options.DbLogger.DeleteLogsBefore(options.KeepLogAfter);
            }
        }
    }
}