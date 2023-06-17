using Microsoft.AspNetCore.Http;
using PlateSmart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlateSmart.Interfaces
{
    public interface IHandleImages
    {
        Task<bool> StoreImageInfo(List<AlprEvent> alprEvents);
        Task<bool> SaveImage(HttpRequest request, string id);
    }
}
