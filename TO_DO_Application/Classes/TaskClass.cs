using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo_Dev
{
    public class TaskClass
    {
        public TaskClass()
        {
            TaskID = 0;
            TaskName = "";
            TaskStatus = "";
            TaskButtonStatus = "";
        }
        public int TaskID { set; get; }
        public string TaskName { set; get; }
        public string TaskStatus { set; get; }
        public string TaskButtonStatus { set; get; }
        public int Numbering { set; get; }
    }

    public class ChartData
    {
        public ChartData()
        {
            CompletedRecordCount = 0;
            ActiveRecordCount = 0;
        }
        public int CompletedRecordCount { get; set; }
        public int ActiveRecordCount { get; set; }
    }
}