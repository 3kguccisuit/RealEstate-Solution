using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Models.BaseModels
{
    public class Person
    {
        public string ID { get; set; }
        public virtual string Type => "Person";

    }
}
