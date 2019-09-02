using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EasyPlat.Extends
{
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <typeparam name="V">枚举值类型</typeparam>
    public class EnumHelper<T, V>
        where T : struct
        where V : struct
    {
        private static IDictionary<T, string> enumAndDescriptionCache;
        private static IDictionary<V, string> valueAndDescriptionCache;

        static EnumHelper()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("数据类型必须为枚举类型！");
            var enumNames = Enum.GetNames(type);
            var enumValues = Enum.GetValues(type) as V[];
            var enums = Enum.GetValues(type) as T[];
            var len = enumNames.Length;
            enumAndDescriptionCache = new Dictionary<T, string>(len);
            valueAndDescriptionCache = new Dictionary<V, string>(len);
            for (int i = 0; i < len; i++)
            {
                var tempAttr = GetDescriptionAttr(type.GetField(enumNames[i]));
                var temp = tempAttr == null ? string.Empty : tempAttr.Description;
                enumAndDescriptionCache.Add(enums[i], temp);
                valueAndDescriptionCache.Add(enumValues[i], temp);
            }
        }

        /// <summary>
        /// 获取枚举类型描述信息
        /// </summary>
        /// <param name="field">字段</param>
        /// <returns></returns>
        private static EnumDescriptionAttribute GetDescriptionAttr(FieldInfo field)
        {
            var attrs =
                field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false) as EnumDescriptionAttribute[];
            if (attrs != null && attrs.Length > 0)
                return attrs[0];
            return null;
        }

        /// <summary>
        /// 通过枚举类型获取描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(T value)
        {
            string description = null;
            if (enumAndDescriptionCache.ContainsKey(value))
                enumAndDescriptionCache.TryGetValue(value, out description);
            return description;
        }

        /// <summary>
        /// 通过枚举值类型获取描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionByValue(V value)
        {
            string description = null;
            if (valueAndDescriptionCache.ContainsKey(value))
                valueAndDescriptionCache.TryGetValue(value, out description);
            return description;
        }

        /// <summary>
        /// 返回所有枚举类型及描述
        /// </summary>
        public static IEnumerable<KeyValuePair<T, string>> EnumDescriptions
        {
            get { return enumAndDescriptionCache; }
        }

        /// <summary>
        /// 返回所有枚举类型及描述
        /// </summary>
        public static IEnumerable<KeyValuePair<V, string>> ValueDescriptions
        {
            get { return valueAndDescriptionCache; }
        }
    }

    /// <summary>
    /// 自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumDescriptionAttribute : Attribute
    {
        public string Description;
        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
