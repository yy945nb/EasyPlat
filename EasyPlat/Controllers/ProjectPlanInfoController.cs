using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using EasyPlat.Dto;
using EasyPlat.Models;
using EasyPlat.QueryModels;

namespace EasyPlat.Controllers
{
    public class ProjectPlanInfoController : Controller
    {
        private EasyPlatContext db = new EasyPlatContext();

        public ActionResult Index()
        {
            return View(db.ProjectPlanInfos.ToList());
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
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPageData()
        {
            var ajaxModel = new AjaxDataResult() { code = 1, msg = "未获取到数据！" };
            var queryModel = new ProjectPlanQueryModel();

            // 分页数据
            queryModel.page = !string.IsNullOrEmpty(Request["page"]) ? Convert.ToInt32(Request["page"]) : 1;
            queryModel.limit = !string.IsNullOrEmpty(Request["limit"]) ? Convert.ToInt32(Request["limit"]) : 10;

            queryModel.ProjectName = Request["ProjectName"];
            queryModel.CLevel = Request["CLevel"];

            if (!string.IsNullOrEmpty(Request["Build_bdt"]))
            {
                queryModel.Build_bdt = Convert.ToDateTime(Request["Build_bdt"]).ToString("yyyy-MM");
            }
            if (!string.IsNullOrEmpty(Request["Build_tdt"]))
            {
                queryModel.Build_tdt = Convert.ToDateTime(Request["Build_tdt"]).ToString("yyyy-MM");
            }

            queryModel.Catagory = Request["Catagory"];
            queryModel.PType = Request["PType"];
            queryModel.Nature = Request["Nature"];
            queryModel.IsBeginOrWork = Request["IsBeginOrWork"];

            var list = db.ProjectPlanInfos.Where(m =>
                (string.IsNullOrEmpty(queryModel.ProjectName) || (!string.IsNullOrEmpty(queryModel.ProjectName) && m.ProjectName.Contains(queryModel.ProjectName)))
                && (string.IsNullOrEmpty(queryModel.CLevel) || (!string.IsNullOrEmpty(queryModel.CLevel) && m.CLevel.Contains(queryModel.CLevel)))
                && (string.IsNullOrEmpty(queryModel.Catagory) || (!string.IsNullOrEmpty(queryModel.Catagory) && m.Catagory.Contains(queryModel.Catagory)))
                && (string.IsNullOrEmpty(queryModel.PType) || (!string.IsNullOrEmpty(queryModel.PType) && m.PType.Contains(queryModel.PType)))
                && (string.IsNullOrEmpty(queryModel.Nature) || (!string.IsNullOrEmpty(queryModel.Nature) && m.Nature.Contains(queryModel.Nature)))
            ).ToList();

            if (!string.IsNullOrEmpty(queryModel.Build_bdt))
            {
                list = list.Where(m => m.Build_bdt.HasValue && m.Build_bdt.Value.ToString("yyyy-MM").Contains(queryModel.Build_bdt)).ToList();
            }

            if (!string.IsNullOrEmpty(queryModel.Build_tdt))
            {
                list = list.Where(m => m.Build_tdt.HasValue && m.Build_tdt.Value.ToString("yyyy-MM").Contains(queryModel.Build_tdt)).ToList();
            }

            if (queryModel.IsBeginOrWork == "开工")
            {
                list = list.Where(m => m.IsBegin == "是" && m.IsWork != "是").ToList();
            }
            if (queryModel.IsBeginOrWork == "投产")
            {
                list = list.Where(m => m.IsWork == "是").ToList();
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
                var list = new List<ProjectPlanInfo>();

                if (!string.IsNullOrEmpty(planBatchStr))
                {
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectPlanInfo>>(planBatchStr);

                    if (list.Any())
                    {

                        foreach (var projectPlanInfo in list)
                        {
                            var projectPlanModel = db.ProjectPlanInfos.FirstOrDefault(m => m.Phid == projectPlanInfo.Phid);

                            if (projectPlanModel != null)
                            {
                                projectPlanModel.LineId = projectPlanInfo.LineId;
                                projectPlanModel.ProjectNo = projectPlanInfo.ProjectNo;
                                projectPlanModel.Province = projectPlanInfo.Province;
                                projectPlanModel.Company = projectPlanInfo.Company;
                                projectPlanModel.CLevel = projectPlanInfo.CLevel;
                                projectPlanModel.ProjectName = projectPlanInfo.ProjectName;
                                projectPlanModel.Catagory = projectPlanInfo.Catagory;
                                projectPlanModel.PType = projectPlanInfo.PType;
                                projectPlanModel.Nature = projectPlanInfo.Nature;
                                projectPlanModel.IsBegin = projectPlanInfo.IsBegin;
                                projectPlanModel.IsWork = projectPlanInfo.IsWork;
                                projectPlanModel.Vlength = projectPlanInfo.Vlength;
                                projectPlanModel.Volume = projectPlanInfo.Volume;
                                projectPlanModel.CycleNum = projectPlanInfo.CycleNum;
                                projectPlanModel.GroupNum = projectPlanInfo.GroupNum;
                                projectPlanModel.Address = projectPlanInfo.Address;
                                projectPlanModel.Pre_fno = projectPlanInfo.Pre_fno;
                                projectPlanModel.Pre_bdt = projectPlanInfo.Pre_bdt;
                                projectPlanModel.Pre_yno = projectPlanInfo.Pre_yno;
                                projectPlanModel.Pre_sno = projectPlanInfo.Pre_sno;
                                projectPlanModel.Pre_isable = projectPlanInfo.Pre_isable;
                                projectPlanModel.Pre_hno = projectPlanInfo.Pre_hno;
                                projectPlanModel.Pre_hdt = projectPlanInfo.Pre_hdt;
                                projectPlanModel.Pre_pdt = projectPlanInfo.Pre_pdt;
                                projectPlanModel.Pre_sdt = projectPlanInfo.Pre_sdt;
                                projectPlanModel.Start_zdt = projectPlanInfo.Start_zdt;
                                projectPlanModel.Start_kdt = projectPlanInfo.Start_kdt;
                                projectPlanModel.Start_sdt = projectPlanInfo.Start_sdt;
                                projectPlanModel.Start_fdt = projectPlanInfo.Start_fdt;
                                projectPlanModel.Start_ydt = projectPlanInfo.Start_ydt;
                                projectPlanModel.Start_wdt = projectPlanInfo.Start_wdt;
                                projectPlanModel.Start_bdt = projectPlanInfo.Start_bdt;
                                projectPlanModel.Start_jdt = projectPlanInfo.Start_jdt;
                                projectPlanModel.Start_gdt = projectPlanInfo.Start_gdt;
                                projectPlanModel.Start_xdt = projectPlanInfo.Start_xdt;
                                projectPlanModel.Start_ldt = projectPlanInfo.Start_ldt;
                                projectPlanModel.Start_pdt = projectPlanInfo.Start_pdt;
                                projectPlanModel.Start_cdt = projectPlanInfo.Start_cdt;
                                projectPlanModel.Start_hdt = projectPlanInfo.Start_hdt;
                                projectPlanModel.Start_ddt = projectPlanInfo.Start_ddt;
                                projectPlanModel.Start_rdt = projectPlanInfo.Start_rdt;
                                projectPlanModel.Build_bdt = projectPlanInfo.Build_bdt;
                                projectPlanModel.Build_sdt = projectPlanInfo.Build_sdt;
                                projectPlanModel.Build_xdt = projectPlanInfo.Build_xdt;
                                projectPlanModel.Build_ydt = projectPlanInfo.Build_ydt;
                                projectPlanModel.Build_hdt = projectPlanInfo.Build_hdt;
                                projectPlanModel.Build_cdt = projectPlanInfo.Build_cdt;
                                projectPlanModel.Build_tdt = projectPlanInfo.Build_tdt;
                                projectPlanModel.Finish_jdt = projectPlanInfo.Finish_jdt;
                                projectPlanModel.Finish_fdt = projectPlanInfo.Finish_fdt;
                                projectPlanModel.Finish_zdt = projectPlanInfo.Finish_zdt;
                                projectPlanModel.DynamicNum = projectPlanInfo.DynamicNum;
                                projectPlanModel.DemandNum = projectPlanInfo.DemandNum;
                                projectPlanModel.TotalNum = projectPlanInfo.TotalNum;
                                projectPlanModel.PlanNum = projectPlanInfo.PlanNum;
                                projectPlanModel.Progress = projectPlanInfo.Progress;
                                projectPlanModel.Remark = projectPlanInfo.Remark;
                                projectPlanModel.Important_high = projectPlanInfo.Important_high;
                                projectPlanModel.Important_assort = projectPlanInfo.Important_assort;
                                projectPlanModel.Important_strong = projectPlanInfo.Important_strong;
                                projectPlanModel.Important_iron = projectPlanInfo.Important_iron;
                                projectPlanModel.Important_rail = projectPlanInfo.Important_rail;
                                projectPlanModel.Important_wind = projectPlanInfo.Important_wind;
                                projectPlanModel.Important_light = projectPlanInfo.Important_light;
                                projectPlanModel.Important_give = projectPlanInfo.Important_give;
                                projectPlanModel.Important_farmer = projectPlanInfo.Important_farmer;
                                projectPlanModel.Important_summary = projectPlanInfo.Important_summary;
                                projectPlanModel.Months = projectPlanInfo.Months;
                                projectPlanModel.WorkReady = projectPlanInfo.WorkReady;
                                projectPlanModel.WorkSet = projectPlanInfo.WorkSet;
                                projectPlanModel.WorkStart = projectPlanInfo.WorkStart;
                                projectPlanModel.WorkBid = projectPlanInfo.WorkBid;
                                projectPlanModel.WorkCheck = projectPlanInfo.WorkCheck;

                                // 已开工项目
                                if (projectPlanInfo.IsBegin == "是" && projectPlanInfo.IsWork != "是")
                                {
                                    var beginProjectInfo = db.BeginProjectScaleInfos.FirstOrDefault(m => m.PPhid == projectPlanModel.Phid);

                                    if (beginProjectInfo != null)
                                    {
                                        beginProjectInfo.Year = projectPlanInfo.Build_bdt.Value.ToString("yyyy");
                                        beginProjectInfo.Month = projectPlanInfo.Build_bdt.Value.ToString("MM");

                                        beginProjectInfo.VolLevel = projectPlanInfo.CLevel;
                                        beginProjectInfo.PNumber = 1;
                                        beginProjectInfo.CircuitNum = Convert.ToInt32(projectPlanInfo.CycleNum ?? "0");
                                        beginProjectInfo.TableNum = Convert.ToInt32(projectPlanInfo.GroupNum ?? "0");
                                        beginProjectInfo.Length = Convert.ToDouble(projectPlanInfo.Vlength ?? "0");
                                        beginProjectInfo.Volume = Convert.ToDecimal(projectPlanInfo.Volume ?? "0");
                                    }
                                }

                                // 已投产项目
                                if (projectPlanInfo.IsWork == "是")
                                {
                                    var workProjectInfo = db.WorkProjectScaleInfos.FirstOrDefault(m => m.PPhid == projectPlanInfo.Phid);

                                    if (workProjectInfo != null)
                                    {
                                        workProjectInfo.Year = projectPlanInfo.Build_tdt.Value.ToString("yyyy");
                                        workProjectInfo.Month = projectPlanInfo.Build_tdt.Value.ToString("MM");
                                        workProjectInfo.VolLevel = projectPlanInfo.CLevel;
                                        workProjectInfo.PNumber = 1;
                                        workProjectInfo.CircuitNum = Convert.ToInt32(projectPlanInfo.CycleNum ?? "0");
                                        workProjectInfo.TableNum = Convert.ToInt32(projectPlanInfo.GroupNum ?? "0");
                                        workProjectInfo.Length = Convert.ToDouble(projectPlanInfo.Vlength ?? "0");
                                        workProjectInfo.Volume = Convert.ToDecimal(projectPlanInfo.Volume ?? "0");
                                    }

                                }

                                db.SaveChanges();
                            }
                            else
                            {
                                db.ProjectPlanInfos.Add(projectPlanInfo);
                                db.SaveChanges();

                                // 已开工项目
                                if (projectPlanInfo.IsBegin == "是" && projectPlanInfo.IsWork != "是")
                                {
                                    var beginProjectInfo = new BeginProjectScaleInfo();

                                    beginProjectInfo.PPhid = projectPlanInfo.Phid;
                                    beginProjectInfo.Year = projectPlanInfo.Build_bdt.Value.ToString("yyyy");
                                    beginProjectInfo.Month = projectPlanInfo.Build_bdt.Value.ToString("MM");

                                    beginProjectInfo.VolLevel = projectPlanInfo.CLevel;
                                    beginProjectInfo.PNumber = 1;
                                    beginProjectInfo.CircuitNum = Convert.ToInt32(projectPlanInfo.CycleNum ?? "0");
                                    beginProjectInfo.TableNum = Convert.ToInt32(projectPlanInfo.GroupNum ?? "0");
                                    beginProjectInfo.Length = Convert.ToDouble(projectPlanInfo.Vlength ?? "0");
                                    beginProjectInfo.Volume = Convert.ToDecimal(projectPlanInfo.Volume ?? "0");

                                    db.BeginProjectScaleInfos.Add(beginProjectInfo);
                                }

                                // 已投产项目
                                if (projectPlanInfo.IsWork == "是")
                                {
                                    var workProjectInfo = new WorkProjectScaleInfo();

                                    workProjectInfo.PPhid = projectPlanInfo.Phid;
                                    workProjectInfo.Year = projectPlanInfo.Build_tdt.Value.ToString("yyyy");
                                    workProjectInfo.Month = projectPlanInfo.Build_tdt.Value.ToString("MM");
                                    workProjectInfo.VolLevel = projectPlanInfo.CLevel;
                                    workProjectInfo.PNumber = 1;
                                    workProjectInfo.CircuitNum = Convert.ToInt32(projectPlanInfo.CycleNum ?? "0");
                                    workProjectInfo.TableNum = Convert.ToInt32(projectPlanInfo.GroupNum ?? "0");
                                    workProjectInfo.Length = Convert.ToDouble(projectPlanInfo.Vlength ?? "0");
                                    workProjectInfo.Volume = Convert.ToDecimal(projectPlanInfo.Volume ?? "0");

                                    db.WorkProjectScaleInfos.Add(workProjectInfo);
                                }

                                db.SaveChanges();
                            }
                        }

                        ajaxModel.code = 1;
                        ajaxModel.msg = "保存成功！";
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
                            var planProjectInfo = db.ProjectPlanInfos.Find(id);

                            if (planProjectInfo != null)
                            {
                                db.ProjectPlanInfos.Remove(planProjectInfo);
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
        /// 获取Excel数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private static List<ProjectPlanInfo> GetExcelData(string fileName)
        {
            var list = new List<ProjectPlanInfo>();
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
                    var dr = dt.Rows[j];
                    var model = new ProjectPlanInfo();

                    model.LineId = Convert.ToInt64(dr[i++].ToString());
                    model.ProjectNo = dr[i++].ToString();
                    model.Province = dr[i++].ToString();
                    model.Company = dr[i++].ToString();
                    model.CLevel = dr[i++].ToString();
                    model.ProjectName = dr[i++].ToString();
                    model.Catagory = dr[i++].ToString();
                    model.PType = dr[i++].ToString();
                    model.Nature = dr[i++].ToString();
                    model.IsBegin = dr[i++].ToString();
                    model.IsWork = dr[i++].ToString();
                    model.Vlength = dr[i++].ToString();
                    model.Volume = dr[i++].ToString();
                    model.CycleNum = dr[i++].ToString();
                    model.GroupNum = dr[i++].ToString();
                    model.Address = dr[i++].ToString();
                    model.Pre_fno = dr[i++].ToString();

                    var tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Pre_bdt = Convert.ToDateTime(tempStr);
                    }

                    model.Pre_yno = dr[i++].ToString();
                    model.Pre_sno = dr[i++].ToString();
                    model.Pre_isable = dr[i++].ToString();
                    model.Pre_hno = dr[i++].ToString();

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Pre_hdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Pre_pdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Pre_sdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_zdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_kdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_sdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_fdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_ydt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_wdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_bdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_jdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_gdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_xdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_ldt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_pdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_cdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_hdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_ddt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Start_rdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_bdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_sdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_xdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_ydt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_hdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_cdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Build_tdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Finish_jdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Finish_fdt = Convert.ToDateTime(tempStr);
                    }

                    tempStr = dr[i++].ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr.ToLower() != "null")
                    {
                        model.Finish_zdt = Convert.ToDateTime(tempStr);
                    }

                    model.DynamicNum = Convert.ToDecimal(dr[i++].ToString());
                    model.DemandNum = Convert.ToDecimal(dr[i++].ToString());
                    model.TotalNum = Convert.ToDecimal(dr[i++].ToString());
                    model.PlanNum = Convert.ToDecimal(dr[i++].ToString());

                    model.Progress = dr[i++].ToString();
                    model.Remark = dr[i++].ToString();

                    model.Important_high = dr[i++].ToString();
                    model.Important_assort = dr[i++].ToString();
                    model.Important_strong = dr[i++].ToString();
                    model.Important_iron = dr[i++].ToString();
                    model.Important_rail = dr[i++].ToString();
                    model.Important_wind = dr[i++].ToString();
                    model.Important_light = dr[i++].ToString();
                    model.Important_give = dr[i++].ToString();
                    model.Important_farmer = dr[i++].ToString();
                    model.Important_summary = dr[i++].ToString();

                    model.Months = dr[i++].ToString();
                    model.WorkReady = dr[i++].ToString();
                    model.WorkSet = dr[i++].ToString();
                    model.WorkStart = dr[i++].ToString();
                    model.WorkBid = dr[i++].ToString();
                    model.WorkCheck = dr[i++].ToString();

                    list.Add(model);
                }
            }

            return list;
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
