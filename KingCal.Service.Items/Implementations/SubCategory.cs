using KingCal.Common.DTOs;
using KingCal.Data;
using KingCal.Service.Item.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Implementations
{
    public class SubCategory : ISubCategory
    {
        private readonly ILogger<SubCategory> _logger;
        private readonly DataContext _context;

        public SubCategory(ILogger<SubCategory> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<SubCategoryDTO> GetAllAsync()
        {
            await foreach (var subCategory in _context.SubCategory.AsAsyncEnumerable())
            {
                if (subCategory.DeletedDate is null)
                {
                    SubCategoryDTO subCategoryDTO = CloneSubCategoryEntity(subCategory);

                    yield return subCategoryDTO;
                }
            }
        }

        public async Task<SubCategoryDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.SubCategory subCategory = await _context.FindAsync<Data.Entities.SubCategory>(id);

            if (subCategory is null || subCategory.DeletedDate != null)
                return new SubCategoryDTO();

            return CloneSubCategoryEntity(subCategory);
        }

        public async IAsyncEnumerable<SubCategoryDTO> GetByCategoryAsync(Guid categoryId)
        {
            List<Data.Entities.SubCategory> subCategoryList = await _context.SubCategory.Where(x => x.CategoryId == categoryId).ToListAsync();

            foreach (var subCategory in subCategoryList)
            {
                if (subCategory.DeletedDate is null)
                {

                    SubCategoryDTO subCategoryDTO = CloneSubCategoryEntity(subCategory);

                    yield return subCategoryDTO;
                }
            };

        }

        public async Task<Guid> CreateAsync(SubCategoryDTO subCategoryDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.SubCategory.AddAsync(new Data.Entities.SubCategory
            {
                Id = id,
                Name = subCategoryDTO.Name,
                CategoryId = subCategoryDTO.CategoryId,
                Description = subCategoryDTO.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = subCategoryDTO.CreatedBy,
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
                _logger.LogError("There was an exception when attempting to create a SubCategory.", ex);
                return Guid.Empty;
            }
        }

        public SubCategoryDTO CloneSubCategoryEntity(Data.Entities.SubCategory subCategory)
        {
            SubCategoryDTO subCategoryDTO = new SubCategoryDTO
            {
                Id = subCategory.Id,
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name,
                Description = subCategory.Description,
                CreatedDate = subCategory.CreatedDate,
                CreatedBy = subCategory.CreatedBy,
                UpdatedDate = subCategory.UpdatedDate,
                UpdatedBy = subCategory.UpdatedBy,
                DeletedDate = subCategory.DeletedDate,
                DeletedBy = subCategory.DeletedBy,
    };
            return subCategoryDTO;
        }
    }
}
