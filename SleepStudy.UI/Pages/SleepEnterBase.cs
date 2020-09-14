using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SleepStudy.Data.Data;
using SleepStudy.Data.Models;

namespace SleepStudy.UI.Pages
{
    public class SleepEnterBase : ComponentBase
    {
        [Inject]
        public ApplicationDbContext Context { get; set; }
        public List<SleepEntry> sleepentries { get; set; } = new List<SleepEntry>();
        public SleepEntry newentry { get; set; } = new SleepEntry();
        public string Message { get; set; }
        public DateTime NewDay { get; set; } = DateTime.Now;


        protected override async Task OnInitializedAsync()
        {
            sleepentries = await Context.SleepEntries.ToListAsync();
            newentry=new SleepEntry();
            newentry.Date = NewDay;
            newentry.StartTime=DateTime.Now;
            newentry.Note = "";
        }

        public void NewDayHandler()
        {
            sleepentries=new List<SleepEntry>();
            newentry = new SleepEntry();
            newentry.Date = NewDay;
            newentry.StartTime = DateTime.Now;
            newentry.Note = "";
        }
        public async  void WakeUpHandler()
        {
            newentry.WakeTime=DateTime.Now;
            await Context.SleepEntries.AddAsync(newentry);
            await Context.SaveChangesAsync();
            sleepentries = await Context.SleepEntries.ToListAsync();
            newentry = new SleepEntry();
            newentry.Date = NewDay;
            newentry.StartTime = DateTime.Now;
            newentry.Note = "";
            StateHasChanged();
        }

        public void SleepHandler()
        {
            newentry = new SleepEntry();
            newentry.Date = NewDay;
            newentry.StartTime = DateTime.Now;
            newentry.Note = "";
        }
    }
}
