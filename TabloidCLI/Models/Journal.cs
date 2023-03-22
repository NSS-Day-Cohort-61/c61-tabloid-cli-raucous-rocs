using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }


        public string JournalInfo
        {
            get
            {
                return
                    @$"Journal Title - {Title} 
                    Date Created - {CreateDateTime} 
                    Journal Content - {Content}";
            }
        }

        public override string ToString()
        {
            return JournalInfo;
        }
    }
}