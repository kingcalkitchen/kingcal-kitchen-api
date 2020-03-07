using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface ISubItem
    {
        Task<Guid> CreateAsync(SubItemDTO subItemDTO);

        IAsyncEnumerable<SubItemDTO> GetAllAsync();

        IAsyncEnumerable<SubItemDTO> GetByIdAsync(Guid id);
    }
}
