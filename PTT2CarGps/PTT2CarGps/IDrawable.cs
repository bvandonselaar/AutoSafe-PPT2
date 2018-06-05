using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps
{
    public interface IDrawable
    {
        void Draw(Graphics Canvas, Color Color);
    }
}
