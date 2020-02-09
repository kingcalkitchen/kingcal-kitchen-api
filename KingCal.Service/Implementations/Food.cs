using KingCal.Data;
using KingCal.Data.DTOs;
using KingCal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.Implementations
{
    //public class Food : IFood
    //{
    //    private readonly ILogger<Food> _logger;
    //    private readonly DataContext _context;

    //    public Food(ILogger<Food> logger, DataContext context) 
    //    {
    //        _logger = logger;
    //        _context = context;
    //    }

    //    public async IAsyncEnumerable<FoodDTO> GetAllAsync() 
    //    {
    //        await foreach (var food in _context.Foods.AsAsyncEnumerable())
    //        {
    //            if (food.DeletedDate is null) 
    //            {
    //                FoodDTO foodDTO = new FoodDTO
    //                {
    //                    Id = food.Id,
    //                    Name = food.Name,
    //                };
    //                yield return foodDTO;
    //            }
    //        }
    //    }

    //    public async Task<FoodDTO> GetByIdAsync(Guid id) 
    //    {
    //        Data.Entities.Food food = await _context.FindAsync<Data.Entities.Food>(id);

    //        if (food is null || food.DeletedDate != null)
    //            return new FoodDTO();

    //        return new FoodDTO 
    //        {
    //            Id = food.Id,
    //            Name = food.Name,
    //        };
    //    }

    //    public async Task<Guid> CreateAsync(FoodDTO foodDTO) 
    //    {
    //        Guid id = Guid.NewGuid();
    //        await _context.Foods.AddAsync(new Data.Entities.Food
    //        {
    //            Id = id,
    //            Name = foodDTO.Name,
    //            CreatedDate = DateTime.Now,
    //        });

    //        try
    //        {
    //            var response = await _context.SaveChangesAsync();
    //            if (response > 0)
    //                return id;
    //            else
    //                return Guid.Empty;
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError("There was an exception when attempting to create a Food.", ex);
    //            return Guid.Empty;
    //        }
    //    }

    //    public async Task<int> UpdateAsync(FoodDTO foodDTO)
    //    {
    //        Data.Entities.Food food = await _context.FindAsync<Data.Entities.Food>(foodDTO.Id);

    //        if (food is null)
    //            return -1;

    //        food.Name = foodDTO.Name;
    //        food.UpdatedDate = DateTime.Now;

    //        try
    //        {
    //            return await _context.SaveChangesAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError("There was an exception when attempting to update a Food.", ex);
    //            return -1;
    //        }
    //    }

    //    public async Task<int> DeleteAsync(Guid id) 
    //    {
    //        Data.Entities.Food food = await _context.FindAsync<Data.Entities.Food>(id);

    //        if (food is null)
    //            return -1;

    //        if (food.DeletedDate != null)
    //            return -2;

    //        food.DeletedDate = DateTime.Now;

    //        try
    //        {
    //            return await _context.SaveChangesAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError("There was an exception when attempting to delete a Food.", ex);
    //            return -3;
    //        }
    //    }
    //}
}
