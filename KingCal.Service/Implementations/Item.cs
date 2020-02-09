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
    public class Item : IItem
    {
        private readonly ILogger<Item> _logger;
        private readonly DataContext _context;

        public Item(ILogger<Item> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async IAsyncEnumerable<ItemDTO> GetAllAsync()
        {
            await foreach (var item in _context.Item.AsAsyncEnumerable())
            {
                if (item.DeletedDate is null)
                {
                    ItemDTO itemDTO = CloneItemEntity(item);

                    yield return itemDTO;
                }
            }
        }

        public async Task<ItemDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.Item item = await _context.FindAsync<Data.Entities.Item>(id);

            if (item is null || item.DeletedDate != null)
                return new ItemDTO();

            return CloneItemEntity(item);
        }

        public async Task<Guid> CreateAsync(ItemDTO itemDTO)
        {
            Guid id = Guid.NewGuid();
            await _context.Item.AddAsync(new Data.Entities.Item
            {
                Id = id,
                CreatedDate = DateTime.Now,
                CreatedBy = itemDTO.CreatedBy,
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
                _logger.LogError("There was an exception when attempting to create a Item.", ex);
                return Guid.Empty;
            }
        }


        public ItemDTO CloneItemEntity(Data.Entities.Item item)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Id = item.Id,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                UpdatedDate = item.UpdatedDate,
                UpdatedBy = item.UpdatedBy,
                DeletedDate = item.DeletedDate,
                DeletedBy = item.DeletedBy,
            };
            return itemDTO;
        }

        public static implicit operator Item(Data.Entities.Item v)
        {
            throw new NotImplementedException();
        }
    }

}
