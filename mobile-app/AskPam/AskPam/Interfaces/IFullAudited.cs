using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Interfaces
{
    interface IFullAudited : IAudited, ICreationAudited, IModificationAudited, IDeletionAudited
    {
    }

    public interface IAudited : ICreationAudited, IModificationAudited
    {
    }

    public interface ICreationAudited
    {
        DateTime? CreationTime { get; set; }
        string CreatedUserId { get; set; }
    }

    public interface IModificationAudited
    {
        DateTime? LastModificationTime { get; set; }
        string LastModifiedUserId { get; set; }
    }

    public interface IDeletionAudited
    {
        DateTime? DeletionTime { get; set; }
        string DeletedUserId { get; set; }
    }
}
