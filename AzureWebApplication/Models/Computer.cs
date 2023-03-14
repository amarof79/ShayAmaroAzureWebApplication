using System.ComponentModel.DataAnnotations;

namespace AzureWebApp.Models {
    public class Computer {
        //string? = string could be null

        //all keys in database
        public int Id { get; set; }
        public string? ComputerName { get; set; }
        public string? SerialNumber { get; set; }
        public string? Manufacturer { get; set; }

    }
}
