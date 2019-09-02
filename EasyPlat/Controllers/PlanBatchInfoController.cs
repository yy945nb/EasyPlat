using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using EasyPlat.Dto;
using EasyPlat.Extends;
using EasyPlat.Models;
using EasyPlat.QueryModels;
using Newtonsoft.Json;

namespace EasyPlat.Controllers
{
    public class PlanBatchInfoController : Controller
    {
        private EasyPlatContext db = new EasyPlatContext();

        public ActionResult Index()
        {
            return View(db.PlanBatchInfos.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        public ActionResult Import()
        {
            return View();
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Save(PlanBatchInfo model)
        {
            var ajaxData = new AjaxDataResult() { code = 0, msg = "保存失败！" };

            try
            {
                if (model != null)
                {
                    db.PlanBatchInfos.Add(model);
                    var result = db.SaveChanges() > 0;

                    if (result)
                    {
                        ajaxData.code = 1;
                        ajaxData.msg = "保存成功！";
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxData.msg = ex.Message;
            }

            return Json(ajaxData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取页面分页数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPageData()
        {
            var ajaxModel = new AjaxDataResult() { code = 1, msg = "未获取到数据！" };

            var queryModel = new PlanBatchQueryModel
            {
                page = !string.IsNullOrEmpty(Request["page"]) ? Convert.ToInt32(Request["page"]) : 1,
                limit = !string.IsNullOrEmpty(Request["limit"]) ? Convert.ToInt32(Request["limit"]) : 10,

                PlanBatch = Request["PlanBatch"],
                PlanName = Request["PlanName"],
                CityPlace = Request["CityPlace"],
                BidMode = Request["BidMode"],
                BuildWay = Request["BuildWay"],
                BidYear = Request["BidYear"]
            };

            if (!string.IsNullOrEmpty(Request["PlanStartDt"]))
            {
                queryModel.PlanStartDt = Convert.ToDateTime(Request["PlanStartDt"]);
            }
            if (!string.IsNullOrEmpty(Request["PlanEndDt"]))
            {
                queryModel.PlanEndDt = Convert.ToDateTime(Request["PlanEndDt"]);
            }

            var list = db.PlanBatchInfos.Where(m =>
               (string.IsNullOrEmpty(queryModel.PlanBatch) || (!string.IsNullOrEmpty(queryModel.PlanBatch) && m.PlanBatch.Contains(queryModel.PlanBatch)))
               && (string.IsNullOrEmpty(queryModel.PlanName) || (!string.IsNullOrEmpty(queryModel.PlanName) && m.PlanName.Contains(queryModel.PlanName)))
               && (string.IsNullOrEmpty(queryModel.CityPlace) || (!string.IsNullOrEmpty(queryModel.PlanBatch) && m.CityPlace.Contains(queryModel.CityPlace)))
               && (string.IsNullOrEmpty(queryModel.BidMode) || (!string.IsNullOrEmpty(queryModel.PlanBatch) && m.BidMode.Contains(queryModel.BidMode)))
               && (string.IsNullOrEmpty(queryModel.BuildWay) || (!string.IsNullOrEmpty(queryModel.BuildWay) && m.BuyWay.Contains(queryModel.BuildWay)))
               && (string.IsNullOrEmpty(queryModel.BidYear) || (!string.IsNullOrEmpty(queryModel.BidYear) && m.BidYear.Contains(queryModel.BidYear)))
            ).ToList();

            if (queryModel.PlanStartDt.HasValue)
            {
                list = list.Where(m => m.PlanStartDt.Value >= queryModel.PlanStartDt).ToList();
            }
            if (queryModel.PlanEndDt.HasValue)
            {
                list = list.Where(m => m.PlanEndDt.Value < queryModel.PlanEndDt).ToList();
            }
            ajaxModel.count = list.Count;

            list = list.OrderBy(m => m.Phid).Skip(queryModel.page - 1).Take(queryModel.limit).ToList();

            if (list.Any())
            {
                ajaxModel.code = 0;
                ajaxModel.msg = "获取数据成功！";
                ajaxModel.data = list;
            }

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 展示Excel 数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public JsonResult PresentExcelData(string fileName)
        {
            var ajaxData = new AjaxDataResult() { code = 1, msg = "未获取到数据！" };

            try
            {
                //获取分页数据
                var list = GetExcelData(fileName);

                if (list != null)
                {
                    ajaxData.code = 0;
                    ajaxData.msg = "获取数据成功！";
                    ajaxData.data = list;
                    ajaxData.count = list.Count;
                }
            }
            catch (Exception ex)
            {
                ajaxData.msg = ex.Message;
            }

            return Json(ajaxData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="planBatchStr"></param>
        /// <returns></returns>
        public JsonResult BatchSave(string planBatchStr)
        {
            var ajaxModel = new AjaxDataResult() { code = 0, msg = "保存失败！" };

            try
            {
                var list = new List<PlanBatchInfo>();

                if (!string.IsNullOrEmpty(planBatchStr))
                {
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanBatchInfo>>(planBatchStr);

                    if (list.Any())
                    {
                        foreach (var planBatchInfo in list)
                        {
                            var planBatchModel = db.PlanBatchInfos.FirstOrDefault(m => m.Phid == planBatchInfo.Phid);

                            if (planBatchModel != null)
                            {
                                planBatchModel.PlanBatch = planBatchInfo.PlanBatch;
                                planBatchModel.PlanName = planBatchInfo.PlanName;
                                planBatchModel.CityPlace = planBatchInfo.CityPlace;
                                planBatchModel.BidMode = planBatchInfo.BidMode;
                                planBatchModel.BuyWay = planBatchInfo.BuyWay;
                                planBatchModel.BidYear = planBatchInfo.BidYear;
                                planBatchModel.BidFlag = planBatchInfo.BidFlag;
                                planBatchModel.PlanCheckDt = planBatchInfo.PlanCheckDt;
                                planBatchModel.PlanEndDt = planBatchInfo.PlanEndDt;
                                planBatchModel.Status = planBatchInfo.Status;
                                planBatchModel.PlanStartDt = planBatchInfo.PlanStartDt;
                                planBatchModel.CityStartDt = planBatchInfo.CityStartDt;
                                planBatchModel.Period = planBatchInfo.Period;
                            }

                            else
                            {
                                db.PlanBatchInfos.Add(planBatchInfo);
                            }
                        }

                        var result = db.SaveChanges() > 0;

                        if (result)
                        {
                            ajaxModel.code = 1;
                            ajaxModel.msg = "保存成功！";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxModel.msg = ex.Message;
            }

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult BatchDelete(string Ids)
        {
            var ajaxModel = new AjaxDataResult() { code = 0, msg = "删除失败！" };

            try
            {
                if (!string.IsNullOrEmpty(Ids))
                {
                    var IdList = Ids.Split(',').Select(m => Convert.ToInt64(m)).ToList();

                    if (IdList.Any())
                    {
                        foreach (var id in IdList)
                        {
                            var planBatchInfo = db.PlanBatchInfos.Find(id);

                            if (planBatchInfo != null)
                            {
                                db.PlanBatchInfos.Remove(planBatchInfo);
                            }
                        }

                        db.SaveChanges();

                        ajaxModel.msg = "删除成功!";
                        ajaxModel.code = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxModel.msg = ex.Message;
            }

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量导入Excel数据
        /// </summary>
        /// <param name="planBatchStr"></param>
        /// <returns></returns>
        public JsonResult SaveExcelData(string planBatchStr)
        {
            var ajaxModel = new AjaxDataResult() { code = 1, msg = "保存成功！" };

            try
            {
                var list = new List<PlanBatchInfo>();

                if (!string.IsNullOrEmpty(planBatchStr))
                {
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanBatchInfo>>(planBatchStr);
                }
                foreach (var model in list)
                {
                    db.PlanBatchInfos.Add(model);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ajaxModel.code = 0;
                ajaxModel.msg = "保存失败！";
            }

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private static List<PlanBatchInfo> GetExcelData(string fileName)
        {
            var list = new List<PlanBatchInfo>();
            var url = System.AppDomain.CurrentDomain.BaseDirectory + fileName;

            Workbook book = new Workbook(url);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    var i = 0;
                    DataRow dr = dt.Rows[j];
                    var model = new PlanBatchInfo();
                    model.PlanBatch = dr[i++].ToString();
                    model.PlanName = dr[i++].ToString();
                    model.CityPlace = dr[i++].ToString();
                    model.BidMode = dr[i++].ToString();
                    model.BuyWay = dr[i++].ToString();
                    model.BidYear = dr[i++].ToString();
                    model.BidFlag = dr[i++].ToString();
                    model.PlanCheckDt = Convert.ToDateTime(dr[i++].ToString());
                    model.Status = dr[i++].ToString();
                    model.PlanStartDt = Convert.ToDateTime(dr[i++].ToString());
                    model.PlanEndDt = Convert.ToDateTime(dr[i++].ToString());
                    model.CityStartDt = Convert.ToDateTime(dr[i++].ToString());
                    model.Period = dr[i++].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <returns></returns>
        public FileResult ExportReportExcel()
        {
            try
            {
                //easyui分页必备参数
                int totalCount = 0;

                var retData = db.PlanBatchInfos.ToList();

                if (retData.Any())
                {
                    var excelTitle = HttpUtility.HtmlDecode(Request["ExcelTitle"] ?? "sheet");
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                    var dicpath = AppDomain.CurrentDomain.BaseDirectory + "Files\\ExportExcel";
                    if (!DirectoryHelper.IsExistDirectory(dicpath))
                    {
                        DirectoryHelper.CreateDirectory(dicpath);
                    }

                    var path = AppDomain.CurrentDomain.BaseDirectory + "Files\\ExportExcel\\" + fileName;
                    GenerateExportData(retData, excelTitle, path, false);

                    byte[] fileContext = FileHelper.FileReadBinary(path);

                    return File(fileContext, "application/octet-stream", fileName);
                }

            }
            catch (Exception ex)
            {
            }

            return null;
        }

        /// <summary>
        /// 汇总列
        /// </summary>
        private Dictionary<string, string> ExcelShowColumn
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string>();

                return result;
            }
        }

        /// <summary>
        /// 生成Excel列
        /// </summary>
        /// <returns></returns>
        private DataGridColumn GetExcelColumn()
        {
            var result = new DataGridColumn();
            var columns = new List<CellContent>();
            var style = ExcelStyle.ColumnStyle;

            var colIndex = 0;
            var columnWidth = new int[ExcelShowColumn.Count];

            foreach (var item in ExcelShowColumn)
            {
                var key = item.Key;
                var value = item.Value;

                columns.Add(new CellContent()
                {
                    CellValue = value,
                    MappingValue = item.Key,
                    RowIndex = 0,
                    ColumnIndex = colIndex,
                    TotalRows = 1,
                    TotalColumns = 1,
                    CellStyle = style,
                });

                if (colIndex == 2)
                {
                    columnWidth[colIndex] = 50;
                }
                else
                {
                    columnWidth[colIndex] = 20;
                }
                colIndex++;
            }
            result.ShowColumns = columns;
            result.ShowColumnWidth = columnWidth;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="excelTitle"></param>
        /// <param name="path"></param>
        /// <param name="isPrint"></param>
        private void GenerateExportData(List<PlanBatchInfo> rows, string excelTitle, string path, bool isPrint)
        {
            ExcelWork excelWork = new ExcelWork();
            excelWork.ExcelSheet = new List<ExcelSheet>();

            ExcelSheet excelSheet = new ExcelSheet()
            {
                SheetName = excelTitle,

                SheetDataGrid = new SheetDataGrid()
                {
                    ContentItems = ListConvertToDataTable.ToDataTable(rows),
                    DataGridColumn = GetExcelColumn(),
                    ContentRowHeight = 20,
                    StartRowIndex = 1,
                    ContentStyle = ExcelStyle.SecordTitleStyle
                },
            };

            excelWork.FilePath = path;
            excelWork.ExcelSheet.Add(excelSheet);

            ExcelAsposeOper.ExportExcel(excelWork, isPrint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
