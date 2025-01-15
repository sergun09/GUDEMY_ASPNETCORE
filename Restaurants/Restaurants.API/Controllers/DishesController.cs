using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Queries.GetAllDishesByFromRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        int dishId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDishFromRestaurant), new { dishId, restaurantId }, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesFromRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await _mediator.Send(new GetAllDishesFromRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishFromRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId )
    {
        var dishes = await _mediator.Send(new GetDishFromRestaurantQuery(restaurantId, dishId));
        return Ok(dishes);
    }

    [HttpDelete("{dishId}")]
    public async Task<ActionResult<IEnumerable<DishDto>>> DeleteDishFromRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await _mediator.Send(new DeleteDishFromRestaurantCommand(restaurantId, dishId));
        return NoContent();
    }
}

