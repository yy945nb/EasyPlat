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
    public class BeginProjectScaleInfoController : Controller
    {
        private EasyPlatContext db = new EasyPlatContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPageData()
        {
            var levelList = new List<string>() {
                "35","110","220","500",
                "当月汇总","当月规模占年度比例","1-当月规模累计","1-当月规模占年度比例"
            };
            var ajaxModel = new AjaxDataResult() { code = 1, msg = "未获取到数据！" };

            var queryModel = new BeginProjectScaleQueryModel();
            queryModel.page = !string.IsNullOrEmpty(Request["page"]) ? Convert.ToInt32(Request["page"]) : 1;
            queryModel.limit = !string.IsNullOrEmpty(Request["limit"]) ? Convert.ToInt32(Request["limit"]) : 10;

            if (!string.IsNullOrEmpty(Request["Month"]))
            {
                queryModel.Month = Convert.ToDateTime(Request["Month"]).ToString("yyyy-MM");
            }
            var list = db.BeginProjectScaleInfos.Where(m => (!string.IsNullOrEmpty(queryModel.Month)) || (queryModel.Month != string.Empty && m.Month.Contains(queryModel.Month)));

            if (list.Any())
            {
                var groupData = list.GroupBy(m => new { m.Year, m.Month, m.VolLevel })
                    .Select(m => new BeginProjectScaleDto()
                    {
                        Year = m.Key.Year,
                        Month = m.Key.Month,
                        VolLevel = m.Key.VolLevel,
                        PNumber = m.Sum(n => n.PNumber),
                        TableNum = m.Sum(n => n.TableNum),
                        CircuitNum = m.Sum(n => n.CircuitNum),
                        Length = m.Sum(n => n.Length),
                        Volume = m.Sum(n => n.Volume),
                        AvgRate = 0
                    }).ToList();

                var monthList = groupData.Select(m => m.Year + m.Month).Distinct().ToList();

                foreach (var month in monthList)
                {
                    var firstData = groupData.FirstOrDefault(m => (m.Year + m.Month) == month);
                    var notContainsData = levelList.Where(m => !groupData.Where(t => t.TJDate == month).Select(n => n.VolLevel).Contains(m)).ToList();

                    foreach (var item in notContainsData)
                    {
                        groupData.Add(new BeginProjectScaleDto()
                        {
                            Year = firstData.Year,
                            Month = firstData.Month,
                            VolLevel = item,
                            PNumber = 0,
                            TableNum = 0,
                            CircuitNum = 0,
                            Length = 0,
                            Volume = 0,
                            AvgRate = 0
                        });
                    }
                }

                foreach (var year in list.Select(m => m.Year).Distinct())
                {
                    // 当年数据
                    var currentYearData = groupData.Where(m => m.Year == year).ToList();

                    var totalPNumber = currentYearData.Sum(m => m.PNumber);
                    var totalTableNum = currentYearData.Sum(m => m.TableNum);
                    var totalCircuitNum = currentYearData.Sum(m => m.CircuitNum);
                    var totalLength = currentYearData.Sum(m => m.Length);
                    var totalVolume = currentYearData.Sum(m => m.Volume);

                    foreach (var month in list.Where(m => m.Year == year).Select(m => m.Month).Distinct())
                    {
                        // 1月至当月的数据
                        var currentMonthData = currentYearData.Where(m => Convert.ToInt32(m.Month) <= Convert.ToInt32(month)).ToList();

                        // 1至当月累计数据
                        var totalMonthPNumber = currentMonthData.Sum(m => m.PNumber);
                        var totalMonthTableNum = currentMonthData.Sum(m => m.TableNum);
                        var totalMonthCircuitNum = currentMonthData.Sum(m => m.CircuitNum);
                        var totalMonthLength = currentMonthData.Sum(m => m.Length);
                        var totalMonthVolume = currentMonthData.Sum(m => m.Volume);

                        // 当前维度数据
                        var currentData = currentMonthData.Where(m => m.TJDate == year + month).ToList();

                        // 当月累计数据
                        var currentPNumber = currentData.Sum(m => m.PNumber);
                        var currentTableNum = currentData.Sum(m => m.TableNum);
                        var currentCircuitNum = currentData.Sum(m => m.CircuitNum);
                        var currentLength = currentData.Sum(m => m.Length);
                        var currentVolume = currentData.Sum(m => m.Volume);

                        // 月度汇总
                        var tempData1 = currentData.FirstOrDefault(m => m.VolLevel == "当月汇总");
                        tempData1.PNumber = currentPNumber;
                        tempData1.TableNum = currentTableNum;
                        tempData1.CircuitNum = currentCircuitNum;
                        tempData1.Length = currentLength;
                        tempData1.Volume = currentVolume;

                        // 当月规模占年度比例
                        var tempData2 = currentData.FirstOrDefault(m => m.VolLevel == "当月规模占年度比例");

                        tempData2.Length = Math.Round(currentLength / totalLength * 100, 2);
                        tempData2.Volume = Math.Round(currentVolume / totalVolume * 100, 2);

                        // 1-当月规模累计
                        var tempData3 = currentData.FirstOrDefault(m => m.VolLevel == "1-当月规模累计");
                        tempData3.PNumber = totalMonthPNumber;
                        tempData3.TableNum = totalMonthTableNum;
                        tempData3.CircuitNum = totalMonthCircuitNum;
                        tempData3.Length = totalMonthLength;
                        tempData3.Volume = totalMonthVolume;

                        // 1 - 当月规模占年度比例
                        var tempData4 = currentData.FirstOrDefault(m => m.VolLevel == "1-当月规模占年度比例");

                        tempData4.Length = Math.Round(totalMonthLength / totalLength * 100, 2);
                        tempData4.Volume = Math.Round(totalMonthVolume / totalVolume * 100, 2);
                    }
                }

                ajaxModel.count = groupData.Count;
                groupData = groupData.OrderBy(m => m.Year).ThenBy(m => m.Month).ThenBy(m => m.Sort).Skip(queryModel.page - 1).Take(queryModel.limit).ToList();

                if (groupData.Count > 0)
                {
                    ajaxModel.code = 0;
                    ajaxModel.data = groupData;
                    ajaxModel.msg = "获取数据成功！";
                }
            }

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
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
