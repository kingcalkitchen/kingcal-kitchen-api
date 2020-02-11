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
    public class ItemSubItem : IItemSubItem
    {
        private readonly ILogger<ItemSubItem> _logger;
        private readonly DataContext _context;

        public ItemSubItem(ILogger<ItemSubItem> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*        public async IAsyncEnumerable<ItemSubItemDTO> GetAllAsync()
                {
                    await foreach (var itemSubItem in _context.ItemSubItem.AsAsyncEnumerable())
                    {
                        if (itemSubItem.DeletedDate is null)
                        {
                            ItemSubItemDTO itemSubItemDTO = CloneItemSubItemEntity(itemSubItem);

                            yield return itemSubItemDTO;
                        }
                    }
                }

                public async Task<ItemSubItemDTO> GetByIdAsync(Guid id)
                {
                    Data.Entities.ItemSubItem itemSubItem = await _context.FindAsync<Data.Entities.ItemSubItem>(id);

                    if (itemSubItem is null || itemSubItem.DeletedDate != null)
                        return new ItemSubItemDTO();

                    return CloneItemSubItemEntity(itemSubItem);
                }
        */

        public async Task<bool> AddSubItemToItemAsync(Guid itemId, Guid subItemId)
        {
            await _context.ItemSubItem.AddAsync(new Data.Entities.ItemSubItem
            {
                ItemId = itemId,
                SubItemId = subItemId,
            });

            try
            {
                var response = await _context.SaveChangesAsync();
                if (response > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an exception when attempting to create a ItemSubItem.", ex);
                return false;
            }
        }

        public static implicit operator ItemSubItem(Data.Entities.ItemSubItem v)
        {
            throw new NotImplementedException();
        }
    }

}
