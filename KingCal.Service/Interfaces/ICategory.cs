using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface ICategory
    {
        Task<Guid> CreateAsync(CategoryDTO categoryDTO);

        IAsyncEnumerable<CategoryDTO> GetAllAsync();

        Task<CategoryDTO> GetByIdAsync(Guid id);
    }
}
