using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlat.Extends
{
    public class ListConvertToDataTable
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// 字典对象集合转DataTable
        /// </summary>
        /// <param name="dicList"></param>
        /// <returns></returns>
        public static DataTable DicListToTable(List<Dictionary<object, object>> dicList)
        {
            DataTable dt = new DataTable();

            if (dicList.Count == 0)
            {
                return dt;
            }

            foreach (var colName in dicList.First())
            {
                dt.Columns.Add(colName.Key.ToString(), colName.Key.GetType());
            }

            foreach (var dic in dicList)
            {
                DataRow dr = dt.NewRow();

                foreach (var colName in dic)
                {
                    dr[colName.Key.ToString()] = colName.Value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable DicListToTable(List<Dictionary<string, object>> dicList)
        {
            DataTable dt = new DataTable();

            if (dicList.Count == 0)
            {
                return dt;
            }
            var storageList = new List<string>();

            foreach (var item in dicList)
            {
                foreach (var temp in item)
                {
                    if (!storageList.Contains(temp.Key))
                    {
                        storageList.Add(temp.Key);
                    }
                }
            }

            foreach (var item in storageList)
            {
                dt.Columns.Add(item, item.GetType());
            }

            foreach (var dic in dicList)
            {
                DataRow dr = dt.NewRow();

                foreach (var colName in dic)
                {
                    dr[colName.Key] = colName.Value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
