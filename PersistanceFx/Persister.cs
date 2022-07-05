using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceFx
{
    public enum PersistanceType
    {
        XML,JSON,BINARY,CSV,RSS
    }

    public enum PropertyTye
    {
        Attribute,
        Element
    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class TargetPersistaneTypeAttribute:System.Attribute
    {
        public PersistanceType format;
        public TargetPersistaneTypeAttribute(PersistanceType format)
        {
            this.format = format;
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TargetPropertyTypeAttribute : System.Attribute
    {
        public PropertyTye format;
        public TargetPropertyTypeAttribute(PropertyTye format)
        {
            this.format = format;
        }

    }

    internal class XMLPersister
    {
        public void WriteObject(object source)
        {
            //Property List (public)
            // How to transform each Property (xml attribute ,xml element)

            var properties = source.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var targetPropertyTypeAttribute = properties[i].GetType().GetCustomAttributes(typeof(TargetPropertyTypeAttribute), true).FirstOrDefault() as TargetPropertyTypeAttribute;
                PropertyTye _propertyFormat = targetPropertyTypeAttribute.format;
                Console.WriteLine($"Target format is : {_propertyFormat} ");

                switch (_propertyFormat)
                {
                    case PropertyTye.Element:
                        //..Handle
                        break;
                    case PropertyTye.Attribute:
                        //..Handle
                        break;
                } 
            }
        }
    }
    public class Persister
    {
        
        public bool Persist(object source)
        {
            var targetTypeAttribute = source.GetType().GetCustomAttributes(typeof(TargetPersistaneTypeAttribute), true).FirstOrDefault() as TargetPersistaneTypeAttribute;
            PersistanceType _targetFormat = targetTypeAttribute.format;
            Console.WriteLine($"Target format is : {_targetFormat} ");
            switch (_targetFormat)
            {
                case PersistanceType.XML: XMLPersister _persister = new XMLPersister();
                                          _persister.WriteObject(source);
                                           break;
            }

            return false;

        }
    }
}
