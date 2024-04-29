using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLs.Application.Queries
{
    public class QueryResult<T>
    {
        public QueryResult(int pageNumber, int pageSize, List<T>? results)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = results.Count();
            Results = results.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<T>? Results { get; set; }
    }
}
