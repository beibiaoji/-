using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;

namespace GameServer.Server
{
    class Message
    {
        private byte[] _data = new byte[1024];
        private int _startIndex = 0;

        public byte[] Data
        {
            get { return _data; }
        }

        public int StartIndex
        {
            get { return _startIndex; }
        }

        public int RemainSize
        {
            get { return _data.Length - _startIndex; }
        }
        public byte[] GetBytes(string data)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
            int dataLength = dataBytes.Length;
            byte[] lengthBytes = BitConverter.GetBytes(dataLength);
            byte[] newBytes = lengthBytes.Concat(dataBytes).ToArray();
            return newBytes;
        }

        public void AddCount(int count)
        {
            _startIndex += count;
        }

        public void ReadMessage(int amount, Action<RequestCode, ActionCode, string> procssDataCallback)
        {
            if (_startIndex <= 4) return;
            _startIndex += amount;
            int count = BitConverter.ToInt32(_data, 0);
            if ((_startIndex - 4) >= count)
            {

                RequestCode requestCode = (RequestCode)BitConverter.ToInt32(_data, 4);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(_data, 8);
                string s = System.Text.Encoding.UTF8.GetString(_data, 12, count - 8);
                procssDataCallback(requestCode, actionCode, s);
                Array.Copy(_data, count + 4, _data, 0, _startIndex - 4 - count);
                _startIndex -= (count + 4);
            }
            else
            {
                return;
            }
        }
        public void ReadMessage(int amount, Action<RequestCode, string> procssDataCallback)
        {
            if (_startIndex <= 4) return;
            _startIndex += amount;
            int count = BitConverter.ToInt32(_data, 0);
            if ((_startIndex - 4) >= count)
            {

                RequestCode requestCode = (RequestCode)BitConverter.ToInt32(_data, 4);
                
                string s = System.Text.Encoding.UTF8.GetString(_data, 8, count - 4);
                procssDataCallback(requestCode, s);
                Array.Copy(_data, count + 4, _data, 0, _startIndex - 4 - count);
                _startIndex -= (count + 4);
            }
            else
            {
                return;
            }
        }

        public static byte[] PackData(RequestCode requestdCode, string data)
        {
            byte[] requestBytes = BitConverter.GetBytes((int)requestdCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestBytes).Concat(dataBytes).ToArray();
        }
        public static byte[] PackData(RequestCode requestdCode,ActionCode actionCode, string data)
        {
            byte[] requestBytes = BitConverter.GetBytes((int)requestdCode);
            byte[] actionBytes = BitConverter.GetBytes((int) actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestBytes.Length + dataBytes.Length+actionBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestBytes).Concat(actionBytes).Concat(dataBytes).ToArray();
        }
    }
}
