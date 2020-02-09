using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface IProperty
    {
        Task<Guid> CreateAsync(PropertyDTO propertyDTO);

        IAsyncEnumerable<PropertyDTO> GetAllAsync();

        Task<PropertyDTO> GetByIdAsync(Guid id);

    }
}
