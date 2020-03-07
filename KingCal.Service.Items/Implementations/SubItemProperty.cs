using KingCal.Common.DTOs;
using KingCal.Data;
using KingCal.Service.Item.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Implementations
{
    public class SubItemProperty : ISubItemProperty
    {
        private readonly ILogger<SubItemProperty> _logger;
        private readonly DataContext _context;

        public SubItemProperty(ILogger<SubItemProperty> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<SubItemPropertyDTO> GetAllAsync()
        {
            await foreach (var subItemProperty in _context.SubItemProperty.AsAsyncEnumerable())
            {
                if (subItemProperty.DeletedDate is null)
                {
                    SubItemPropertyDTO subItemPropertyDTO = CloneSubItemPropertyEntity(subItemProperty);

                    yield return subItemPropertyDTO;
                }
            }
        }

        public async IAsyncEnumerable<SubItemPropertyDTO> GetBySubItemAsync(Guid subItemId)
        {

            List<Data.Entities.SubItemProperty> subItemPropertyList = await _context.SubItemProperty.Where(a => a.SubItemId == subItemId).ToListAsync();

            foreach (var subItemProperty in subItemPropertyList)
            {
                if (subItemProperty.DeletedDate is null)
                {
                    SubItemPropertyDTO subItemPropertyDTO = CloneSubItemPropertyEntity(subItemProperty);

                    yield return subItemPropertyDTO;
                }
            }
        }

        public async Task<SubItemPropertyDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.SubItemProperty subItemProperty = await _context.FindAsync<Data.Entities.SubItemProperty>(id);

            if (subItemProperty is null || subItemProperty.DeletedDate != null)
                return new SubItemPropertyDTO();

            return CloneSubItemPropertyEntity(subItemProperty);
        }

        public async Task<Guid> CreateAsync(SubItemPropertyDTO subItemPropertyDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.SubItemProperty.AddAsync(new Data.Entities.SubItemProperty
            {
                Id = id,
                SubItemId = subItemPropertyDTO.SubItemId,
                Name = subItemPropertyDTO.Name,
                Value = subItemPropertyDTO.Value,
                Description = subItemPropertyDTO.Description,
                CreatedBy = subItemPropertyDTO.CreatedBy,
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
                _logger.LogError("There was an exception when attempting to create a SubItemProperty.", ex);
                return Guid.Empty;
            }
        }

        public SubItemPropertyDTO CloneSubItemPropertyEntity(Data.Entities.SubItemProperty subItemProperty)
        {
            SubItemPropertyDTO subItemPropertyDTO = new SubItemPropertyDTO
            {
                Id = subItemProperty.Id,
                SubItemId = subItemProperty.SubItemId,
                Name = subItemProperty.Name,
                Value = subItemProperty.Value,
                Description = subItemProperty.Description,
                CreatedDate = subItemProperty.CreatedDate,
                CreatedBy = subItemProperty.CreatedBy,
                UpdatedDate = subItemProperty.UpdatedDate,
                UpdatedBy = subItemProperty.UpdatedBy,
                DeletedDate = subItemProperty.DeletedDate,
                DeletedBy = subItemProperty.DeletedBy,
            };
            return subItemPropertyDTO;
        }
    }
}
