using System;
using System.Collections.Generic;
using KingCal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using KingCal.Data;
using KingCal.Data.DTOs;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KingCal.Service.Implementations
{
    public class Property : IProperty
    {
        private readonly ILogger<Property> _logger;
        private readonly DataContext _context;

        public Property(ILogger<Property> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<PropertyDTO> GetAllAsync()
        {
            await foreach (var property in _context.Property.AsAsyncEnumerable())
            {
                if (property.DeletedDate is null)
                {
                    PropertyDTO propertyDTO = ClonePropertyEntity(property);

                    yield return propertyDTO;
                }
            }
        }

        public async IAsyncEnumerable<PropertyDTO> GetByItemAsync(Guid itemId)
        {
            List<Data.Entities.Property> propertyList = await _context.Property.Where(a => a.ItemId == itemId).ToListAsync();

            foreach (var property in propertyList)
            {
                if (property.DeletedDate is null)
                {
                    PropertyDTO propertyDTO = ClonePropertyEntity(property);

                    yield return propertyDTO;
                }
            }
        }

        public async Task<PropertyDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.Property property = await _context.FindAsync<Data.Entities.Property>(id);

            if (property is null || property.DeletedDate != null)
                return new PropertyDTO();

            return ClonePropertyEntity(property);
        }

        public async Task<Guid> CreateAsync(PropertyDTO propertyDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.Property.AddAsync(new Data.Entities.Property
            {
                Id = id,
                ItemId = propertyDTO.ItemId,
                Name = propertyDTO.Name,
                Value = propertyDTO.Value,
                Description = propertyDTO.Description,
                CreatedBy = propertyDTO.CreatedBy,
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
                _logger.LogError("There was an exception when attempting to create a Property.", ex);
                return Guid.Empty;
            }
        }

        public PropertyDTO ClonePropertyEntity(Data.Entities.Property property)
        {
            PropertyDTO propertyDTO = new PropertyDTO
            {
                Id = property.Id,
                ItemId = property.ItemId,
                Name = property.Name,
                Value = property.Value,
                Description = property.Description,
                CreatedDate = property.CreatedDate,
                CreatedBy = property.CreatedBy,
                UpdatedDate = property.UpdatedDate,
                UpdatedBy = property.UpdatedBy,
                DeletedDate = property.DeletedDate,
                DeletedBy = property.DeletedBy,
            };
            return propertyDTO;
        }
    }
}
