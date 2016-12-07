using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZMultipleForms
{
    class Shape
    {
        public enum Item
        {
            Rectangle, Ellipse,Any
        }
        public Item curentItem;
        List<int> list_Points;
        public Shape(Item currentItem, List<int> list_Points)
        {
            this.curentItem = currentItem;
            this.list_Points = list_Points;
        }
        
    }
}
