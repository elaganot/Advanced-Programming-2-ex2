using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Chat.Data;
using E_Chat.Models;

namespace E_Chat.Controllers
{
    public class UserListsController : Controller
    {
        private readonly E_ChatContext _context;

        public UserListsController(E_ChatContext context)
        {
            _context = context;
        }

        // GET: UserLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: UserLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // GET: UserLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] UserList userList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userList);
        }

        // GET: UserLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.Users.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }
            return View(userList);
        }

        // POST: UserLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] UserList userList)
        {
            if (id != userList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserListExists(userList.Id))
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
            return View(userList);
        }

        // GET: UserLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // POST: UserLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userList = await _context.Users.FindAsync(id);
            _context.Users.Remove(userList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserListExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
