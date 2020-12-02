using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonBlog2.Data;
using DragonBlog2.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using DragonBlog2.Utilities;

namespace DragonBlog2.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ImageHelper imageHelper = new ImageHelper();

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.Image != null)
            {
            ViewData["Image"] = imageHelper.DecodeImage(post.Image, post.FileName);
            }
            
            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create(int? id)
        {
            //Option 1: we are given an id for which to create the post for
            //Option 2: we do not receive an id 
            Post post = null;
            if (id != null)
            {
                var blog = _context.Blog.Find(id);
                if (blog == null)
                {
                    return NotFound();
                }
                post = new Post() { BlogId = (int)id };
                ViewData["BlogName"] = blog.Name;
            }
            else
            {
                ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name");
            }

            return View(post);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Abstract,Content")] Post post, IFormFile image)
        {
            if (ModelState.IsValid)   //Update method to receive & store image
            {
                post.Created = DateTime.Now;
                if (image != null)
                {
                    post.FileName = image.FileName;

                    post.Image = imageHelper.EncodeImage(image);
                }

                    _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        //New BlogPosts Action
        public async Task<IActionResult> BlogPosts(int? id)
        {
            if (id == null)
            {
                return NotFound();

            } 
        
            var blog = await _context.Blog.FindAsync(id); 
            if (blog == null) 
            {
            return NotFound();
            }
            ViewData["Count"] = blog.Posts.Count();
            ViewData["BlogName"] = blog.Name;
            ViewData["BlogId"] = blog.Id;

            var blogPosts = await _context.Post.Where(p=> p.BlogId == id).ToListAsync();
            return View(blogPosts);
        
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,Title,Abstract,Content,Created,Image")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.Updated = DateTime.Now; 
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
