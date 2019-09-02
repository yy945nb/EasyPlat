using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyPlat.Dto
{
    public class AjaxDataResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 值对象
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int count { get; set; }
    }
}