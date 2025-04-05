using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

namespace YourFavECommerce.Api.Services
{
    public class BrandServices : IBrandServices
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Brand Add(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            _dbContext.SaveChanges();

            return brand;
        }

        public bool Edit(int id, Brand brand)
        {
            Brand? brandInDb = _dbContext.Brands.AsNoTracking().FirstOrDefault(b=>b.Id == id);

            if (brandInDb != null)
            {
                brand.Id = id;
                _dbContext.Brands.Update(brand);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public Brand? Get(Expression<Func<Brand, bool>> expression) => _dbContext.Brands.FirstOrDefault(expression);

        public IEnumerable<Brand> GetAll() => _dbContext.Brands.ToList();

        public bool Remove(int id)
        {
            Brand? brandInDb = _dbContext.Brands.Find(id);

            if (brandInDb != null)
            {
                _dbContext.Brands.Remove(brandInDb);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }

}
