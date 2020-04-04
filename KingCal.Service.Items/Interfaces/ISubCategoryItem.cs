using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface ISubCategoryItem
    {
        Task<bool> AddItemToSubCategoryAsync(Guid subCategoryId, Guid itemId);

        IAsyncEnumerable<SubCategoryItemDTO> GetSubCategoryByItemAsync(Guid itemId);

        IAsyncEnumerable<SubCategoryItemDTO> GetItemBySubCategoryAsync(Guid subCategoryId);
    }
}
