using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkerPluginAPI.Extensions;
using WorkerPluginAPI.Models;
using WorkerPluginAPI.Models.Controllers;

namespace WorkerPluginAPI.Services
{
    public interface IStateService
    {
        Task<WorkerCheck> NextStateAsync(int user);
        Task<WorkerCheck> CurrentStateAsync(int user);
        Task<WorkerCheck> PauseAsync(int user);
        DateTime GetTimerTime(WorkerCheck currentCheck);
        WorkerCheck GetStartDateTime(WorkerCheck workerCheck);
        Task<WorkerInfo<DayInfo>> GetWorkerDaysInfoAsync(int id, int year, int month);
    }

    public class StateService : IStateService
    {
        DatabaseContext _context;

        public StateService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<WorkerCheck> CurrentStateAsync(int user)
        {
            var currentState = await _context.WorkerChecks.Where(x => x.WorkerId == user).OrderByDescending(x => x.ID).FirstOrDefaultAsync() ?? new WorkerCheck { Type = Models.Type.Out };
            currentState.DateTime = GetTimerTime(currentState);
            return currentState;
        }

        public async Task<WorkerCheck> NextStateAsync(int user)
        {
            var workerCheck = (await _context.WorkerChecks.AsNoTracking().Where(x => x.WorkerId == user).OrderByDescending(x => x.ID).FirstOrDefaultAsync()) ?? new WorkerCheck { Type = Models.Type.Out, WorkerId = user };
            switch (workerCheck.Type)
            {
                case Models.Type.In:
                    workerCheck.Type = Models.Type.Out;
                    break;
                case Models.Type.Out:
                    workerCheck.Type = Models.Type.In;
                    break;
                case Models.Type.Pause:
                    workerCheck.Type = Models.Type.Continue;
                    break;
                case Models.Type.Continue:
                    workerCheck.Type = Models.Type.Out;
                    break;
            }

            workerCheck.ID = 0;
            workerCheck.DateTime = DateTime.Now;
            _context.Add(workerCheck);
            _context.SaveChanges();
            workerCheck.DateTime = GetTimerTime(workerCheck);
            return workerCheck;
        }

        public async Task<WorkerCheck> PauseAsync(int user)
        {
            var newAction = new WorkerCheck { Type = Models.Type.Pause, WorkerId = user };
            _context.Add(newAction);
            await _context.SaveChangesAsync();
            newAction.DateTime = GetTimerTime(newAction);
            return newAction;
        }

        public WorkerCheck GetStartDateTime(WorkerCheck workerCheck) => _context.WorkerChecks.Where(x => x.WorkerId == workerCheck.WorkerId && x.Type == Models.Type.In && x.ID < workerCheck.ID)
                                                                                                                                        .OrderByDescending(x => x.ID).FirstOrDefault();

        public DateTime GetTimerTime(WorkerCheck currentCheck)
        {
            switch (currentCheck?.Type)
            {
                case Models.Type.In:
                    return new DateTime(DateTime.Now.Ticks - currentCheck.DateTime.Ticks);
                case Models.Type.Out:
                case Models.Type.Pause:
                    {
                        var startIn = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.Type == Models.Type.In && x.ID < currentCheck.ID).OrderByDescending(x => x.ID).FirstOrDefault();
                        if (startIn == null)
                            return DateTime.Now;
                        var allTimes = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.ID >= startIn.ID && x.ID < currentCheck.ID).ToList();
                        long ticks = 0;
                        for (int i = 1; i < allTimes.Count; i += 2)
                            ticks += allTimes[i].DateTime.Ticks - allTimes[i - 1].DateTime.Ticks;
                        ticks += currentCheck.DateTime.Ticks - allTimes.Last().DateTime.Ticks;
                        return new DateTime(ticks);
                    }
                case Models.Type.Continue:
                    {
                        var startPause = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.Type == Models.Type.Pause && x.ID < currentCheck.ID).OrderByDescending(x => x.ID).First();
                        var pauseTime = GetTimerTime(startPause);
                        return new DateTime(pauseTime.Ticks + DateTime.Now.Ticks - currentCheck.DateTime.Ticks);
                    }
            }
            return DateTime.Now;
        }

        public async Task<WorkerInfo<DayInfo>> GetWorkerDaysInfoAsync(int id, int year, int month)
        {
            var worker = _context.Workers.FirstOrDefault(x => x.ID == id);
            if (worker == null)
                return null;
            var workerInfo = new WorkerInfo<DayInfo> { Worker = worker };
            var workJobs = await _context.WorkerChecks.Where(x => x.WorkerId == id && x.DateTime.InMonth(year, month)).ToListAsync();
            foreach (var group in workJobs.GroupBy(x => x.DateTime.Day))
            {
                var first = group.First();
                bool isPrevStart = false;
                if (first.Type != Models.Type.In)
                    isPrevStart = true;

                var processChecks = group.Where(x => x.Type == Models.Type.Out && GetStartDateTime(x).DateTime.Day == group.Key).ToList();
                if (group.Last().Type != Models.Type.Out)
                    processChecks.Add(group.Last());

                long ticks = 0;
                if (isPrevStart)
                {
                    var prev = group.Where(x => x.Type == Models.Type.Out && x.ID > first.ID).FirstOrDefault();
                    if (prev == null)
                        prev = group.Last();
                    var prevStart = _context.WorkerChecks.Where(x => x.ID < prev.ID && x.DateTime.Date < prev.DateTime.Date).Last();
                    ticks += GetTimerTime(prev).Ticks - GetTimerTime(prevStart).Ticks;
                }

                foreach (var timePoint in processChecks)
                    ticks += GetTimerTime(timePoint).Ticks;

                workerInfo.WorkDistances.Add(new DayInfo { Day = group.Key, Time = ticks.ToTimeString() });
            }

            return workerInfo;
        }
    }
}
