using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class NewEmployeeModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Hotels { get; set; }
        public bool Buses { get; set; }
        public bool Shops { get; set; }
        public bool? Status { get; set; }
        public bool HotelsTrain { get; set; }
        public bool BusesTrain { get; set; }
        public bool ShopsTrain { get; set; }
    }
}