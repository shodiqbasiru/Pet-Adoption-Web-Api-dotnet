using System;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Mappers;

public static class StoreMapper
{
    public static StoreResponse ConvertToStoreResponse(this Store store)
    {
        return new StoreResponse
        {
            Id = store.Id,
            StoreName = store.StoreName,
            Rating = store.Rating,
            Address = store.Address,
            CreatedAt = store.CreatedAt,
            UpdatedAt = store.UpdatedAt,
            Products = store.Products?.ConvertToProductResponses()
        };
    }

    public static List<StoreResponse> ConvertToStoreResponses(this List<Store> stores)
    {
        return stores.Select(store => store.ConvertToStoreResponse()).ToList();
    }
}
