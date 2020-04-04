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
    public class SubCategoryItem : ISubCategoryItem
    {
        private readonly ILogger<SubCategoryItem> _logger;
        private readonly DataContext _context;

        public SubCategoryItem(ILogger<SubCategoryItem> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<SubCategoryItemDTO> GetSubCategoryByItemAsync(Guid itemId)
        {
            List<Data.Entities.SubCategoryItem> subCategoryItemList = await _context.SubCategoryItem.Where(a => a.ItemId == itemId).ToListAsync();

            foreach (var subCategoryItem in subCategoryItemList)
            {
                SubCategoryItemDTO subCategoryItemDTO = CloneSubCategoryItemEntity(subCategoryItem);

                yield return subCategoryItemDTO;
            }
        }

        public async IAsyncEnumerable<SubCategoryItemDTO> GetItemBySubCategoryAsync(Guid subCategoryId)
        {
            List<Data.Entities.SubCategoryItem> subCategoryItemList = await _context.SubCategoryItem.Where(a => a.SubCategoryId == subCategoryId).ToListAsync();

            foreach (var subCategoryItem in subCategoryItemList)
            {
                SubCategoryItemDTO subCategoryItemDTO = CloneSubCategoryItemEntity(subCategoryItem);

                yield return subCategoryItemDTO;
            }
        }

        public async Task<bool> AddItemToSubCategoryAsync(Guid subCategoryId, Guid itemId)
        {
            await _context.SubCategoryItem.AddAsync(new Data.Entities.SubCategoryItem
            {
                ItemId = itemId,
                SubCategoryId = subCategoryId,
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
                _logger.LogError("There was an exception when attempting to create a SubCategoryItem.", ex);
                return false;
            }
        }

        public static implicit operator SubCategoryItem(Data.Entities.SubCategoryItem v)
        {
            throw new NotImplementedException();
        }

        public SubCategoryItemDTO CloneSubCategoryItemEntity(Data.Entities.SubCategoryItem subCategoryItem)
        {
            SubCategoryItemDTO subCategoryItemDTO = new SubCategoryItemDTO
            {
                ItemId = subCategoryItem.ItemId,
                SubCategoryId = subCategoryItem.SubCategoryId,
            };
            return subCategoryItemDTO;
        }
    }
}
