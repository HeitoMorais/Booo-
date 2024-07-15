using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste.Entities;

namespace teste.System
{
    public class GameManager
    {
        private readonly Timer _timer;
        private readonly SpriteFont _font;
        public GameManager()
        {
            _font = Globals.Content.Load<SpriteFont>("Font");
            _timer = new(Globals.Content.Load<Texture2D>("spr_arvore"), _font, new(300, 300), 5f);
            _timer.StartStop();
            _timer.Repeat = true;
        }

        public void Update()
        {
            InputManager.Update();
            _timer.Update();
        }

        public void Draw()
        {
            _timer.Draw();
        }
    }
}
