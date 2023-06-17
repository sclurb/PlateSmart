using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateSmart.Models;
using PlateSmart.Manager;
using PlateSmart.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace PlateSmart.Controllers
{

    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PlateSmartController : ControllerBase
    {
        private IEventManager _handleImages;

        public PlateSmartController(IEventManager handleImages)
        {
            this._handleImages = handleImages ?? throw new ArgumentNullException(nameof(EventManager));
        }

        [HttpPost("event")]
        public IActionResult Event([FromBody] List<AlprEvent> alprEvents)
        {

            var result = _handleImages.StoreImageInfo(alprEvents);
            if (result.IsCompletedSuccessfully)
            {
                return Ok(result.IsCompleted);
            }
            else
            {
                return BadRequest(result.Exception);
            }
        }

        [HttpPost("image/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostImage([FromRoute] string id)
        {
            if (await _handleImages.SaveImage(Request, id))
                return Accepted();
            else
                return BadRequest();


        }
    }
}
