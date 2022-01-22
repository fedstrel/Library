using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Interop.Dto;

namespace Library.Models
{
    public class ReaderOverlookModel
    {
        public ReaderDto Reader { get; set; }
        public List<string> BookNames { get; set; }
    }
}
