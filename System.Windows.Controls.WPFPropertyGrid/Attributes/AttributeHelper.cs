﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Windows.Controls.WpfPropertyGrid.Attributes
{
    public enum EncodingType
    {
        UTF8,
        GB2312,
        ASCII,
        UTF16,
        UTF7,
        UTF32,
        BigEndianUTF8,
        Unknown
    }

    internal class AttributeHelper
    {


        public static bool IsBaseType(Type a, Type baseType)
        {
            Type t = a;
            while (t.BaseType != baseType)
            {
                t = t.BaseType;
                if (t == null)
                    return false;
                if (t == typeof (object))
                    return false;
            }
            return true;
        }

        public static IEnumerable<T> GetAttributes<T>(object target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            return GetAttributes<T>(target.GetType());
        }

        public static IEnumerable<T> GetAttributes<T>(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IEnumerable<T> attributes = from T attribute in type.GetCustomAttributes(typeof (T), true) select attribute;

            return attributes;
        }

        /// <summary>
        ///     获取枚举项的Attribute
        /// </summary>
        /// <typeparam name="T">自定义的Attribute</typeparam>
        /// <param name="source">枚举</param>
        /// <returns>返回枚举,否则返回null</returns>
        public static T GetCustomAttribute<T>(Enum source) where T : Attribute
        {
            Type sourceType = source.GetType();
            string sourceName = Enum.GetName(sourceType, source);
            FieldInfo field = sourceType.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof (T), false);
            return attributes.OfType<T>().FirstOrDefault();
        }



        /// <summary>
        ///     获取DescriptionAttribute描述
        /// </summary>
        /// <param name="source">枚举</param>
        /// <returns>有description标记，返回标记描述，否则返回null</returns>
        public static string GetDescription(Enum source)
        {
            var attr = GetCustomAttribute<DescriptionAttribute>(source);
            if (attr == null)
            {
                return null;
            }
            return attr.Description;
        }




    }

}