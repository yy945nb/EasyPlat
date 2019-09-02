using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyPlat.Extends
{
    public class CommonHelper
    {

        /// <summary>  
        /// 泛型集合转DataTable  
        /// </summary>  
        /// <typeparam name="T">集合类型</typeparam>  
        /// <param name="entityList">泛型集合</param>  
        /// <returns>DataTable</returns>  
        public static DataTable ListToDt<T>(IList<T> entityList)
        {
            if (entityList == null) return null;
            DataTable dt = CreateTable<T>();
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();  
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in entityList)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyDescriptor property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }
        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();  
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            //生成DataTable的结构  
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                dt.Columns.Add(prop.Name);
            }
            return dt;
        }

        // 从一个对象信息生成Json串  
        public static string ObjectToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        // 从一个Json串生成对象信息  
        public static object JsonToObject(string jsonString, object obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, obj.GetType());
        }

        // 获取时间戳  
        public static string GetTimeStamp()
        {
            //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan ts = DateTime.Today - new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString(); //精确到秒
        }

        /// <summary>
        /// 通过key[int]获取字典中的值
        /// </summary>
        /// <param name="enmuInt"></param>
        /// <param name="enmuDict"></param>
        /// <returns></returns>
        public static string GetEnmuNameByStr(string enmuInt, Dictionary<string, string> enmuDict)
        {
            enmuInt = string.IsNullOrWhiteSpace(enmuInt) ? "0" : enmuInt;
            if (enmuDict.Keys.Contains(enmuInt))
                return enmuDict.FirstOrDefault(o => o.Key == enmuInt).Value;
            else
                return "";
        }
        /// <summary>
        ///通过key[string]获取字典中的值
        /// </summary>
        /// <param name="enmuInt"></param>
        /// <param name="enmuDict"></param>
        /// <returns></returns>
        public static string GetEnmuNameByInt(int enmuInt, Dictionary<int, string> enmuDict)
        {
            if (enmuDict.Keys.Contains(enmuInt))
                return enmuDict.FirstOrDefault(o => o.Key == enmuInt).Value;
            else
                return "";
        }

        /// <summary>
        /// 将父类的值复制给子类
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static TChild AutoCopy<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;

        }

        /// <summary>
        /// 相同类，不同命名空间值赋值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="obj1">源数据</param>
        /// <param name="obj2">赋值后数据</param>
        /// <returns></returns>
        public static T CopyObject<T>(object obj1, T obj2) where T : new()
        {
            try
            {
                var type1 = obj1.GetType();
                var type2 = obj2.GetType();

                foreach (var propertyInfo in type2.GetProperties())
                {
                    var name = propertyInfo.Name;
                    //获取 
                    var propertyPkid = type1.GetProperty(name);
                    var value = propertyPkid.GetValue(obj1, null);
                    propertyInfo.SetValue(obj2, value, null);//设置1 
                }

                return obj2;
            }
            catch (Exception)
            {
                return new T();
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            return MD5Encrypt(input, new UTF8Encoding()).ToUpper();
        }

        /// <summary>
        /// md5加密16|32位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, int length)
        {
            string res = MD5Encrypt(input, new UTF8Encoding());
            if (length == 16)
            {
                res = res.Substring(8, 16);
            }
            return res.ToUpper();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encode)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(encode.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }
    }
}
