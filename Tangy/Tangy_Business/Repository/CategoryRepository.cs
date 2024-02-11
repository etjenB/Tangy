﻿using AutoMapper;
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

        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);

            category.CreatedDate = DateTime.Now;

            _db.Categories.Add(category);
            _db.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public int Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category!=null)
            {
                _db.Categories.Remove(category);
                return _db.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public CategoryDTO GetById(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public CategoryDTO Update(CategoryDTO categoryDTO)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                _db.Categories.Update(category);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return categoryDTO;
        }
    }
}
