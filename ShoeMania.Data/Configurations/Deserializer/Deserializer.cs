using Newtonsoft.Json;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations.Deserializer
{
    public class Deserializer
    {
        public static List<DeliveryOffice> ImportAddresses(string jsonString)
        {
            List<DeliveryOffice> addresses = JsonConvert.DeserializeObject<List<DeliveryOffice>>(jsonString)!;

            List<DeliveryOffice> officesToImport = new();

            foreach (var address in addresses)
            {
                if (!IsValid(address))
                {
                    continue;
                }

                DeliveryOffice office = new DeliveryOffice()
                {
                    Address = address.Address,
                    Name = address.Name,
                };

                officesToImport.Add(office);
            }



            return officesToImport;
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }

    }
}
