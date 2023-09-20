using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeProject_WFA
{
    internal class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static string direction;

        public Settings() 
        {
            Width= 16;
            Height= 16;
            direction = "left";
        }



    }
}
