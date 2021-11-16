using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.Model
{
    public class Colors
    {
        public List<Color> ListOfColors { get; set; }
        public Colors()
        {
            ListOfColors = new List<Color>
            {
                new Color
                {
                    Name = "Blue",
                    Hex = "#1bc0ff",
                    Hover = "#f4fcff"
                },

                new Color
                {
                    Name = "Pink",
                    Hex = "#ffa6bc",
                    Hover = "#fffbfc"
                },

                new Color
                {
                    Name = "Turquoise",
                    Hex = "#50e3c1",
                    Hover = "#f5fefb"
                },
                new Color
                {
                    Name = "Royal Blue",
                    Hex = "#5a71d9",
                    Hover = "#b4bad4"
                },
                new Color
                {
                    Name = "Orange",
                    Hex = "#ff9d4c",
                    Hover = "#fff7f0"
                },
                new Color
                {
                    Name = "Purple",
                    Hex = "#aa61fe",
                    Hover = "#f8f5fe"
                },
                new Color
                {
                    Name = "Green",
                    Hex = "#b8e986",
                    Hover = "#dbe6d1"
                }
            };
        }
    }
}
