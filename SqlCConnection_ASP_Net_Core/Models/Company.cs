﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Country { get; set; }

        public Company(int id, string name, int postCode, string city, string street, int houseNumber, string country)
        {
            Id = id;
            Name = name;
            PostCode = postCode;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Country = country;
        }
    }
}