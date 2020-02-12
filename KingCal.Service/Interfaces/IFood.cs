using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface IFood
    {
        IAsyncEnumerable<FoodDTO> GetAllAsync();

        Task<FoodDTO> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(FoodDTO foodDTO, Guid currentUserId);

        Task<int> UpdateAsync(FoodDTO foodDTO);

        Task<int> DeleteAsync(Guid id);
    }
}
