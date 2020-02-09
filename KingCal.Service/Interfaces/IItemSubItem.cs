using KingCal.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Interfaces
{
    public interface IItemSubItem
    {
        Task<Guid> AddSubItemToItem(Guid itemId, Guid subItemId);
    }
}
