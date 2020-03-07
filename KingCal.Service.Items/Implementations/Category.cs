using KingCal.Common.DTOs;
using KingCal.Data;
using KingCal.Service.Item.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KingCal.Service.Item.Implementations
{
    public class Category: ICategory
    {
        private readonly ILogger<Category> _logger;
        private readonly DataContext _context;

        public Category(ILogger<Category> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<CategoryDTO> GetAllAsync()
        {
            await foreach (var category in _context.Category.AsAsyncEnumerable())
            {
                if (category.DeletedDate is null)
                {
                    CategoryDTO categoryDTO = CloneCategoryEntity(category);

                    yield return categoryDTO;
                }
            }
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.Category category = await _context.FindAsync<Data.Entities.Category>(id);

            if (category is null || category.DeletedDate != null)
                return new CategoryDTO();

            return CloneCategoryEntity(category);
        }

        public async Task<Guid> CreateAsync(CategoryDTO categoryDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.Category.AddAsync(new Data.Entities.Category
            {
                Id = id,
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = categoryDTO.CreatedBy,
            });

            try
            {
                var response = await _context.SaveChangesAsync();
                if (response > 0)
                    return id;
                else
                    return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an exception when attempting to create a Category.", ex);
                return Guid.Empty;
            }
        }

        public CategoryDTO CloneCategoryEntity(Data.Entities.Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedDate = category.CreatedDate,
                CreatedBy = category.CreatedBy,
                UpdatedDate = category.UpdatedDate,
                UpdatedBy = category.UpdatedBy,
                DeletedDate = category.DeletedDate,
                DeletedBy = category.DeletedBy,
            };
            return categoryDTO;
        }
    }
}
