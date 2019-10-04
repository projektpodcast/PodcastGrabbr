﻿using CommonTypes;
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
        public static IDataTarget CreateFileTarget(int targetType)
        {
            IDataTarget dataTargetInstance = null; ;
            switch (targetType)
            {
                case 1:
                    dataTargetInstance = new XmlDataTarget();
                    break;
                case 2:
                    dataTargetInstance = null;
                    break;
                case 3:
                    dataTargetInstance = null;
                    break;
                default:
                    dataTargetInstance = null;
                    break;
            }
            return dataTargetInstance;
        }

        public static IDataSource CreateFileSource(int sourceType)
        {
            IDataSource dataSourceInstance = null; ;
            switch (sourceType)
            {
                case 1:
                    dataSourceInstance = new XmlDataSource();
                    break;
                case 2:
                    dataSourceInstance = null;
                    break;
                case 3:
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
