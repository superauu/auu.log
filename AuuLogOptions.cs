//Author: Nathan Li
//Create Time: Tuesday, 21 January 2020

using System;

namespace auu.log
{
    public class AuuLogOptions
    {
        public string LogPath { get; set; } = "log";
        public IDbLogger DbLogger { get; set; }
        public string LogTableName { get; set; } = "auu_log";
        public long KeepLogNumber { get; set; } = 1000000;
        public DateTime KeepLogAfter { get; set; } = DateTime.Now.AddYears(-3);
    }
}