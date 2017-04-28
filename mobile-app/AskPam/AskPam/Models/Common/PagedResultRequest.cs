using AskPam.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Models.Common
{
    public class PagedResultRequest : IPagedResultRequest, ILimitedResultRequest
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public PagedResultRequest()
        {

        }
    }
}
