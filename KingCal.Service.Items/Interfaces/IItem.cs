using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface IItem
    {
        Task<Guid> CreateAsync(ItemDTO itemDTO);
        IAsyncEnumerable<ItemDTO> GetAllAsync();
        IAsyncEnumerable<ItemDTO> GetAllWithSubTypeAsync();
        IAsyncEnumerable<ItemDTO> GetByIdAsync(Guid id);
    }
}
