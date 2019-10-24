using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Don_SampleTrackingApp;

namespace Don_SampleTrackingApp.Controllers
{
    public class TrackingUsersController : Controller
    {
        private readonly RaportareDbContext _context;

        public TrackingUsersController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: TrackingUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: TrackingUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackingUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (trackingUser == null)
            {
                return NotFound();
            }

            return View(trackingUser);
        }

        // GET: TrackingUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrackingUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,Nume,Prenume,Rol,IsEnable")] TrackingUser trackingUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trackingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trackingUser);
        }

        // GET: TrackingUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackingUser = await _context.Users.FindAsync(id);
            if (trackingUser == null)
            {
                return NotFound();
            }
            return View(trackingUser);
        }

        // POST: TrackingUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,Nume,Prenume,Rol,IsEnable")] TrackingUser trackingUser)
        {
            if (id != trackingUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackingUserExists(trackingUser.UserId))
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
            return View(trackingUser);
        }

        // GET: TrackingUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackingUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (trackingUser == null)
            {
                return NotFound();
            }

            return View(trackingUser);
        }

        // POST: TrackingUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trackingUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(trackingUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackingUserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
