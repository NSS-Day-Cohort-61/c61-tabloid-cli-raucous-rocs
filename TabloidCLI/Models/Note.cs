using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.Models
{
    internal class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Post Post { get; set; }

        public string NoteInfo
        {
            get
            {
                return
                    @$"Note Title - {Title} 
                    Date Created - {CreateDateTime} 
                    Note Content - {Content}";
            }
        }

        public override string ToString()
        {
            return NoteInfo;
        }
    }

    
}
