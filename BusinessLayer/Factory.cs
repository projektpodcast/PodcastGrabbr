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

        public static IDatenArt DatenHaltung { get; set; }
        public static IDataTarget CreateDataTarget()
        {

            int manfredoDb = 3;

            IDataTarget dataTargetInstance = null; ;
            switch (manfredoDb)
            {
                case 1:
                    dataTargetInstance = new XmlDataTarget();
                    break;
                case 2:
                    dataTargetInstance = new MySQLDataTarget();
                    break;
                case 3:
                    dataTargetInstance = new PostDataTarget();
                    break;
                default:
                    throw new Exception(); //impl.
            }
            return dataTargetInstance;
        }

        public static IDataSource CreateDataSource()
        {
            int manfredoDb = 3;

            IDataSource dataSourceInstance = null; ;
            switch (manfredoDb)
            {
                case 1:
                    dataSourceInstance = new XmlDataSource();
                    break;
                case 2:
                    dataSourceInstance = new MySQLDataSource();
                    break;
                case 3:
                    dataSourceInstance = new PostDataSource();
                    break;
                default:
                    throw new Exception(); //impl.
            }
            return dataSourceInstance;
        }
    }
}
