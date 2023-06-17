using Microsoft.AspNetCore.Http;
using PlateSmart.Interfaces;
using PlateSmart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlateSmart.Manager
{
    public class EventManager : IEventManager
    {
        public Task StoreImageInfo(List<AlprEvent> alprEvents)
        {
            string fileName = "C:\\Temp\\Image.csv";
            try
            {
                if (!File.Exists(fileName))
                {
                    List<string> lines = new List<string>();
                    string alprHeader = "EventId, Event Type, Event Version, Event TimeStamp, ImageId, Image Width, Image Height, DeviceId, Device type, " +
                                        "SourceId, Source Type, Source Name, PLate Tag, Plate Code, Plate Region X Coordinate, Plate Region Y Coordinate, Plate Region Width, Plate Region Height, " +
                                        "Vehicle Color, Vehicle Make Name, Vehicle Make Code";
                    lines.Add(alprHeader);
                    foreach (var alpr in alprEvents)
                    {
                        string newLine = $"{alpr.Id}, {alpr.ALPR}, {alpr.Version}, {alpr.TimeStamp}, {alpr.Image.Id}, {alpr.Image.Width}," +
                                        $"{alpr.Image.Height}, {alpr.Device.Id}, {alpr.Device.Type}, {alpr.Source.Id}, {alpr.Source.Type}, {alpr.Source.Name}," +
                                        $"{alpr.Plate.Tag}, {alpr.Plate.Code}, {alpr.Plate.Region.X}, {alpr.Plate.Region.Y}, {alpr.Plate.Region.Width}, {alpr.Plate.Region.Height}," +
                                        $"{alpr.Vehicle.Color.Code}, {alpr.Vehicle.Make.Name}, {alpr.Vehicle.Make.Code} ";
                        lines.Add(newLine);
                    }
                    File.WriteAllLines(fileName, lines);
                }
                else
                {
                    List<string> newLines = new List<string>();
                    foreach (var alpr in alprEvents)
                    {
                        string newLine = $"{alpr.Id}, {alpr.ALPR}, {alpr.Version}, {alpr.TimeStamp}, {alpr.Image.Id}, {alpr.Image.Width}," +
                                        $"{alpr.Image.Height}, {alpr.Device.Id}, {alpr.Device.Type}, {alpr.Source.Id}, {alpr.Source.Type}, {alpr.Source.Name}," +
                                        $"{alpr.Plate.Tag}, {alpr.Plate.Code}, {alpr.Plate.Region.X}, {alpr.Plate.Region.Y}, {alpr.Plate.Region.Width}, {alpr.Plate.Region.Height}," +
                                        $"{alpr.Vehicle.Color.Code}, {alpr.Vehicle.Make.Name}, {alpr.Vehicle.Make.Code} ";
                        newLines.Add(newLine);

                    }
                    File.AppendAllLines(fileName, newLines);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public async Task<bool> SaveImage(HttpRequest request, string id)
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
    }
}
