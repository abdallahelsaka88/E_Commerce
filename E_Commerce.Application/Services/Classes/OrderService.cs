using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Orders;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Application.Specification;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Contracts.Repositories;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class OrderService(
        IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork

        ) : IOrderService
    {
        public async Task<Result<OrderToReturn>> CreateOrderAsync(OrderDto dto, string email, CancellationToken ct)
        {
            //Create Order
            //Address
            var orderAddress = mapper.Map<OrderAddress>(dto.ShipToAddress);

            //Order Item
            //Get Basket By Id
            var basket = await basketRepository.GetBasketAsync(dto.BasketId, ct);
            if (basket is null)
                return Result<OrderToReturn>.Fail(Error.NotFound("Basket.NotFound", $"Basket With Id {dto.BasketId} Is Not Found"));

            var orderItems = new List<OrderItem>();

            //Convert BasketItem To OrderItem
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null)
                    return Result<OrderToReturn>.Fail(Error.NotFound("Basket Is Empty", $"Can Not Create Order With Basket Id {basket.Id}"));

                if (basket.Items.Count == 0)
                    return Result<OrderToReturn>.Fail(Error.Validation("Product.NotFound", $"Product With Id {item.Id} Is Not Found"));

                var productItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);

                if (product.Price != item.Price) product.Price = item.Price;

                var orderItem = new OrderItem(productItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);

            }

            //Get Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(dto.DeliveryMethodId, ct);
            if (deliveryMethod is null)
                return Result<OrderToReturn>.Fail(Error.Validation("DeliveryMethod.NotFound", $"DeliveryMethod With Id {dto.DeliveryMethodId} Is Not Found"));


            //Sub Total
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(email, orderAddress, orderItems, deliveryMethod, subTotal);
            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var count = await unitOfWork.SaveChangesAsync(ct);
            if (count <= 0)
                return Result<OrderToReturn>.Fail(Error.Failure("Create.Order.Failure", "Can Not Create This Order"));
            await basketRepository.DeleteBasketAsync(dto.BasketId, ct);
            var orderToReturn = mapper.Map<OrderToReturn>(order);
            return Result<OrderToReturn>.OK(orderToReturn);
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);
            if (deliveryMethods.Any())
            {
                return Result<IReadOnlyList<DeliveryMethodDto>>.OK(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
            }
            return Result<IReadOnlyList<DeliveryMethodDto>>.Fail(Error.NotFound("DeliveryMethods.NotFound", "NotFound Any DeliveryMethods "));
        }

        public async Task<Result<OrderToReturn>> GetOrderByIdForSpecificUserAsync(Guid orderId, string email, CancellationToken ct)
        {
            var spec = new OrderSpecifications(orderId, email);

            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec, orderId, ct);
            if (order is null)
                return Result<OrderToReturn>.Fail(Error.NotFound("Order.NotFound", "Order Is Not Found"));
            return Result<OrderToReturn>.OK(mapper.Map<OrderToReturn>(order));
        }

        public async Task<Result<IReadOnlyList<OrderToReturn>>> GetOrderForSpecificUserAsync(string email, CancellationToken ct)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email), ct);
            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturn>>.OK(mapper.Map<IReadOnlyList<OrderToReturn>>(orders));

            }
            return Result<IReadOnlyList<OrderToReturn>>.Fail(Error.NotFound("Orders.NotFound", $"User With Email {email} Is Not Found"));






        }
    }
}
