//Author: Nathan Li
//Create Time: Tuesday, 21 January 2020

using System;

namespace auu.log
{
    public class LogEntity
    {
        public string Id { get; set; }
        public LogLevel Level { get; set; }
        public string LogTitle { get; set; }
        public string LogType { get; set; }
        public string Source { get; set; }
        public DateTime LogDateTime { get; set; }
        public string User { get; set; }
        public string ClientMachine { get; set; }
        public string DetailContent { get; set; }
        public string ReferenceData { get; set; }
    }
}