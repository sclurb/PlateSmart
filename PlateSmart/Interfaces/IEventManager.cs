using Microsoft.AspNetCore.Http;
using PlateSmart.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlateSmart.Interfaces
{
    public interface IEventManager
    {
        Task StoreImageInfo(AlprEvent alprEvents);
        Task<bool> SaveImage(HttpRequest request, string id, Int64 timeStamp, int width, int height, string category, string correlationId);
    }
}
