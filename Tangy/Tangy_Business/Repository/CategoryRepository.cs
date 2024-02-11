using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);

            category.CreatedDate = DateTime.Now;

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public async Task<int> Delete(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category!=null)
            {
                _db.Categories.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return categoryDTO;
        }
    }
}
