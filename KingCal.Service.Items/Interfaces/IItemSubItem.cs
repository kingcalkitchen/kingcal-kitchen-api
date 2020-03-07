using KingCal.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Interfaces
{
    public interface IItemSubItem
    {
        Task<bool> AddSubItemToItemAsync(Guid itemId, Guid subItemId);

        IAsyncEnumerable<ItemSubItemDTO> GetSubItemByItemAsync(Guid itemId);
    }
}
