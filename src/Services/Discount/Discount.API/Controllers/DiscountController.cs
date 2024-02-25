using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {

            var coupon = await _repository.GetDiscount(productName);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof (Coupon), 200)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtAction("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        [HttpDelete("{productonName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
        }

    }
}
