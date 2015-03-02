using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevNet.Models
{
    public class Automobile
    {
        public Automobile()
        {

        }
        
        public Automobile(string make, string body_style, decimal wheel_base, decimal engine_size, decimal horsepower, decimal peak_rpm, decimal price)
        {
            this.Make = make;
            this.Body_Style = body_style;
            this.Wheel_Base = wheel_base;
            this.Engine_Size = engine_size;
            this.Horsepower = horsepower;
            this.Peak_Rpm = peak_rpm;
            this.Price = price;
        }

        public string Make { get; set; }
        public string Body_Style { get; set; }
        public decimal Wheel_Base { get; set; }
        public decimal Engine_Size { get; set; }
        public decimal Horsepower { get; set; }
        public decimal Peak_Rpm { get; set; }
        public decimal Price { get; set; }
    }
}

 