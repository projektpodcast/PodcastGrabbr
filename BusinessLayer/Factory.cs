using CommonTypes;
using DataAccessLayer;
using DataAccessLayer.PostgreSQL;
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
        public static IDataTarget CreateDataTarget()
        {
            IDataTarget dataTargetInstance = null; ;
            switch (TargetType)
            {
                case 1:
                    dataTargetInstance = new XmlDataTarget();
                    break;
                case 2:
                    dataTargetInstance = new MySQLDataTarget();
                    break;
                case 3:
                    dataTargetInstance = null; //impl.
                    break;
                default:
                    throw new Exception(); //impl.
            }
            return dataTargetInstance;
        }

        public static IDataSource CreateDataSource()
        {
            IDataSource dataSourceInstance = null; ;
            switch (TargetType)
            {
                case 1:
                    dataSourceInstance = new XmlDataSource();
                    break;
                case 2:
                    dataSourceInstance = new MySQLDataSource();
                    break;
                case 3:
                    dataSourceInstance = new PostDataSource(); //impl.
                    break;
                default:
                    break;
                    ; //impl.
            }
            return dataSourceInstance;
        }
    }
}
