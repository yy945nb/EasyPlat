using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using EasyPlat.Extends;

namespace Concrete.CloudPlat.Web.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public ActionResult FileUpload()
        {
            return View();
        }

        /// <summary>
        /// 上传公司信息所属图片或文件信息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="savepath"></param>
        /// <returns></returns>
        public JsonResult UploadFile(HttpPostedFileBase file, string savepath)
        {
            var rf = new AjaxModel();

            var currentPath = "/Files/TempFiles/";

            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;
            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            string saveUrl = currentPath + savepath + "/";

            var imageList = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            var accessoryName = fileName.Split('.')[0];

            if (imageList.Contains(fileExt))
            {
                rf = SaveUploadFile(file, saveUrl, "image");
            }

            else
            {
                rf = SaveUploadFile(file, saveUrl, "file");
            }

            return Json(rf, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadFileByEditor(string savepath)
        {
            var rf = new AjaxModel();

            var obj = new ReturnObj() { error = 1, url = "" };

            HttpPostedFileBase file = Request.Files["imgFile"];

            var currentPath = "/Files/TempFiles/";

            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;
            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            string saveUrl = currentPath + savepath + "/";

            var imageList = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            var accessoryName = fileName.Split('.')[0];

            if (imageList.Contains(fileExt))
            {
                rf = SaveUploadFile(file, saveUrl, "image");
            }
            else
            {
                rf = SaveUploadFile(file, saveUrl, "file");
            }

            if (rf.Value != null)
            {
                obj.error = 0;
                obj.url = "/Web" + rf.Value.ToString();
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //培训资料上传（压缩图片尺寸100*60）
        public JsonResult UploadThumbnail(string savepath)
        {
            var obj = new ReturnObj() { error = 1, url = "" };

            HttpPostedFileBase file = Request.Files["imgFile"];
            Image resourceImage = Image.FromStream(file.InputStream);

            var currentPath = "/Files/TempFiles/";

            // 文件的绝对路径地址
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;
            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            string saveUrl = currentPath + savepath + "/";

            var imageList = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            var accessoryName = fileName.Split('.')[0];

            if (imageList.Contains(fileExt))
            {
                var compressWidth = 0;
                var compressHeight = 0;

                var primaryWidth = resourceImage.Width;
                var primaryHeight = resourceImage.Height;

                if (primaryWidth <= 100 && primaryHeight <= 60)
                {
                    compressWidth = primaryWidth;
                    compressHeight = primaryHeight;
                }
                else
                {
                    compressWidth = 100;
                    compressHeight = 60;
                }

                var saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff") + fileExt;

                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(compressWidth, compressHeight, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(resourceImage, new Rectangle(0, 0, compressWidth, primaryHeight));

                bitmap.Save(System.AppDomain.CurrentDomain.BaseDirectory + saveUrl + saveFileName, ImageFormat.Jpeg);

                obj.error = 0;
                obj.url = "/Web" + saveUrl + saveFileName;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //附件上传（未压缩）
        public JsonResult UploadThumbnail2(string savepath)
        {
            var obj = new ReturnObj() { error = 1, url = "" };

            HttpPostedFileBase file = Request.Files["imgFile"];
            Image resourceImage = Image.FromStream(file.InputStream);

            var currentPath = "/Files/TempFiles/";

            // 文件的绝对路径地址
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;
            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            string saveUrl = currentPath + savepath + "/";

            var imageList = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            var accessoryName = fileName.Split('.')[0];

            if (imageList.Contains(fileExt))
            {
                var compressWidth = 0;
                var compressHeight = 0;

                var primaryWidth = resourceImage.Width;
                var primaryHeight = resourceImage.Height;

                if (primaryWidth <= 1000 && primaryHeight <= 800)
                {
                    compressWidth = primaryWidth;
                    compressHeight = primaryHeight;
                }
                else
                {
                    compressWidth = 1000;
                    compressHeight = 800;
                }

                var saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff") + fileExt;

                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(compressWidth, compressHeight, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(resourceImage, new Rectangle(0, 0, compressWidth, primaryHeight));

                bitmap.Save(System.AppDomain.CurrentDomain.BaseDirectory + saveUrl + saveFileName, ImageFormat.Jpeg);

                obj.error = 0;
                obj.url = "/Web" + saveUrl + saveFileName;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //消息模块富文本编辑器
        public JsonResult UploadThumbnail3(string savepath)
        {
            var obj = new ReturnObj() { error = 1, url = "" };

            HttpPostedFileBase file = Request.Files["imgFile"];
            Image resourceImage = Image.FromStream(file.InputStream);

            var currentPath = "/Files/EntMgr/EntMsg/";

            // 文件的绝对路径地址
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;
            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            string saveUrl = currentPath + savepath + "/";

            var imageList = new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            var accessoryName = fileName.Split('.')[0];

            if (imageList.Contains(fileExt))
            {
                var compressWidth = 0;
                var compressHeight = 0;

                var primaryWidth = resourceImage.Width;
                var primaryHeight = resourceImage.Height;

                if (primaryWidth <= 1000 && primaryHeight <= 800)
                {
                    compressWidth = primaryWidth;
                    compressHeight = primaryHeight;
                }
                else
                {
                    compressWidth = 1000;
                    compressHeight = 800;
                }

                var saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff") + fileExt;

                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(compressWidth, compressHeight, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(resourceImage, new Rectangle(0, 0, compressWidth, primaryHeight));

                bitmap.Save(System.AppDomain.CurrentDomain.BaseDirectory + saveUrl + saveFileName, ImageFormat.Jpeg);

                obj.error = 0;
                string url = AppDomain.CurrentDomain.BaseDirectory;
                string[] array = url.Split('/');
                int lenth = array.Length;
                obj.url ="/"+ array[lenth-1] + saveUrl + saveFileName;
                //obj.url = System.Configuration.ConfigurationManager.AppSettings["EditAddress"] + saveUrl + saveFileName;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通用文件上传方法
        /// </summary>
        /// HttpPostedFileBase file
        /// <param name="file"></param>
        /// <param name="savepath"></param>
        /// <returns></returns>
        public JsonResult UploadCommonFile(string savepath)
        {
            var rf = new AjaxModel();

            var currentPath = "/Files/TrainMgr/";
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;

            HttpPostedFileBase file = Request.Files["file"];

            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            var saveUrl = currentPath + savepath + "/";
            rf = SaveUploadFile(file, saveUrl, "file");

            return Json(rf, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 消息模块附件上传方法
        /// </summary>
        /// HttpPostedFileBase file
        /// <param name="file"></param>
        /// <param name="savepath"></param>
        /// <returns></returns>
        public JsonResult UploadCommonFiles(string savepath)
        {
            var rf = new AjaxModel();

            var currentPath = "/Files/EntMgr/EntMsg/";
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + currentPath + savepath;

            HttpPostedFileBase file = Request.Files["file"];

            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            var saveUrl = currentPath + savepath + "/";
            rf = SaveUploadFiles(file, saveUrl, "file");

            return Json(rf, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 通用附件上传方法
        /// </summary>
        /// HttpPostedFileBase file
        /// <param name="file"></param>
        /// <param name="savepath"></param>
        /// <returns></returns>
        public JsonResult UploadCommonFilesNew(string savepath)
        {
            var rf = new AjaxModel();

            var filePath = System.AppDomain.CurrentDomain.BaseDirectory  + savepath;

            HttpPostedFileBase file = Request.Files["file"];

            if (!DirectoryHelper.IsExistDirectory(filePath))
            {
                DirectoryHelper.CreateDirectory(filePath);
            }

            var saveUrl =  savepath + "/";
            rf = SaveUploadFiles(file, saveUrl, "file");

            return Json(rf, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public JsonResult DeleteFile(string filePath)
        {
            var result = true;

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var fileUrl = System.AppDomain.CurrentDomain.BaseDirectory + filePath;

                    FileHelper.FileDel(fileUrl);
                }
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="saveUrl">保存路径</param>
        /// <param name="fileType">文件类型[image,flash,media,file]</param>
        /// <returns></returns>
        private AjaxModel SaveUploadFile(HttpPostedFileBase file, string saveUrl, string fileType)
        {
            var rf = new AjaxModel();

            //前置
            if (file == null)
            {
                rf.Status = 0;
                rf.Message = "请选择文件。";
                return rf;
            }

            if (string.IsNullOrEmpty(fileType))
            {
                rf.Status = 0;
                rf.Message = "文件类型不能为空。";
                return rf;
            }

            //文件保存目录路径
            string savePath = System.AppDomain.CurrentDomain.BaseDirectory + saveUrl;

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();

            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,pdf");

            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            string saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff") + fileExt;

            //判断文件的扩展名
            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[fileType]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                rf.Status = 0;
                rf.Message = "上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[fileType]) + "格式。";
                return rf;
            }

            file.SaveAs(savePath + saveFileName);

            rf.Status = 1;
            rf.Message = "上传成功。";
            rf.Value = saveUrl + saveFileName;

            return rf;
        }


        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="saveUrl">保存路径</param>
        /// <param name="fileType">文件类型[image,flash,media,file]</param>
        /// <returns></returns>
        private AjaxModel SaveUploadFiles(HttpPostedFileBase file, string saveUrl, string fileType)
        {
            var rf = new AjaxModel();

            //前置
            if (file == null)
            {
                rf.Status = 0;
                rf.Message = "请选择文件。";
                return rf;
            }

            if (string.IsNullOrEmpty(fileType))
            {
                rf.Status = 0;
                rf.Message = "文件类型不能为空。";
                return rf;
            }

            //文件保存目录路径
            string savePath = System.AppDomain.CurrentDomain.BaseDirectory + saveUrl;

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();

            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            //'jpg', 'jpeg', 'png', 'bmp', 'doc', 'docx', 'pdf', 'ppt', 'pptx', 'txt', 'xls', 'xlsx', 'zip', 'rar
            extTable.Add("file", "jpg,jpeg,png,bmp,doc,docx,xls,xlsx,pdf,ppt,pptx,txt,zip,rar,gz");

            string fileName = file.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
            string saveFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff_") + fileName;

            //判断文件的扩展名
            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[fileType]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                rf.Status = 0;
                rf.Message = "上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[fileType]) + "格式。";
                return rf;
            }

            file.SaveAs(savePath + saveFileName);

            rf.Status = 1;
            rf.Message = "上传成功。";
            rf.Value = saveUrl + saveFileName;

            return rf;
        }


       
    }

    public class AjaxModel
    {
        public object Value { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class ReturnObj
    {
        public int error { get; set; }
        public string url { get; set; }
    }
}