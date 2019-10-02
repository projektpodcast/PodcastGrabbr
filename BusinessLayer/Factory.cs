using CommonTypes;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class Factory
    {
        public static Podcast CreateSerializer()
        {
            return new Podcast();
        }

        public static IDataTarget CreateFileTarget()
        {
            return new FileDataTarget();
        }

        public static FileDataSource CreateFileSource()
        {
            return new FileDataSource();
        }
    }
}
