using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReportApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyReportApp.Services
{
    public class DatabaseService
    {
        public async Task<List<DailyReport>> GetDailyReportsAsync(DateTime date)
        {
            using (var context = new DailyReportAppDbContext())
            {
                return await context.DailyReports
                    .Where(r => r.Date == date)
                    .ToListAsync();
            }
        }

        public async Task<bool> SaveDailyReportAsync(DailyReport report)
        {
            using (var context = new DailyReportAppDbContext())
            {
                var existingReport = await context.DailyReports.FirstOrDefaultAsync(r => r.Date == report.Date);

                if (existingReport == null)
                {
                    context.DailyReports.Add(report);
                }
                else
                {
                    existingReport.Tasks = report.Tasks;
                    existingReport.Results = report.Results;
                    existingReport.TomorrowPlan = report.TomorrowPlan;
                    existingReport.MonthlyReview = report.MonthlyReview;
                context.Entry(existingReport).State = EntityState.Modified;
                }
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<List<DailyReport>> GetAllDailyReportsAsync()
        {
            using (var context = new DailyReportAppDbContext())
            {
                return await context.DailyReports.ToListAsync();
            }
        }
    }
}
