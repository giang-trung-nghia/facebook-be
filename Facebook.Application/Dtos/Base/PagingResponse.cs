using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Base
{
    public class PagingResponse<T>
    {
        public List<T> data {  get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
    }
}
