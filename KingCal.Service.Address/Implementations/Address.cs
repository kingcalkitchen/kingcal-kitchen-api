using KingCal.Common.DTOs;
using KingCal.Data;
using KingCal.Service.Address.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCal.Service.Address.Implementations
{
    public class Address : IAddress
    {
        private readonly ILogger<Address> _logger;
        private readonly DataContext _context;

        public Address(ILogger<Address> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<AddressDTO> GetAllAsync()
        {
            await foreach (var address in _context.Address.AsAsyncEnumerable())
            {
                if (address.DeletedDate is null)
                {
                    AddressDTO addressDTO = new AddressDTO
                    {
                        Id = address.Id,
                        Country = address.Country,
                        AdministrativeArea = address.AdministrativeArea,
                        SubAdministrativeArea = address.SubAdministrativeArea,
                        Locality = address.Locality,
                        PostalCode = address.PostalCode,
                        StreetAddress = address.StreetAddress,
                        Premise = address.Premise,
                        LandMark = address.LandMark,
                        CreatedDate = address.CreatedDate,
                        UpdatedDate = address.UpdatedDate,
                    };
                    yield return addressDTO;
                }
            }
        }

        public async Task<AddressDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.Address address = await _context.FindAsync<Data.Entities.Address>(id);

            if (address is null || address.DeletedDate != null)
                return new AddressDTO();

            return CloneAddressEntity(address);
        }

        public async IAsyncEnumerable<AddressDTO> GetByCityStateAsync(string city, string state, int addressType)
        {
            List<Data.Entities.Address> addressList = await _context.Address.Where(a => a.Locality.ToLower() == city.ToLower() && 
                                                                                        a.AdministrativeArea.ToLower() == state.ToLower() &&
                                                                                        a.AddressType == addressType).ToListAsync();

            foreach (var address in addressList)
            {
                if (address.DeletedDate is null)
                {

                    AddressDTO addressDTO = CloneAddressEntity(address);

                    yield return addressDTO;
                }
            };
        }
        public async IAsyncEnumerable<AddressDTO> GetByPostalCodeAsync(int postalCode, int addressType)
        {
            List<Data.Entities.Address> addressList = await _context.Address.Where(a => a.PostalCode == postalCode &&
                                                                                        a.AddressType == addressType).ToListAsync();
            
            foreach (var address in addressList)
            {
                if (address.DeletedDate is null)
                {
                    AddressDTO addressDTO = CloneAddressEntity(address);
                    yield return addressDTO;
                }
            };
        }

        public async Task<Guid> CreateAsync(AddressDTO addressDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.Address.AddAsync(new Data.Entities.Address
            {
                Id = id,
                Country = addressDTO.Country,
                AdministrativeArea = addressDTO.AdministrativeArea,
                SubAdministrativeArea = addressDTO.SubAdministrativeArea, 
                Locality = addressDTO.Locality,
                PostalCode = addressDTO.PostalCode,
                StreetAddress = addressDTO.StreetAddress, 
                Premise = addressDTO.Premise, 
                LandMark = addressDTO.LandMark, 
                CreatedDate = DateTime.Now,
            });

            try
            {
                var response = await _context.SaveChangesAsync();
                if (response > 0)
                    return id;
                else
                    return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an exception when attempting to create a Address.", ex);
                return Guid.Empty;
            }
        }

        public AddressDTO CloneAddressEntity(Data.Entities.Address address)
        {
            AddressDTO addressDTO = new AddressDTO
            {
                Id = address.Id,
                Country = address.Country,
                AdministrativeArea = address.AdministrativeArea,
                SubAdministrativeArea = address.SubAdministrativeArea,
                Locality = address.Locality,
                PostalCode = address.PostalCode,
                StreetAddress = address.StreetAddress,
                Premise = address.Premise,
                LandMark = address.LandMark,
                CreatedDate = address.CreatedDate,
                UpdatedDate = address.UpdatedDate,
                AddressType = address.AddressType,
            };
            return addressDTO;
        }



    }
}
