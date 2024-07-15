using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste.Graphics;

namespace teste.Entities
{
    public struct Ghost
    {
        public float GHOST_POSITION_X;
        public float GHOST_POSITION_Y;
        public float GHOST_SPEED_X;
        public float GHOST_SPEED_Y;
        
        public Ghost(float position_x, float position_y, float speed_x, float speed_y)
        {
            GHOST_POSITION_X = position_x;
            GHOST_POSITION_Y = position_y;
            GHOST_SPEED_X = speed_x;
            GHOST_SPEED_Y = speed_y;
        }
    }
}
