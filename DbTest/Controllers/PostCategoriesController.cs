using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbTest.Ef;
using DbTest.Models;

namespace DbTest.Controllers
{
    public class PostCategoriesController : Controller
    {
        private readonly DataContext _context;

        public PostCategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: PostCategories
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PostCategory.Include(p => p.Category).Include(p => p.Post);
            return View(await dataContext.ToListAsync());
        }

        // GET: PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory
                .Include(p => p.Category)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: PostCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        // POST: PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,CategoryId")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // GET: PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // POST: PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,CategoryId")] PostCategory postCategory)
        {
            if (id != postCategory.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.PostId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // GET: PostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory
                .Include(p => p.Category)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postCategory = await _context.PostCategory.FindAsync(id);
            _context.PostCategory.Remove(postCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
            return _context.PostCategory.Any(e => e.PostId == id);
        }
    }
}
