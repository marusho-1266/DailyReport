using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReportApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyReportApp.Services
{
    public class TemplateService
    {
        public async Task<List<ReportTemplate>> GetReportTemplatesAsync()
        {
            using (var context = new DailyReportAppDbContext())
            {
                return await context.ReportTemplates.ToListAsync();
            }
        }

        public async Task<ReportTemplate> GetReportTemplateAsync(int id)
        {
            using (var context = new DailyReportAppDbContext())
            {
                return await context.ReportTemplates.FindAsync(id);
            }
        }

        public async Task<bool> AddReportTemplateAsync(ReportTemplate template)
        {
            using (var context = new DailyReportAppDbContext())
            {
                context.ReportTemplates.Add(template);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateReportTemplateAsync(ReportTemplate template)
        {
            using (var context = new DailyReportAppDbContext())
            {
                context.Entry(template).State = EntityState.Modified;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteReportTemplateAsync(int id)
        {
            using (var context = new DailyReportAppDbContext())
            {
                var template = await context.ReportTemplates.FindAsync(id);
                if (template == null) return false;

                context.ReportTemplates.Remove(template);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
