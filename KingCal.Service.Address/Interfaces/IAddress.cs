using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Address.Interfaces
{
    public interface IAddress
    {
        Task<Guid> CreateAsync(AddressDTO addressDTO);

        IAsyncEnumerable<AddressDTO> GetAllAsync();

        Task<AddressDTO> GetByIdAsync(Guid id);

        IAsyncEnumerable<AddressDTO> GetByPostalCodeAsync(int postalCode, int addressType);

        IAsyncEnumerable<AddressDTO> GetByCityStateAsync(string city, string state, int addressType);

        //Task<int> UpdateAsync(AddressDTO addressDTO);

        //Task<int> DeleteAsync(Guid id);
    }
}

