using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface ISubItemProperty
    {
        Task<Guid> CreateAsync(SubItemPropertyDTO subItemPropertyDTO);

        IAsyncEnumerable<SubItemPropertyDTO> GetAllAsync();

        IAsyncEnumerable<SubItemPropertyDTO> GetBySubItemAsync(Guid subItemId);

        Task<SubItemPropertyDTO> GetByIdAsync(Guid id);

    }
}
