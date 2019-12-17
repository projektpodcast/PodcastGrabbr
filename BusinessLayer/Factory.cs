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
    /// <summary>
    /// Die Factory erstellt für die Klassen "GetObjects" und "SaveObjects" Instanzen, um den DataAccessLayer aufzurufen.
    /// 
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Der Singleton stellt sicher, dass es für jede Klasse des DatenAccessLayers nur eine aktive Instanz gibt.
        /// </summary>
        #region Singleton
        private static readonly Factory instance = new Factory();

        static Factory()
        {

        }

        private Factory()
        {
            _mediaSource = new MediaDataSource();
            _mediaTarget = new MediaDataTarget();
        }

        public static Factory Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion Singleton

        /// <summary>
        /// Die Property DatenHaltung wird von der Klasse "PresentationLayer.Services.UserConfigurationService" gesetzt.
        /// Wenn sich die Verbindungsinformationen ändern, muss eine neue Instanz erstellt werden (geregelt durch SetStorageInstances im Setter())
        /// </summary>
        private IDataStorageType _datenHaltung { get; set; }
        public IDataStorageType DatenHaltung
        {
            private get { return _datenHaltung; }
            set
            {
                _datenHaltung = value;
                if (value.DataType.Key != 0)
                {
                    SetStorageInstances();
                }
            }
        }

        /// <summary>
        /// Die Properties enthalten die geöffneten Instanzen in den DataAccessLayer
        /// </summary>
        #region Instanzen-Properties
        private readonly ILocalMediaSource _mediaSource;
        private readonly ILocalMediaTarget _mediaTarget;
        private IDataSource _dataSource { get; set; }
        private IDataTarget _dataTarget { get; set; }
        #endregion
        /// <summary>
        /// Es gibt drei verschiedene Datenziele & -quellen
        /// Der Key des Dictionary entscheidet welche Art des DataAccessLayers initialisiert werden soll.
        /// </summary>
        private void SetStorageInstances()
        {
            switch (DatenHaltung.DataType.Key)
            {
                case 1:
                    _dataTarget = new XmlAsDataTarget();
                    _dataSource = new XmlAsDataSource();
                    break;
                case 2:
                    _dataTarget = new MySQLDataTarget();
                    _dataSource = new MySQLDataSource();
                    break;
                case 3:
                    _dataTarget = new PostDataTarget();
                    _dataSource = new PostDataSource();
                    break;
                default:
                    break;
                    //throw new Exception(); //impl.
            }
        }

        internal IDataTarget CreateDataTarget()
        {
            return _dataTarget;
        }

        internal IDataSource CreateDataSource()
        {
            return _dataSource;
        }

        internal ILocalMediaSource CreateLocalMediaSource()
        {
            return _mediaSource;
        }

        internal ILocalMediaTarget CreateLocalMediaTarget()
        {
            return _mediaTarget;
        }
    }
}
