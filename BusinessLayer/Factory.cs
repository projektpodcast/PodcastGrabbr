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
        public static int TargetType { get; set; }
        public static IDataTarget CreateFileTarget()
        {
            IDataTarget dataTargetInstance = null; ;
            switch (TargetType)
            {
                case 0:
                    dataTargetInstance = new XmlDataTarget();
                    break;
                case 1:
                    dataTargetInstance = null;
                    break;
                case 2:
                    dataTargetInstance = null;
                    break;
                default:
                    dataTargetInstance = null;
                    break;
            }
            return dataTargetInstance;
        }

        public static IDataSource CreateFileSource(int Target)
        {
            IDataSource dataSourceInstance = null; ;
            switch (Target)
            {
                case 0:
                    dataSourceInstance = new XmlDataSource();
                    break;
                case 1:
                    dataSourceInstance = null;
                    break;
                case 2:
                    dataSourceInstance = null;
                    break;
                default:
                    dataSourceInstance = null;
                    break;
            }
            return dataSourceInstance;
        }
    }
}
