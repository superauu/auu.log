//Author: Nathan Li
//Create Time: Tuesday, 21 January 2020

using System;
using System.Collections.Generic;

namespace auu.log
{
    public interface IDbLogger
    {
        void SaveLog(LogEntity log);
        IList<LogEntity> GetLogTitle(int last);
        LogEntity GetLog(string id);

        void DeleteLogs(long keep);
        void DeleteLogsBefore(DateTime dt);
        void CreateTable(string tableName);
    }
}