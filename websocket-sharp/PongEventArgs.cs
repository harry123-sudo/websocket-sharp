using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;


namespace WebSocketSharp
{
    /// <summary>
    /// Represents the event data for the <see cref="WSExtension.OnMessage"/> event.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///   That event occurs when the <see cref="WSExtension"/> receives
    ///   a message or a ping if the <see cref="WSExtension.EmitOnPing"/>
    ///   property is set to <c>true</c>.
    ///   </para>
    ///   <para>
    ///   If you would like to get the message data, you should access
    ///   the <see cref="Data"/> or <see cref="RawData"/> property.
    ///   </para>
    /// </remarks>
    public class PongEventArgs : EventArgs
    {
        #region Private Fields

        private string _data;
        private bool _dataSet;
        private Opcode _opcode;
        private byte[] _rawData;

        #endregion

        #region Internal Constructors

        internal PongEventArgs(WebSocketFrame frame)
        {
            _opcode = frame.Opcode;
            _rawData = frame.PayloadData.ApplicationData;
        }

        #endregion

        #region Internal Properties


        internal Opcode Opcode
        {
            get
            {
                return _opcode;
            }
        }

        #endregion

        #region Public Properties

        public string Data
        {
            get
            {
                setData();
                return _data;
            }
        }

        public bool IsBinary
        {
            get
            {
                return _opcode == Opcode.Binary;
            }
        }

        public bool IsPing
        {
            get
            {
                return _opcode == Opcode.Ping;
            }
        }
        public bool IsPong
        {
            get
            {
                return _opcode == Opcode.Pong;
            }
        }
        public bool IsText
        {
            get
            {
                return _opcode == Opcode.Text;
            }
        }
        public byte[] RawData
        {
            get
            {
                setData();
                return _rawData;
            }
        }

        #endregion

        #region Private Methods

        private void setData()
        {
            if (_dataSet)
                return;

            if (_opcode == Opcode.Binary)
            {
                _dataSet = true;
                return;
            }

            if (_opcode == Opcode.Pong)
            {
                //_dataSet = true;
                string data;
                if (_rawData.TryGetUTF8DecodedString(out data))
                    _data = data;

                _dataSet = true;

                return;
            }
        }
        #endregion
    }
}