using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.DTO
{
    public class PageModel
    {
        const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        private int _pageSize { get; set; } = 5;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }   
}
