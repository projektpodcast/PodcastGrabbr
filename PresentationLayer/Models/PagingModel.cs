using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class PagingModel
    {
        private int _pageCount;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _pageIndex;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }



    }
}
