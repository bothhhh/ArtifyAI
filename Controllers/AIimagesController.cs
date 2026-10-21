
using ArtifyAI.Data;
using ArtifyAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ArtifyAI.Controllers
{
    public class AIImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AIImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AIImages
        public async Task<IActionResult> Index()
        {
            var images = await _context.AIImages
                .OrderByDescending(i => i.UploadDate)
                .ToListAsync();

            return View(images);
        }

        // GET: AIImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var image = await _context.AIImages
                .FirstOrDefaultAsync(i => i.Id == id);

            if (image == null)
                return NotFound();

            return View(image);
        }

        // GET: AIImages/Create
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AIImages/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Prompt,ImageGenerator")] AIImage image,
            IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError(
                    "imageFile", "Please select an image.");
            }
            else
            {
                var extensions = new[]
                {
                    ".jpg", ".jpeg", ".png", ".webp"
                };

                var extension = Path.GetExtension(
                    imageFile.FileName).ToLowerInvariant();

                if (!extensions.Contains(extension))
                {
                    ModelState.AddModelError(
                        "imageFile",
                        "Only JPG, PNG and WebP images are allowed.");
                }

                if (imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError(
                        "imageFile",
                        "Maximum image size is 5 MB.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(image);
            }

            var fileExtension = Path.GetExtension(
                imageFile!.FileName).ToLowerInvariant();

            var filename = $"{Guid.NewGuid()}{fileExtension}";

            var uploadFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "images");

            Directory.CreateDirectory(uploadFolder);

            var filePath = Path.Combine(uploadFolder, filename);

            await using (var stream = new FileStream(
                filePath, FileMode.CreateNew))
            {
                await imageFile.CopyToAsync(stream);
            }

            image.Filename = filename;
            image.UploadDate = DateTime.Now;
            image.Likes = 0;
            image.canIncreaseLike = true;

            _context.AIImages.Add(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            }
            
        // GET: AIImages/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var image = await _context.AIImages.FindAsync(id);

            if (image == null)
                return NotFound();

            return View(image);
        }

        // POST: AIImages/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Prompt,ImageGenerator")] AIImage editedImage)
        {
            if (id != editedImage.Id)
                return NotFound();

            var image = await _context.AIImages.FindAsync(id);

            if (image == null)
                return NotFound();

            // The file information and likes are not editable.
            ModelState.Remove(nameof(AIImage.Filename));

            if (!ModelState.IsValid)
                return View(image);

            image.Prompt = editedImage.Prompt;
            image.ImageGenerator = editedImage.ImageGenerator;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            }
        // GET: AIImages/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var image = await _context.AIImages.FindAsync(id);

            if (image == null)
                return NotFound();

            return View(image);
        }

        // POST: AIImages/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.AIImages.FindAsync(id);

            if (image == null)
                return NotFound();

            _context.AIImages.Remove(image);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(image.Filename))
            {
                var filename = Path.GetFileName(image.Filename);

                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", "images", filename);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction(nameof(Index));
        
        }
    }
}
