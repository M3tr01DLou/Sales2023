using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _dataContext.Categories.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var country = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (country is null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Category category)
        {
            try
            {
                _dataContext.Add(category);
                await _dataContext.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"La categoría {category.Name} ya existe en la base de datos.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Category category)
        {
            try
            {
                _dataContext.Update(category);
                await _dataContext.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"La categoría {category.Name} ya existe en la base de datos.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _dataContext.Remove(category);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
