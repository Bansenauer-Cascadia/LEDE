using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Entities;

namespace LEDE.WebUI.DTOs
{
    public class EntryDTO
    {
        public int NumEntries;

        public int VersID;

        public EntryDTO(ReadingLogEntry Entry)
        {
            this.NumEntries = Entry.NumEntries;
            this.VersID = Entry.TaskVersion.VersID; 
        }
    }
}