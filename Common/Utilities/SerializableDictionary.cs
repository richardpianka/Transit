using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Transit.Common.Utilities
{
    /// <summary>
    /// Source: http://www.dacris.com/blog/2010/07/31/c-serializable-dictionary-a-working-example/
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableSortedDictionary<TKey, TValue> : SortedDictionary<TKey, TValue>, IXmlSerializable, ISerializable
    {
        private XmlSerializer _keySerializer;
        private XmlSerializer _valueSerializer;

        private const string ItemNodeName = "Item";
        private const string KeyNodeName = "Key";
        private const string ValueNodeName = "Value";

        public SerializableSortedDictionary() { }
        public SerializableSortedDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public SerializableSortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer) : base(dictionary, comparer) { }
        public SerializableSortedDictionary(IComparer<TKey> comparer) : base(comparer) { }

        protected SerializableSortedDictionary(SerializationInfo info, StreamingContext context)
        {
            for (int i = 0; i < info.GetInt32("ItemCount"); i++)
            {
                KeyValuePair<TKey, TValue> kvp = (KeyValuePair<TKey, TValue>)info.GetValue(String.Format("Item{0}", i), typeof(KeyValuePair<TKey, TValue>));
                Add(kvp.Key, kvp.Value);
            }
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ItemCount", Count);
            int index = 0;

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                info.AddValue(String.Format("Item{0}", index), pair, typeof(KeyValuePair<TKey, TValue>));
                index++;
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                writer.WriteStartElement(ItemNodeName);
                writer.WriteStartElement(KeyNodeName);
                KeySerializer.Serialize(writer, pair.Key);
                writer.WriteEndElement();
                writer.WriteStartElement(ValueNodeName);
                ValueSerializer.Serialize(writer, pair.Value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement) return;

            if (!reader.Read())
            {
                throw new XmlException("Error in Deserialization of Dictionary");
            }

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement(ItemNodeName);
                reader.ReadStartElement(KeyNodeName);
                TKey key = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement(ValueNodeName);
                TValue value = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                Add(key, value);
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        protected XmlSerializer ValueSerializer
        {
            get { return _valueSerializer ?? (_valueSerializer = new XmlSerializer(typeof(TValue))); }
        }

        private XmlSerializer KeySerializer
        {
            get { return _keySerializer ?? (_keySerializer = new XmlSerializer(typeof(TKey))); }
        }
    }
}