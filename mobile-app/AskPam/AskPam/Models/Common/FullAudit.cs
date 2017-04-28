using AskPam.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Models.Common
{
    public class FullAudited : IFullAudited
    {
        public DateTime? CreationTime { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeletedUserId { get; set; }
    }
}
