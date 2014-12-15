using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Entities;

namespace LEDE.WebUI.DTOs
{
    public class HoursDTO
    {
        public int UserID;

        public double NumHours;

        public HoursDTO(InternReflection Reflection)
        {
            this.UserID = Reflection.TaskVersion.UserID;

            this.NumHours = Reflection.NumHrs; 
        }
    }
}