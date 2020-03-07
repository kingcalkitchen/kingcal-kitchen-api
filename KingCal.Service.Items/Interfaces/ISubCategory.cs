using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface ISubCategory
    {
        Task<Guid> CreateAsync(SubCategoryDTO SubCategoryDTO);

        IAsyncEnumerable<SubCategoryDTO> GetAllAsync();

        IAsyncEnumerable<SubCategoryDTO> GetByCategoryAsync(Guid categoryId);
        Task<SubCategoryDTO> GetByIdAsync(Guid id);
    }
}
