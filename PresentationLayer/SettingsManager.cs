using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public static class UserSettingsManager
    {
        private static int _testValue { get; set; }
        public static int TestValue {
            get
            {
                return _testValue; }
            set
            {
                UpdateSettings(value);
                _testValue = value;
            }
        }


        public static bool IsDataTypeSet()
        {
            TestValue = Properties.Settings.Default.DataType;
            if (TestValue == 0)
            {
                return false;
            }
            return true;
        }

        //public static void SetTestValue()
        //{
        //    TestValue = Properties.Settings.Default.DataType;
        //}

        private static void UpdateSettings(int newValue)
        {
            Properties.Settings.Default.DataType = newValue;
            Properties.Settings.Default.Save();
            Factory.TargetType = newValue;
        }

    }
}
