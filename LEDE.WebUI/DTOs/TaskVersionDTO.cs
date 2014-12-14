using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Entities;

namespace LEDE.WebUI.DTOs
{
    public class TaskVersionDTO
    {
        public int VersID;

        public int SeminarID;

        public string TaskTitle;

        public TaskVersionDTO(TaskVersion Version)
        {
            this.VersID = Version.VersID;
            this.SeminarID = Version.Task.SeminarID;
        }
    }
}