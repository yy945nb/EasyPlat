using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EasyPlat.Models
{
    public class EasyPlatContext:DbContext
    {
        public EasyPlatContext() : base("EasyPlatConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<ProjectPlanInfo> ProjectPlanInfos { get; set; }
        public DbSet<PlanBatchInfo> PlanBatchInfos { get; set; }
        public DbSet<WorkProjectScaleInfo> WorkProjectScaleInfos { get; set; }
        public DbSet<BeginProjectScaleInfo> BeginProjectScaleInfos { get; set; }
    }
}