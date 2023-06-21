using Microsoft.AspNetCore.Http;
using PlateSmart.Interfaces;
using PlateSmart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PlateSmart.Manager
{
    public class EventManager : IEventManager
    {
        public Task StoreImageInfo(AlprEvent alprEvent)
        {
            string fileName = "C:\\Temp\\Image.csv";
            try
            {
                List<string> lines = new List<string>();
                if (!File.Exists(fileName))
                {
                    string alprHeader = "EventId, Event Type, Event Version, Event TimeStamp, ImageId, Image Width, Image Height, DeviceId, Device type, " +
                                        "SourceId, Source Type, Source Name, PLate Tag, Plate Code, Plate Region X Coordinate, Plate Region Y Coordinate, Plate Region Width, Plate Region Height, " +
                                        "Vehicle Color, Vehicle Make Name, Vehicle Make Code";
                    lines.Add(alprHeader);
                    lines.Add(GenerateCSVLine(alprEvent));
                    File.WriteAllLines(fileName, lines);
                }
                else
                {
                    lines.Add(GenerateCSVLine(alprEvent));
                    File.AppendAllLines(fileName, lines);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public async Task<bool> SaveImage(HttpRequest request, string id, Int64 timeStamp, int width, int height, string category, string correlationId)
        {
            var baseDirectory = $"C:\\Temp\\{id}";
            try
            {
                var completePathAndFileName = Path.ChangeExtension(baseDirectory, "jpg");
                if(!File.Exists(completePathAndFileName))
                {
                    using (var file = new FileStream(completePathAndFileName, FileMode.Create))
                    {
                        await request.Body.CopyToAsync(file);

                    }
                    return await Task.FromResult(true);
                }
                else 
                {
                    return await Task.FromResult(false);
                }

            }
            catch
            {
                return await Task.FromResult(false);
            }

        }

        private string GenerateCSVLine(AlprEvent alprEvent)
        {
            string csvLine = $"{alprEvent.Id}, {alprEvent.ALPR}, {alprEvent.Version}, {alprEvent.TimeStamp}, {alprEvent.Image.Id}, {alprEvent.Image.Width}," +
                $"{alprEvent.Image.Height}, {alprEvent.Device.Id}, {alprEvent.Device.Type}, {alprEvent.Source.Id}, {alprEvent.Source.Type}, {alprEvent.Source.Name}," +
                $"{alprEvent.Plate.Tag}, {alprEvent.Plate.Code}, {alprEvent.Plate.Region.X}, {alprEvent.Plate.Region.Y}, {alprEvent.Plate.Region.Width}, {alprEvent.Plate.Region.Height}," +
                $"{alprEvent.Vehicle.Color.Code}, {alprEvent.Vehicle.Make.Name}, {alprEvent.Vehicle.Make.Code} ";
            return csvLine;
        }
    }
}
