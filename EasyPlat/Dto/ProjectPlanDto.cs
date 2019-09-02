using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Dto
{
    public class ProjectPlanDto
    {
        public long Phid { get; set; }
        public long LineId { get; set; }
        public string ProjectNo { get; set; }
        public string Province { get; set; }
        public string Company { get; set; }
        public string CLevel { get; set; }
        public string ProjectName { get; set; }
        public string Catagory { get; set; }
        public string PType { get; set; }
        public string Nature { get; set; }
        public string IsBegin { get; set; }
        public string IsWork { get; set; }
        public string Vlength { get; set; }
        public string Volume { get; set; }
        public string CycleNum { get; set; }
        public string GroupNum { get; set; }
        public string Address { get; set; }
        public string Pre_fno { get; set; }
        public DateTime? Pre_bdt { get; set; }
        public string Pre_yno { get; set; }
        public string Pre_sno { get; set; }
        public string Pre_isable { get; set; }
        public string Pre_hno { get; set; }
        public DateTime? Pre_hdt { get; set; }
        public DateTime? Pre_pdt { get; set; }
        public DateTime? Pre_sdt { get; set; }
        public DateTime? Start_zdt { get; set; }
        public DateTime? Start_kdt { get; set; }
        public DateTime? Start_sdt { get; set; }
        public DateTime? Start_fdt { get; set; }
        public DateTime? Start_ydt { get; set; }
        public DateTime? Start_wdt { get; set; }
        public DateTime? Start_bdt { get; set; }
        public DateTime? Start_jdt { get; set; }
        public DateTime? Start_gdt { get; set; }
        public DateTime? Start_xdt { get; set; }
        public DateTime? Start_ldt { get; set; }
        public DateTime? Start_pdt { get; set; }
        public DateTime? Start_cdt { get; set; }
        public DateTime? Start_hdt { get; set; }
        public DateTime? Start_ddt { get; set; }
        public DateTime? Start_rdt { get; set; }
        public DateTime? Build_bdt { get; set; }
        public DateTime? Build_sdt { get; set; }
        public DateTime? Build_xdt { get; set; }
        public DateTime? Build_ydt { get; set; }
        public DateTime? Build_hdt { get; set; }
        public DateTime? Build_cdt { get; set; }
        public DateTime? Build_tdt { get; set; }
        public DateTime? Finish_jdt { get; set; }
        public DateTime? Finish_fdt { get; set; }
        public DateTime? Finish_zdt { get; set; }
        public decimal DynamicNum { get; set; }
        public decimal DemandNum { get; set; }
        public decimal TotalNum { get; set; }
        public decimal PlanNum { get; set; }
        public string Progress { get; set; }
        public string Remark { get; set; }
        public string Important_high { get; set; }
        public string Important_assort { get; set; }
        public string Important_strong { get; set; }
        public string Important_iron { get; set; }
        public string Important_rail { get; set; }
        public string Important_wind { get; set; }
        public string Important_light { get; set; }
        public string Important_give { get; set; }
        public string Important_farmer { get; set; }
        public string Important_summary { get; set; }
        public string Months{ get; set; }
        public string WorkReady { get; set; }
        public string WorkSet  { get; set; }
        public string WorkStart { get; set; }
        public string WorkBid  { get; set; }
        public string WorkCheck  { get; set; }
    }
}