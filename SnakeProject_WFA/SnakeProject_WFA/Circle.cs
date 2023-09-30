using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeProject_WFA
{
    internal class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } // Nouvelle propriété pour la largeur
        public int Height { get; set; } // Nouvelle propriété pour la hauteur
        public Circle() 
        {
            X = 0;
            Y = 0;
        }
    }
}
