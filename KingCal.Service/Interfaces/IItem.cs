using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface IItem
    {
        Task<Guid> CreateAsync(ItemDTO itemDTO);

        IAsyncEnumerable<ItemDTO> GetAllAsync();

        Task<ItemDTO> GetByIdAsync(Guid id);

    }
}
