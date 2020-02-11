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
        private readonly IProperty _propertyService;

        public Item(ILogger<Item> logger, DataContext context, IProperty propertyService)
        {
            _logger = logger;
            _context = context;
            _propertyService = propertyService;
        }

        public async IAsyncEnumerable<ItemDTO> GetAllAsync()
        {
            /*
            var itemWithPropAndSubItems = await _context.Item.Join(
                                                            _context.ItemSubItem,
                                                            item => item.Id,
                                                            itemSubItem => itemSubItem.Item.Id,
                                                            (item, itemSubItem) => new
                                                            {
                                                                SubItemId = itemSubItem.SubItemId,
                                                                ItemId = item.Id,
                                                                ItemDeletedDate = item.DeletedDate,

                                                            }
                                                            ).ToListAsync();
            */

            List<Data.Entities.Item> itemList = await _context.Item.ToListAsync();

            foreach (var item in itemList)
            {
                if (item.DeletedDate is null)
                {
                    IAsyncEnumerable<PropertyDTO> propertyDTOList = _propertyService.GetByItemAsync(item.Id);

                    IAsyncEnumerable<ItemDTO> itemDTO = CloneItemEntity(item, propertyDTOList);

                    await foreach (var tempitem in itemDTO)
                    {
                        yield return tempitem;
                    }
                }

            }
        }

        public async IAsyncEnumerable<ItemDTO> GetAllWithSubTypeAsync()
        {
            /*
            var itemWithPropAndSubItems = await _context.Item.Join(
                                                            _context.ItemSubItem,
                                                            item => item.Id,
                                                            itemSubItem => itemSubItem.Item.Id,
                                                            (item, itemSubItem) => new
                                                            {
                                                                SubItemId = itemSubItem.SubItemId,
                                                                ItemId = item.Id,
                                                                ItemDeletedDate = item.DeletedDate,

                                                            }
                                                            ).ToListAsync();
            */

            List<Data.Entities.Item> itemList = await _context.Item.ToListAsync();

            foreach (var item in itemList)
            {
                if (item.DeletedDate is null)
                {
                    IAsyncEnumerable<PropertyDTO> propertyDTOList = _propertyService.GetByItemAsync(item.Id);

                    

                    IAsyncEnumerable<ItemDTO> itemDTO = CloneItemEntity(item, propertyDTOList);

                    await foreach (var tempitem in itemDTO)
                    {
                        yield return tempitem;
                    }
                }

            }
        }


        public async IAsyncEnumerable<ItemDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.Item item = await _context.FindAsync<Data.Entities.Item>(id);

            if (item is null || item.DeletedDate != null)
                yield return new ItemDTO();

            IAsyncEnumerable<PropertyDTO> propertyDTOList = _propertyService.GetByItemAsync(item.Id);

            await foreach (var tempitem in CloneItemEntity(item, propertyDTOList))
            {
                yield return tempitem;
            }
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


        public async IAsyncEnumerable<ItemDTO> CloneItemEntity(Data.Entities.Item item, IAsyncEnumerable<PropertyDTO> propertyDTOList)
        {

            List<PropertyDTO> propertyList = new List<PropertyDTO>();

            await foreach (var property in propertyDTOList)
            {
                propertyList.Add(property);
            };

            ItemDTO itemDTO = new ItemDTO
            {
                Id = item.Id,
                PropertyList = propertyList,
                CreatedDate = item.CreatedDate,
                CreatedBy =  item.CreatedBy,
                UpdatedDate = item.UpdatedDate,
                UpdatedBy = item.UpdatedBy,
                DeletedDate = item.DeletedDate,
                DeletedBy = item.DeletedBy,
            };

            yield return itemDTO;
        }

        public static implicit operator Item(Data.Entities.Item v)
        {
            throw new NotImplementedException();
        }
    }

}
