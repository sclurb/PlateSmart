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
                //return Ok(result.IsCompleted);
                return Accepted(result.IsCompleted);
            }
            else if (result.IsCanceled)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Exception);
            }
        }

        [HttpPost("image/{id}/{timeStamp}/{width}/{height}/{category}/{correlationId}")]
        public async Task<IActionResult> PostImage([FromRoute] string id, Int64 timeStamp, int width, int height, string category, string correlationId)
        {
            if (await _handleImages.SaveImage(Request, id, timeStamp, width, height, category, correlationId))
                return Ok();
            else
                return BadRequest();
        }
    }
}
