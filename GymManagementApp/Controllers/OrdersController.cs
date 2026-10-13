using E_Commerce.Application.DTOS.Orders;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymManagementApp.API.Controllers
{
    public class OrdersController(IOrderService orderService) : ApiBaseController
    {
        //POST Create Order => Order(BasketId, DeliveryMethodId, ShipToAddress)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturn>> CreateOrder(OrderDto orderDto, CancellationToken ct)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();
            var result = await orderService.CreateOrderAsync(orderDto, email, ct);
            return ToActionResult(result);
        }

        //GET Order Of User => [Email] --> Current User Orders For Logged In User
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturn>>> GetOrders(CancellationToken ct)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();
            var result = await orderService.GetOrderForSpecificUserAsync(email, ct);
            return ToActionResult(result);
        }



        //GET Order By Id Of User =>Id + [Email] --> Current User Orders For Logged In User
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturn>> GetOrderById(Guid id, CancellationToken ct)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();

            var result = await orderService.GetOrderByIdForSpecificUserAsync(id, email, ct);
            return ToActionResult(result);
        }





        //GET DeliveryMethod => List Of Delivery Methods
        [HttpGet(("deliveryMethod"))]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethods(CancellationToken ct = default)
        {
            var deliveryMethods = await orderService.GetAllDeliveryMethodsAsync(ct);

            return ToActionResult(deliveryMethods);
        }

    }

}
