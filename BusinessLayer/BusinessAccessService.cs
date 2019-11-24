using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessAccess : IBusinessAccessService
    {
        public GetObjects Get { get; set; }
        public SaveObjects Save { get; set; }

        public BusinessAccess()
        {
            Get = new GetObjects();
            Save = new SaveObjects();
        }
    }
}
