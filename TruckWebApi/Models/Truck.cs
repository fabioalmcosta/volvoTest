using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TruckWebApi.Models
{
    public class Truck
    {
        /// <summary>
        /// id of the truck
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Model of the truck
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Model Year of the truck
        /// </summary>
        public int ModelYear { get; set; }
        /// <summary>
        /// Manufacture Year of the truck
        /// </summary>
        public int ManufacYear { get; set; }
        /// <summary>
        /// NickName of the truck
        /// </summary>
        public string NickName { get; set; }
    }
}
