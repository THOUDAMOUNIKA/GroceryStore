using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroceryItemsController : ControllerBase
    {
        private readonly GroceryStoreContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<GroceryItemsController> _logger;

        public GroceryItemsController(GroceryStoreContext context, IMemoryCache cache, ILogger<GroceryItemsController> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GroceryItem>>> GetGroceryItems(
            [FromQuery] string? search = null,
            [FromQuery] string? category = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12)
        {
            _logger.LogInformation("Getting grocery items - Search: {Search}, Category: {Category}, Page: {Page}", search, category, page);
            
            var cacheKey = $"groceries_{search}_{category}_{page}_{pageSize}";
            
            var query = _context.GroceryItems.Where(g => g.IsAvailable && g.Quantity > 0);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(g => g.Name.Contains(search) || g.Description.Contains(search));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(g => g.Category == category);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(g => g.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<GroceryItem>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            _logger.LogInformation("Retrieved {Count} items out of {Total} total items", items.Count, totalCount);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryItem>> GetGroceryItem(int id)
        {
            var item = await _context.GroceryItems.FindAsync(id);
            if (item == null || !item.IsAvailable)
                return NotFound();

            return item;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            return await _context.GroceryItems
                .Where(g => g.IsAvailable)
                .Select(g => g.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
    }
}