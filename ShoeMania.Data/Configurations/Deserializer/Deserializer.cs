﻿using Newtonsoft.Json;
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
            List<DeliveryOffice> addresses = JsonConvert.DeserializeObject<List<DeliveryOffice>>(jsonString);

            return addresses;
        }

    }
}
