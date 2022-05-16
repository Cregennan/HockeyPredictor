#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaHockey.Data;
using MegaHockey.Models;
using System.Security.Claims;

namespace MegaHockey.Controllers
{
    public class HistoryRecordsController : Controller
    {
        private readonly HistoryRecordContext _context;

        public HistoryRecordsController(HistoryRecordContext context)
        {
            _context = context;
        }

        // GET: HistoryRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.HistoryRecord.Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync());
        }

        // GET: HistoryRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyRecord = await _context.HistoryRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historyRecord == null)
            {
                return NotFound();
            }

            return View(historyRecord);
        }

        // GET: HistoryRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoryRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,First,Second,UserId")] HistoryRecord historyRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historyRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historyRecord);
        }

        // GET: HistoryRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyRecord = await _context.HistoryRecord.FindAsync(id);
            if (historyRecord == null)
            {
                return NotFound();
            }
            return View(historyRecord);
        }

        // POST: HistoryRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,First,Second,UserId")] HistoryRecord historyRecord)
        {
            if (id != historyRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historyRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryRecordExists(historyRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(historyRecord);
        }

        // GET: HistoryRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyRecord = await _context.HistoryRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historyRecord == null)
            {
                return NotFound();
            }

            return View(historyRecord);
        }

        // POST: HistoryRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historyRecord = await _context.HistoryRecord.FindAsync(id);
            _context.HistoryRecord.Remove(historyRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryRecordExists(int id)
        {
            return _context.HistoryRecord.Any(e => e.Id == id);
        }
    }
}
