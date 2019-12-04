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
        public static IDatenArt DatenHaltung { private get; set; }
        internal static IDataTarget CreateDataTarget()
        {
            IDataTarget dataTargetInstance = null;
            switch (DatenHaltung.DataType.Key)
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

        internal static IDataSource CreateDataSource()
        {
            IDataSource dataSourceInstance = null;
            switch (DatenHaltung.DataType.Key)
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
                case 4:
                    dataSourceInstance = new MockDataSource();
                    break;
                default:
                    throw new Exception(); //impl.
            }
            return dataSourceInstance;
        }
    }
}
