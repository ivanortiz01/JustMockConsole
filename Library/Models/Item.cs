using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Item
    {
        public virtual string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }
    }
}
