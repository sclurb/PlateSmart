using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateSmart.Models;
using PlateSmart.Manager;
using PlateSmart.Interfaces;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace PlateSmart.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PlateTestController : ControllerBase
    {
        private IHandleImages _handleImages;

        public PlateTestController(IHandleImages handleImages)
        {
            this._handleImages = handleImages ?? throw new ArgumentNullException(nameof(HandleImages));
        }

        [HttpPost("imageInfo")]
        public IActionResult ImageInfo([FromBody] List<AlprEvent> alprEvents)
        {
           var result =  _handleImages.StoreImageInfo(alprEvents);
            if (result.IsCompleted)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("image/{id}")]
        public async Task<IActionResult> PostImage([FromRoute] string id)
        {

            var result = await _handleImages.SaveImage(Request, id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
