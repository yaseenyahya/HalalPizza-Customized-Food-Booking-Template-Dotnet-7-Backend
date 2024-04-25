using halalpizzabackend.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace halalpizzabackend.Models
{
    public class AddonsService : IAddonsService
    {
        private readonly ApplicationDbContext _dbContext; // Replace YourDbContext with your actual DbContext

        public AddonsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Addons AddAddon(Addons addon)
        {
            _dbContext.Addons.Add(addon);
            _dbContext.SaveChanges();
            return addon;
        }

        public List<Addons> GetAllAddons()
        {
            return _dbContext.Addons.OrderByDescending(s => s.ID).ToList();
        }

        public Addons GetAddonById(int id)
        {
            return _dbContext.Addons.Find(id);
        }

        public Addons EditAddon(int id, Addons updatedAddon)
        {
            var existingAddon = _dbContext.Addons.Find(id);

            if (existingAddon != null)
            {
                existingAddon.ProductTitle = updatedAddon.ProductTitle;
                existingAddon.ProductImagePath = updatedAddon.ProductImagePath;
                existingAddon.ProductSalePrice = updatedAddon.ProductSalePrice;
                existingAddon.ProductPrice = updatedAddon.ProductPrice;

                _dbContext.SaveChanges();
            }

            return existingAddon;
        }

        public void DeleteAddon(int id)
        {
            var addon = _dbContext.Addons.Find(id);

            if (addon != null)
            {
                _dbContext.Addons.Remove(addon);
                _dbContext.SaveChanges();
            }
        }
    }
}
