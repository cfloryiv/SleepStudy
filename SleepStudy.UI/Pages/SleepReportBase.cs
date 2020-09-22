using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SleepStudy.Data.Data;
using SleepStudy.Data.Models;

namespace SleepStudy.UI.Pages
{

    public class SleepReportBase : ComponentBase
    {
        [Inject] public ApplicationDbContext Context { get; set; }
        public IEnumerable<SleepLine> sleepLines { get; set; } = new List<SleepLine>();
        public string Message { get; set; } = "";
        public List<SleepEntry> tempList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
               tempList = await Context.SleepEntries
                  .ToListAsync();

                sleepLines = tempList
                    .OrderBy(s=>s.Date)
                    .GroupBy(s => s.Date.Date)
                    .Select(g =>
                        new SleepLine(g.Key, g.Sum(f => 1), g.Sum(f => (int)SleepLine.CalcDuration(f.StartTime, f.WakeTime))));
            }
            catch (Exception ex)
            {
                Message = "Failed to create sleep entries";
            }
        }
    } 

    public class SleepLine
    {
        public SleepLine(DateTime date, int count, double duration)
        {
            Date = date;
            Count = count;
            Duration = duration;
        }

        public DateTime Date { get; set; }
        public int Count { get; set; }
        public double Duration { get; set; }

        public static double CalcDuration(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = dt2 - dt1;
            return ts.TotalMinutes;

        }
    }
}
