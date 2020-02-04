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
    /// <summary>
    /// Сервис состояний
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// Следующее состояние
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<WorkerCheck> NextStateAsync(int user);
        /// <summary>
        /// Текущее состояние
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<WorkerCheck> CurrentStateAsync(int user);
        /// <summary>
        /// Пауза
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<WorkerCheck> PauseAsync(int user);
        /// <summary>
        /// Получить время для таймера 
        /// </summary>
        /// <param name="currentCheck"></param>
        /// <returns></returns>
        DateTime GetTimerTime(WorkerCheck currentCheck);
        /// <summary>
        /// Получить начало цикла работы для данного состояния
        /// </summary>
        /// <param name="workerCheck"></param>
        /// <returns></returns>
        WorkerCheck GetStartDateTime(WorkerCheck workerCheck);
        /// <summary>
        /// Получить информацию о работнике
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<WorkerInfo<DayInfo>> GetWorkerDaysInfoAsync(int id, int year, int month);
    }

    public class StateService : IStateService
    {
        /// <summary>
        /// БД
        /// </summary>
        readonly DatabaseContext _context;

        public StateService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<WorkerCheck> CurrentStateAsync(int user)
        {
            // Получим последнее состояние
            var currentState = await _context.WorkerChecks.Where(x => x.WorkerId == user).OrderByDescending(x => x.ID).FirstOrDefaultAsync() ?? new WorkerCheck { Type = Models.Type.Out };
            currentState.DateTime = GetTimerTime(currentState);
            return currentState;
        }

        public async Task<WorkerCheck> NextStateAsync(int user)
        {
            // Получим последнее состояние
            var workerCheck = (await _context.WorkerChecks.AsNoTracking().Where(x => x.WorkerId == user).OrderByDescending(x => x.ID).FirstOrDefaultAsync()) ?? new WorkerCheck { Type = Models.Type.Out, WorkerId = user };
            // Опеределим переход
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

            // Заполним поля, как для нового перехода
            workerCheck.ID = 0;
            workerCheck.DateTime = DateTime.Now;
            _context.Add(workerCheck);
            _context.SaveChanges();
            // Вычисление таймера для GUI
            workerCheck.DateTime = GetTimerTime(workerCheck);
            return workerCheck;
        }

        public async Task<WorkerCheck> PauseAsync(int user)
        {
            // Новая пауза
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
                    // Для входа просто посчитаем разницу
                    return new DateTime(DateTime.Now.Ticks - currentCheck.DateTime.Ticks);
                case Models.Type.Out:
                case Models.Type.Pause:
                    {
                        // Получим начало
                        var startIn = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.Type == Models.Type.In && x.ID < currentCheck.ID).LastOrDefault();
                        // Если до этого не было событий
                        if (startIn == null)
                            return DateTime.Now;
                        // Получим все отметки от startIn до текущего
                        var allTimes = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.ID >= startIn.ID && x.ID < currentCheck.ID).ToList();
                        long ticks = 0;
                        // Посчитаем тики для всех
                        for (int i = 1; i < allTimes.Count; i += 2)
                            ticks += allTimes[i].DateTime.Ticks - allTimes[i - 1].DateTime.Ticks;
                        ticks += currentCheck.DateTime.Ticks - allTimes.Last().DateTime.Ticks;
                        // Результат
                        return new DateTime(ticks);
                    }
                case Models.Type.Continue:
                    {
                        // Получить начало в виде паузы
                        var startPause = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.Type == Models.Type.Pause && x.ID < currentCheck.ID).OrderByDescending(x => x.ID).First();
                        var pauseTime = GetTimerTime(startPause);
                        return new DateTime(pauseTime.Ticks + DateTime.Now.Ticks - currentCheck.DateTime.Ticks);
                    }
                case Models.Type.Custom:
                    {
                        // Получим все отметки от startIn до текущего
                        var allTimes = _context.WorkerChecks.Where(x => x.WorkerId == currentCheck.WorkerId && x.DateTime > currentCheck.DateTime.Date 
                                                                                                                        && x.ID < currentCheck.ID).ToList();
                        if (allTimes.FirstOrDefault()?.Type == Models.Type.Continue)
                            allTimes.RemoveAt(0);
                        allTimes.Insert(0, new WorkerCheck { DateTime = currentCheck.DateTime.Date });
                        long ticks = 0;
                        // Посчитаем тики для всех
                        for (int i = 1; i < allTimes.Count; i += 2)
                            ticks += allTimes[i].DateTime.Ticks - allTimes[i - 1].DateTime.Ticks;
                        ticks += currentCheck.DateTime.Ticks - allTimes.Last().DateTime.Ticks;
                        // Результат
                        return new DateTime(ticks);
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
            // Получим все отметки за месяц
            var workJobs = await _context.WorkerChecks.Where(x => x.WorkerId == id && x.DateTime.InMonth(year, month)).ToListAsync();
            // Пройдемся по дням
            foreach (var group in workJobs.GroupBy(x => x.DateTime.Day))
            {
                var first = group.First();
                bool isPrevStart = false;
                // Если начало - продожение предыдущего дня
                if (first.Type != Models.Type.In)
                    isPrevStart = true;

                // Получим все Окончания на этот день
                var processChecks = group.Where(x => x.Type == Models.Type.Out && GetStartDateTime(x).DateTime.Day == group.Key).ToList();
                if (group.Last().Type != Models.Type.Out)
                    processChecks.Add(group.Last());

                long ticks = 0;
                // Если начало - не начало
                if (isPrevStart)
                {
                    
                    // Получим 
                    var prev = group.Where(x => x.Type == Models.Type.Out && x.ID > first.ID).FirstOrDefault();
                    prev.Type = Models.Type.Custom;
                    ticks += GetTimerTime(prev).Ticks;
                }

                foreach (var timePoint in processChecks)
                    ticks += GetTimerTime(timePoint).Ticks;

                workerInfo.WorkDistances.Add(new DayInfo { Day = group.Key, Time = ticks.ToTimeString() });
            }

            return workerInfo;
        }
    }
}
