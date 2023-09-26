using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        { 
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_dataContext.Categories.Any())
            {
                _dataContext.Categories.Add(new Category { Name = "Apple" });
                _dataContext.Categories.Add(new Category { Name = "Calzado" });
                _dataContext.Categories.Add(new Category { Name = "informática" });
                _dataContext.Categories.Add(new Category { Name = "Hogar" });
                _dataContext.Categories.Add(new Category { Name = "Automovil" });
            }

            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_dataContext.Countries.Any())
            {
                _dataContext.Countries.Add(new Country { Name = "España" });
                _dataContext.Countries.Add(new Country { Name = "Portugal" });
                _dataContext.Countries.Add(new Country { Name = "Francia" });
                _dataContext.Countries.Add(new Country { Name = "Alemania" });
            }

            await _dataContext.SaveChangesAsync();
        }

    }
}
