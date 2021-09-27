using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalendar
{
    class cls_theme
    {
        public int id;
        public string name;
        public Color color1;
        public Color color2;
        public Color color3;
        public Color foreColor;
        public bool hasTextShadow;

        public cls_theme(int _id, string _name, Color _color1, Color _color2, Color _color3, Color _foreColor, bool _hasTextShadow)
        {
            id = _id;
            name = _name;
            color1 = _color1;
            color2 = _color2;
            color3 = _color3;
            foreColor = _foreColor;
            hasTextShadow = _hasTextShadow;
        }
    }
}
