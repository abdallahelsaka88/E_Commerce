using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Orders;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IOrderService
    {
        //Create Order : (BasketId, DeliveryMethodId, ShipToAddress) ==> return order
        Task<Result<OrderToReturn>> CreateOrderAsync(OrderDto dto, string email, CancellationToken ct);

        // Get All Product For Specific User
        Task<Result<IReadOnlyList<OrderToReturn>>> GetOrderForSpecificUserAsync(string email, CancellationToken ct);
        // Get All Product By Id For Specific User
        Task<Result<OrderToReturn>> GetOrderByIdForSpecificUserAsync(Guid orderId, string email, CancellationToken ct);

        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default);

    }
}
