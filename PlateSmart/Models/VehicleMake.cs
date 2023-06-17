using System;
using System.Text.Json.Serialization;

namespace PlateSmart.Models
{
    public enum Make
    {
        acura, alfa_romeo, am_general, amc, aston_martin, audi,
        bentley, blakely, bmw, bradley, buick, cadillac, can_am, chevrolet, chrysler,
        citroen, daewoo, daihatsu, datsun, dodge, eagle, ferrari, fiat, ford, gmc,
        holden, honda, hummer, hyundai, infiniti, isuzu, jaguar, jeep, kia, lamborghini,
        land_rover, lexus, lincoln, lotus, maserati, mazda, mercedes_benz, mercury,
        mg, mini, mitsubishi, morris, nash, nissan, oldsmobile, opel, packard, peugeot,
        plymouth, polaris, pontiac, porsche, ram, rambler, renault, rolls_royce, saab,
        saturn, scion, smart, studebaker, subaru, sunbeam, suzuki, tesla, toyota,
        triumph, volkswagen, volvo, willys
    }
    public class VehicleMake
    {
        public string Name
        {
            get
            {
                return GetName();
            }
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Make Code { get; set; }

        public string GetName()
        {
            var result = "";
            string makeEnum = Enum.GetName(typeof(Make), Code);
            if (makeEnum.Contains("_"))
            {
                string[] strArray = makeEnum.Split('_');
                string str1 = strArray[0];
                string str2 = strArray[1];
                result = char.ToUpper(strArray[0][0]) + str1.Substring(1) + "-" + char.ToUpper(strArray[1][0]) + str2.Substring(1);
            }
            else
            {
                result = char.ToUpper(makeEnum[0]) + makeEnum.Substring(1);
            }


            return result;
        }

    }
}
