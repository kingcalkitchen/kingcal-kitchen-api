using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface ISubItemProperty
    {
        Task<Guid> CreateAsync(SubItemPropertyDTO subItemPropertyDTO);

        IAsyncEnumerable<SubItemPropertyDTO> GetAllAsync();

        IAsyncEnumerable<SubItemPropertyDTO> GetBySubItemAsync(Guid subItemId);

        Task<SubItemPropertyDTO> GetByIdAsync(Guid id);

    }
}
