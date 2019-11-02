﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoStation.Models;
using AutoStation.Services;
using Microsoft.AspNetCore.Authorization;

namespace AutoStation.Controllers
{
    /// <summary>
    /// Расписание
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext _context;

        public SchedulesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _context.Schedules.ToListAsync();
        }

        // PUT: api/Schedules/5
        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ID)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return schedule;
        }

        // POST: api/Schedules
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        // DELETE: api/Schedules/5
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        /// <summary>
        /// Существует ли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ID == id);
        }
    }
}
