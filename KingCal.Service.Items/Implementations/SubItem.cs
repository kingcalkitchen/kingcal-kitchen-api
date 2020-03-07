using KingCal.Common.DTOs;
using KingCal.Data;
using KingCal.Service.Item.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Item.Implementations
{
    public class SubItem : ISubItem
    {
        private readonly ILogger<SubItem> _logger;
        private readonly DataContext _subItemContext;
        private readonly ISubItemProperty _subItemPropertyService;
        
        public SubItem(ILogger<SubItem> logger, 
                        DataContext subItemContext, 
                        ISubItemProperty subItemPropertyService)
        {
            _logger = logger;
            _subItemContext = subItemContext;
            _subItemPropertyService = subItemPropertyService;
        }

        public async IAsyncEnumerable<SubItemDTO> GetAllAsync()
        {

            /*var subItemWithProp = await _context.SubItem.Join(
                                                _context.SubItemProperty,
                                                subItem => subItem.Id,
                                                subItemProperty => subItemProperty.SubItem.Id,
                                                (subItem, subItemProperty) => new
                                                {
                                                    SubItemId = subItemProperty.SubItemId,
                                                    SubItemPropId = subItemProperty.Id,
                                                    SubItemDeletedDate = subItem.DeletedDate,
                                                    SubItemPropDeletedDate = subItemProperty.DeletedDate
                                                }
                                                ).Where(x => x.SubItemDeletedDate == null && 
                                                             x.SubItemPropDeletedDate == null).ToListAsync();
*/
            List<Data.Entities.SubItem> subItemList = await _subItemContext.SubItem.ToListAsync();


            foreach (var subItem in subItemList)
            {
                if (subItem.DeletedDate is null)
                {
                    IAsyncEnumerable<SubItemPropertyDTO> subItemPropertyDTOList = _subItemPropertyService.GetBySubItemAsync(subItem.Id);

                    IAsyncEnumerable<SubItemDTO> subItemDTO = CloneSubItemEntity(subItem, subItemPropertyDTOList);

                    await foreach (var item in subItemDTO)
                    {
                        yield return item;
                    }
                    
                }
            }
        }

        
        public async IAsyncEnumerable<SubItemDTO> GetByIdAsync(Guid id)
        {
            Data.Entities.SubItem subItem = await _subItemContext.FindAsync<Data.Entities.SubItem>(id);

            if (subItem is null || subItem.DeletedDate != null)
                yield return new SubItemDTO();

            IAsyncEnumerable<SubItemPropertyDTO> subItemPropertyDTOList = _subItemPropertyService.GetBySubItemAsync(subItem.Id);

            await foreach(var item in CloneSubItemEntity(subItem, subItemPropertyDTOList))
            {
                yield return item;
            }
            
        }

        public async Task<Guid> CreateAsync(SubItemDTO subItemDTO)
        {
            Guid id = Guid.NewGuid();
            await _subItemContext.SubItem.AddAsync(new Data.Entities.SubItem
            {
                Id = id,
                CreatedDate = DateTime.Now,
                CreatedBy = subItemDTO.CreatedBy,
            });

            try
            {
                var response = await _subItemContext.SaveChangesAsync();
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
        
        public async IAsyncEnumerable<SubItemDTO> CloneSubItemEntity(Data.Entities.SubItem subItem, IAsyncEnumerable<SubItemPropertyDTO> subItemPropertyDTOList)
        {

            List<SubItemPropertyDTO> subItemPropertyList = new List<SubItemPropertyDTO>();

            await foreach (var subItemProperty in subItemPropertyDTOList)
            {
                subItemPropertyList.Add(subItemProperty);
            };

            SubItemDTO subItemDTO = new SubItemDTO
            {
                Id = subItem.Id,
                SubItemPropertyList = subItemPropertyList,
                CreatedDate = subItem.CreatedDate,
                CreatedBy = subItem.CreatedBy,
                UpdatedDate = subItem.UpdatedDate,
                UpdatedBy = subItem.UpdatedBy,
                DeletedDate = subItem.DeletedDate,
                DeletedBy = subItem.DeletedBy,
            };

            yield return subItemDTO;
        }

        public static implicit operator SubItem(Data.Entities.SubItem v)
        {
            throw new NotImplementedException();
        }
    }

}
