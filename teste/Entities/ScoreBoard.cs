using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste.Entities
{
    public class ScoreBoard : IGameEntity
    {
        private const int TEXTURE_COORDS_NUMBER_WHIDTH = 7;
        private const int TEXTURE_COORDS_NUMBER_HEIGHT = 7;

        private Texture2D _texture;
        public double Score { get; set; }

        public int DisplayScore => (int)Math.Floor(Score);

        public int HighSocre {  get; set; }

        public int DrawOrder => 100;

        public Vector2 Position { get; set; }

        public ScoreBoard(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int[] scoreDigits = SplitDigits(DisplayScore);

            float posX = Position.X;

            foreach(int digit in scoreDigits)
            {
                Rectangle  textureCoords = GetDigitTextureBounds(digit);

                Vector2 screenPos = new Vector2(posX, Position.Y);

                spriteBatch.Draw(_texture, screenPos, textureCoords, Color.White);

                posX += TEXTURE_COORDS_NUMBER_WHIDTH;
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        //Metodo usado para separar os valores do score individualmente
        private int[] SplitDigits(int input)
        {
            string inputStr = input.ToString();

            int[] result = new int[inputStr.Length];

            for(int i = 0; i < result.Length; i++)
            {
                result[i] = (int)char.GetNumericValue(inputStr[i]);
            }

            return result;
        }

        private Rectangle GetDigitTextureBounds(int digit)
        {
            if(digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException("digit", "The value of digit must be between 0 and 9.");
            }
            int posX = 0 + digit * TEXTURE_COORDS_NUMBER_WHIDTH;
            int posY = 0;

            return new Rectangle(posX, posY, TEXTURE_COORDS_NUMBER_WHIDTH, TEXTURE_COORDS_NUMBER_HEIGHT);
        }
    }
}
