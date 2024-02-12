using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO ProductDTO)
        {
            var Product = _mapper.Map<ProductDTO, Product>(ProductDTO);

            await _db.Products.AddAsync(Product);
            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(Product);
        }

        public async Task<int> Delete(int id)
        {
            var Product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (Product!=null)
            {
                _db.Products.Remove(Product);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(p => p.Category));
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var Product = await _db.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (Product != null)
            {
                return _mapper.Map<Product, ProductDTO>(Product);
            }
            return new ProductDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO ProductDTO)
        {
            var Product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductDTO.Id);
            if (Product != null)
            {
                Product.Name = ProductDTO.Name;
                Product.Description = ProductDTO.Description;
                Product.ImageUrl = ProductDTO.ImageUrl;
                Product.CategoryId = ProductDTO.CategoryId;
                Product.Color = ProductDTO.Color;
                Product.ShopFavorites= ProductDTO.ShopFavorites;
                Product.CustomerFavorites= ProductDTO.CustomerFavorites;
                _db.Products.Update(Product);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(Product);
            }
            return ProductDTO;
        }
    }
}
