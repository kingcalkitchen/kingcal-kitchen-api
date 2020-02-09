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
    public class SubItem : ISubItem
    {
        private readonly ILogger<SubItem> _logger;
        private readonly DataContext _context;

        public SubItem(ILogger<SubItem> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<SubItemDTO> GetAllAsync()
        {
            await foreach (var subItem in _context.SubItem.AsAsyncEnumerable())
            {
                if (subItem.DeletedDate is null)
                {
                    SubItemDTO subItemDTO = CloneSubItemEntity(subItem);

                    yield return subItemDTO;
                }
            }
        }

        public async Task<SubItemDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.SubItem subItem = await _context.FindAsync<Data.Entities.SubItem>(id);

            if (subItem is null || subItem.DeletedDate != null)
                return new SubItemDTO();

            return CloneSubItemEntity(subItem);
        }

        public async Task<Guid> CreateAsync(SubItemDTO subItemDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.SubItem.AddAsync(new Data.Entities.SubItem
            {
                Id = id,
                CreatedDate = DateTime.Now,
                CreatedBy = subItemDTO.CreatedBy,
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
                _logger.LogError("There was an exception when attempting to create a SubItem.", ex);
                return Guid.Empty;
            }
        }


        public SubItemDTO CloneSubItemEntity(Data.Entities.SubItem subItem)
        {
            SubItemDTO subItemDTO = new SubItemDTO
            {
                Id = subItem.Id,
                CreatedDate = subItem.CreatedDate,
                CreatedBy = subItem.CreatedBy,
                UpdatedDate = subItem.UpdatedDate,
                UpdatedBy = subItem.UpdatedBy,
                DeletedDate = subItem.DeletedDate,
                DeletedBy = subItem.DeletedBy,
            };
            return subItemDTO;
        }

        public static implicit operator SubItem(Data.Entities.SubItem v)
        {
            throw new NotImplementedException();
        }
    }

}
