using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste.Graphics;

namespace teste.Entities
{
    public class Player : IGameEntity
    {
        public const int PLAYER_DEFAULT_SPRITE_POS_X = 0;
        public const int PLAYER_DEFAULT_SPRITE_POS_Y = 0;
        public const int PLAYER_DEFAULT_SPRITE_WIDTH = 18;
        public const int PLAYER_DEFAULT_SPRITE_HEIGHT = 18;
        public const int PLAYER_SPRITE_TAM_WIDTH = 40;
        public const int PLAYER_SPRITE_TAM_HEIGHT = 40;
        public int PLAYER_POSITION_X = 500;
        public int PLAYER_POSITION_Y = 350;

        public Sprite Sprite {  get; private set; }

        public PlayerState State { get; private set; }

        public Rectangle Position { get; set; }

        public bool IsAlive { get; private set; }

        public float Speed { get; private set; }

        public int DrawOrder { get; set; }

        public Player(Texture2D spriteSheet, Rectangle position) 
        {
            Sprite = new Sprite(spriteSheet, PLAYER_DEFAULT_SPRITE_POS_X, PLAYER_DEFAULT_SPRITE_POS_Y, PLAYER_DEFAULT_SPRITE_WIDTH, PLAYER_DEFAULT_SPRITE_HEIGHT);
            Position = new Rectangle(PLAYER_POSITION_X, PLAYER_POSITION_Y, PLAYER_SPRITE_TAM_WIDTH, PLAYER_SPRITE_TAM_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, this.Position);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
