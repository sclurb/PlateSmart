using Microsoft.AspNetCore.Http;
using PlateSmart.Interfaces;
using PlateSmart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlateSmart.Manager
{
    public class HandleImages : IHandleImages
    {
        public async Task<bool> StoreImageInfo(List<AlprEvent> alprEvents)
        {
     
            using (StreamWriter writer = new StreamWriter("C:\\Temp\\Image.csv"))
            {
                writer.WriteLine("EventId, Event Type, Event Version, Event TimeStamp, ImageId, Image Width, Image Height, DeviceId, Device type, " +
                    "SourceId, Source Type, Source Name, PLate Tag, Plate Code, Plate Region X Coordinate, Plate Region Y Coordinate, Plate Region Width, Plate Region Height, " +
                    "Vehicle Color, Vehicle Make Name, Vehicle Make Code");
                foreach (var alpr in alprEvents)
                {
                    writer.WriteLine($"{alpr.Id}, {alpr.ALPR.ToString()}, {alpr.Version}, {alpr.TimeStamp}, {alpr.Image.Id}, {alpr.Image.Width}," +
                        $"{alpr.Image.Height}, {alpr.Device.Id}, {alpr.Device.Type}, {alpr.Source.Id}, {alpr.Source.Type}, {alpr.Source.Name}," +
                        $"{alpr.Plate.Tag}, {alpr.Plate.Code}, {alpr.Plate.Region.X}, {alpr.Plate.Region.Y}, {alpr.Plate.Region.Width}, {alpr.Plate.Region.Height}," +
                        $"{alpr.Vehicle.Color.Code}, {alpr.Vehicle.Make.Name}, {alpr.Vehicle.Make.Code} ");
                }

            }

            return await Task.FromResult(true);
        }

        public async Task<bool> SaveImage(HttpRequest request, string id)
        {
            var baseDirectory = $"C:\\Temp\\{id}";
            var completePath = Path.ChangeExtension(baseDirectory, "jpg");
            using (var file = new FileStream(completePath, FileMode.Create))
            {
                await request.Body.CopyToAsync(file);
                Console.WriteLine($"FilePath: {completePath}");
            }
            return await Task.FromResult(true);
        }
    }
}
