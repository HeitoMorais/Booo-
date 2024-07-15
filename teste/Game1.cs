using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using System;
using teste.Graphics;
using teste.Entities;
using teste.System;

namespace teste
{
    public partial class Game1 : Game
    {
        //Definição dos "nomes" dos sprites
        private const string spr_player = "spr_player";
        private const string spr_ghost = "spr_fantasma";
        private const string asset_floor = "asset_floor_comp";
        private const string spr_shadow = "spr_personagem_sombra";
        private const string menu = "menu";
        private const string pause = "pausa";
        private const string score = "asset_num";
        private const string gameover = "game_over";

        //timer
        SpriteFont font;
        Vector2 position_font;
        Vector2 position_surv_time;
        private string _text;
        private string _surv_time;
        private double _time;

        //Declaração das outras pastas
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private KeyboardState _previusKeyboardState;
        public GameState State { get; private set; }
        public GameState Nivel { get; private set; }
        private EntityManager _entityManager;
        private ScoreBoard _scoreBoard;

        //Declaração dos sprites
        private Texture2D _player_texture;
        private Texture2D _ghost_texture;
        private Texture2D _floor_texture;
        private Texture2D _shadow_texture;
        private Texture2D _menu_texture;
        private Texture2D _pause_texture;
        private Texture2D _score_texture;
        private Texture2D _GameOver_texture;

        //Definição do tamanho da tela
        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 700;

        //Player: Tamanho do sprite; Posição do personagem na tela; Velociade de deslocamento
        public const int PLAYER_TAM_WIDTH = 40;
        public const int PLAYER_TAM_HEIGHT = 40;

        public float PLAYER_POSITION_X = 500;
        public float PLAYER_POSITION_Y = 350;

        public float PLAYER_SPEED = 110;

        //Constantes do fantasma
        public const int GHOST_TAM_WIDTH = 40;
        public const int GHOST_TAM_HEIGHT = 40;

        public const float INITIAL_GHOST_SPEED = 110;

        //Posição do fantasma 1
        public float GHOST_1_POSITION_X = 100;
        public float GHOST_1_POSITION_Y = 300;

        //Variavel da velocidade do fantasma 1
        public float GHOST_1_SPEED_X = INITIAL_GHOST_SPEED;
        public float GHOST_1_SPEED_Y = INITIAL_GHOST_SPEED;

        //Posição do fantasma 2
        public float GHOST_2_POSITION_X = 900;
        public float GHOST_2_POSITION_Y = 300;

        //Variavel da velocidade do fantasma 2
        public float GHOST_2_SPEED_X = INITIAL_GHOST_SPEED;
        public float GHOST_2_SPEED_Y = INITIAL_GHOST_SPEED;

        //Posição do fantasma 3
        public float GHOST_3_POSITION_X = 500;
        public float GHOST_3_POSITION_Y = 100;

        //Variavel da velocidade do fantasma 3
        public float GHOST_3_SPEED_X = INITIAL_GHOST_SPEED;
        public float GHOST_3_SPEED_Y = INITIAL_GHOST_SPEED;

        //Posição do fantasma 4
        public float GHOST_4_POSITION_X = 900;
        public float GHOST_4_POSITION_Y = 600;

        //Variavel da velocidade do fantasma 4
        public float GHOST_4_SPEED_X = INITIAL_GHOST_SPEED;
        public float GHOST_4_SPEED_Y = INITIAL_GHOST_SPEED;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
            State = GameState.Initial;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.ApplyChanges();

            position_font = new Vector2 (500, 5);
            position_surv_time = new Vector2 (250, 350);
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player_texture = Content.Load<Texture2D>(spr_player);

            _ghost_texture = Content.Load<Texture2D>(spr_ghost);

            _floor_texture = Content.Load<Texture2D>(asset_floor);

            _shadow_texture = Content.Load<Texture2D>(spr_shadow);

            _menu_texture = Content.Load<Texture2D>(menu);

            _pause_texture = Content.Load<Texture2D>(pause);

            _score_texture = Content.Load<Texture2D>(score);

            _GameOver_texture = Content.Load<Texture2D>(gameover);

            _player = new Player(_player_texture, new Rectangle(0, 0, 40, 40));
            _player.Position = new Rectangle((int)PLAYER_POSITION_X - PLAYER_TAM_WIDTH, (int)PLAYER_POSITION_Y - PLAYER_TAM_HEIGHT, PLAYER_TAM_WIDTH, PLAYER_TAM_HEIGHT);

            _scoreBoard = new ScoreBoard(_score_texture, new Vector2(WINDOW_WIDTH, 10));
            _scoreBoard.Score = 498;

            font = Content.Load<SpriteFont>("Font");

            _entityManager.AddEntity(_scoreBoard);

            if(State == GameState.Nivel2)
            {
                GHOST_1_SPEED_X += 100;
                GHOST_1_SPEED_Y += 100;
                GHOST_2_SPEED_X += 100;
                GHOST_2_SPEED_Y += 100;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            //Menu
            if (State == GameState.Initial && Keyboard.GetState().IsKeyDown(Keys.C) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                State = GameState.Nivel1;
                Nivel = GameState.Nivel1;
            }
            else if (State == GameState.Nivel1 && Keyboard.GetState().IsKeyDown(Keys.P) || State == GameState.Nivel2 && Keyboard.GetState().IsKeyDown(Keys.P) || State == GameState.Nivel3 && Keyboard.GetState().IsKeyDown(Keys.P))
            {
                State = GameState.Pause;
            }
            else if (State == GameState.Pause && Keyboard.GetState().IsKeyDown(Keys.X))
            {
                State = GameState.Initial;
                _time = 0;
            }
            else if (State == GameState.Pause && Keyboard.GetState().IsKeyDown(Keys.C))
            {
                State = GameState.Initial;
                State = Nivel;
            }
            else if(State == GameState.GameOver)
            {
                _surv_time = _text;
                _time = 0;
            }
            else if(State == GameState.GameOver && Keyboard.GetState().IsKeyDown(Keys.C))
            {
                State = GameState.Nivel1;
                Nivel = GameState.Nivel1;
            }
            else if (State == GameState.GameOver && Keyboard.GetState().IsKeyDown(Keys.X))
            {
                State = GameState.Initial;
                Nivel = GameState.Initial;
            }
            else if(State == GameState.Initial && Keyboard.GetState().IsKeyDown(Keys.Escape) || State == GameState.Pause && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if(State == GameState.Initial)
            {
                PLAYER_POSITION_X = 500;
                PLAYER_POSITION_Y = 350;

                GHOST_1_POSITION_Y = 300;
                GHOST_1_POSITION_X = 100;

                GHOST_2_POSITION_Y = 300;
                GHOST_2_POSITION_X = 900;

                GHOST_3_POSITION_Y = 100;
                GHOST_3_POSITION_X = 500;

                GHOST_4_POSITION_Y = 600;
                GHOST_4_POSITION_X = 500;
            }

            //Inciar o jogo
            if (State == GameState.Nivel1 || State == GameState.Nivel2 || State == GameState.Nivel3)
            {
                //timer
                _time += gameTime.ElapsedGameTime.TotalMilliseconds;
                _text = string.Format("{0:hh\\mm\\ss\\.fff}", Convert.ToString(Math.Round(_time / 1000)));

                if (State == GameState.Nivel1 && _time >= 60000 && _time < 120000)
                {
                    State = GameState.Nivel2;
                    Nivel = GameState.Nivel2;
                }
                else if (State == GameState.Nivel2 && _time >= 120000)
                {
                    State = GameState.Nivel3;
                    Nivel = GameState.Nivel3;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D)) //Movimentação em retas
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X += (PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X -= (PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_Y += (PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_Y -= (PLAYER_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.W)) //Movimentação nas diagonais
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X += ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    PLAYER_POSITION_Y -= ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X -= ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    PLAYER_POSITION_Y -= ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X += ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    PLAYER_POSITION_Y += ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    PLAYER_SPEED = 110;
                    PLAYER_POSITION_X -= ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    PLAYER_POSITION_Y += ((PLAYER_SPEED / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds);

                }

                //Fixando o personagem dentro dos limites
                float Xmax_player = WINDOW_WIDTH;
                float Xmin_player = 0 + PLAYER_TAM_WIDTH;

                float Ymax_player = WINDOW_HEIGHT;
                float Ymin_player = 0 + PLAYER_TAM_HEIGHT;

                if (PLAYER_POSITION_X > Xmax_player)
                {
                    PLAYER_SPEED = 0;
                    PLAYER_POSITION_X = Xmax_player;
                }
                else if (PLAYER_POSITION_X < Xmin_player)
                {
                    PLAYER_SPEED = 0;
                    PLAYER_POSITION_X = Xmin_player;
                }
                else if (PLAYER_POSITION_Y > Ymax_player)
                {
                    PLAYER_SPEED = 0;
                    PLAYER_POSITION_Y = Ymax_player;
                }
                else if (PLAYER_POSITION_Y < Ymin_player)
                {
                    PLAYER_SPEED = 0;
                    PLAYER_POSITION_Y = Ymin_player;
                }

                //Movimentação do fantasma
                GHOST_1_POSITION_X += GHOST_1_SPEED_X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                GHOST_1_POSITION_Y += GHOST_1_SPEED_Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                GHOST_2_POSITION_X -= GHOST_2_SPEED_X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                GHOST_2_POSITION_Y -= GHOST_2_SPEED_Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Fixando o fantasma dentro dos limites
                float Xmax_ghost = WINDOW_WIDTH - GHOST_TAM_WIDTH;
                float Xmin_ghost = 0;

                float Ymax_ghost = WINDOW_HEIGHT - GHOST_TAM_HEIGHT;
                float Ymin_ghost = 0;
                
                //fantasma 1
                if (GHOST_1_POSITION_X > Xmax_ghost)
                {
                    GHOST_1_SPEED_X *= -1;
                    GHOST_1_POSITION_X = Xmax_ghost;
                }
                else if (GHOST_1_POSITION_X < Xmin_ghost)
                {
                    GHOST_1_SPEED_X *= -1;
                    GHOST_1_POSITION_X = Xmin_ghost;
                }
                else if (GHOST_1_POSITION_Y > Ymax_ghost)
                {
                    GHOST_1_SPEED_Y *= -1;
                    GHOST_1_POSITION_Y = Ymax_ghost;
                }
                else if (GHOST_1_POSITION_Y < Ymin_ghost)
                {
                    GHOST_1_SPEED_Y *= -1;
                    GHOST_1_POSITION_Y = Ymin_ghost;
                }

                //fantasma 2
                if (GHOST_2_POSITION_X > Xmax_ghost)
                {
                    GHOST_2_SPEED_X *= -1;
                    GHOST_2_POSITION_X = Xmax_ghost;
                }
                else if (GHOST_2_POSITION_X < Xmin_ghost)
                {
                    GHOST_2_SPEED_X *= -1;
                    GHOST_2_POSITION_X = Xmin_ghost;
                }
                else if (GHOST_2_POSITION_Y > Ymax_ghost)
                {
                    GHOST_2_SPEED_Y *= -1;
                    GHOST_2_POSITION_Y = Ymax_ghost;
                }
                else if (GHOST_2_POSITION_Y < Ymin_ghost)
                {
                    GHOST_2_SPEED_Y *= -1;
                    GHOST_2_POSITION_Y = Ymin_ghost;
                }

                //Colisão do player com o fantasma 1 e 2
                if (PLAYER_POSITION_X >= GHOST_1_POSITION_X - (GHOST_TAM_WIDTH-50) && PLAYER_POSITION_X <= GHOST_1_POSITION_X + (GHOST_TAM_WIDTH + 30)
                    && PLAYER_POSITION_Y >= GHOST_1_POSITION_Y - (GHOST_TAM_HEIGHT-40) && PLAYER_POSITION_Y <= GHOST_1_POSITION_Y + (GHOST_TAM_HEIGHT+35))
                {
                    _surv_time = _text;
                    State = GameState.GameOver;
                }
                if (PLAYER_POSITION_X >= GHOST_2_POSITION_X - (GHOST_TAM_WIDTH - 50) && PLAYER_POSITION_X <= GHOST_2_POSITION_X + (GHOST_TAM_WIDTH + 30)
                    && PLAYER_POSITION_Y >= GHOST_2_POSITION_Y - (GHOST_TAM_HEIGHT - 40) && PLAYER_POSITION_Y <= GHOST_2_POSITION_Y + (GHOST_TAM_HEIGHT + 35))
                {
                    _surv_time = _text;
                    State = GameState.GameOver;
                }

                if(State == GameState.Nivel2 || State == GameState.Nivel3)
                {
                    GHOST_3_POSITION_X += GHOST_3_SPEED_X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GHOST_3_POSITION_Y += GHOST_3_SPEED_Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    GHOST_4_POSITION_X -= GHOST_4_SPEED_X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GHOST_4_POSITION_Y -= GHOST_4_SPEED_Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //fantasma 3
                    if (GHOST_3_POSITION_X > Xmax_ghost)
                    {
                        GHOST_3_SPEED_X *= -1;
                        GHOST_3_POSITION_X = Xmax_ghost;
                    }
                    else if (GHOST_3_POSITION_X < Xmin_ghost)
                    {
                        GHOST_3_SPEED_X *= -1;
                        GHOST_3_POSITION_X = Xmin_ghost;
                    }
                    else if (GHOST_3_POSITION_Y > Ymax_ghost)
                    {
                        GHOST_3_SPEED_Y *= -1;
                        GHOST_3_POSITION_Y = Ymax_ghost;
                    }
                    else if (GHOST_3_POSITION_Y < Ymin_ghost)
                    {
                        GHOST_3_SPEED_Y *= -1;
                        GHOST_3_POSITION_Y = Ymin_ghost;
                    }

                    //fantasma 4
                    if (GHOST_4_POSITION_X > Xmax_ghost)
                    {
                        GHOST_4_SPEED_X *= -1;
                        GHOST_4_POSITION_X = Xmax_ghost;
                    }
                    else if (GHOST_4_POSITION_X < Xmin_ghost)
                    {
                        GHOST_4_SPEED_X *= -1;
                        GHOST_4_POSITION_X = Xmin_ghost;
                    }
                    else if (GHOST_4_POSITION_Y > Ymax_ghost)
                    {
                        GHOST_4_SPEED_Y *= -1;
                        GHOST_4_POSITION_Y = Ymax_ghost;
                    }
                    else if (GHOST_4_POSITION_Y < Ymin_ghost)
                    {
                        GHOST_4_SPEED_Y *= -1;
                        GHOST_4_POSITION_Y = Ymin_ghost;
                    }

                    //Colisão do player com o fantasma 3 e 4
                    if (PLAYER_POSITION_X >= GHOST_3_POSITION_X - (GHOST_TAM_WIDTH - 50) && PLAYER_POSITION_X <= GHOST_3_POSITION_X + (GHOST_TAM_WIDTH + 30)
                        && PLAYER_POSITION_Y >= GHOST_3_POSITION_Y - (GHOST_TAM_HEIGHT - 40) && PLAYER_POSITION_Y <= GHOST_3_POSITION_Y + (GHOST_TAM_HEIGHT + 35))
                    {
                        _surv_time = _text;
                        State = GameState.GameOver;
                    }
                    if (PLAYER_POSITION_X >= GHOST_4_POSITION_X - (GHOST_TAM_WIDTH - 50) && PLAYER_POSITION_X <= GHOST_4_POSITION_X + (GHOST_TAM_WIDTH + 30)
                        && PLAYER_POSITION_Y >= GHOST_4_POSITION_Y - (GHOST_TAM_HEIGHT - 40) && PLAYER_POSITION_Y <= GHOST_4_POSITION_Y + (GHOST_TAM_HEIGHT + 35))
                    {
                        _surv_time = _text;
                        State = GameState.GameOver;
                    }
                }
            }
            
            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //timer
            //_timer.Draw();

            //Desenhando o menu
            if (State == GameState.Initial)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_menu_texture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
                _spriteBatch.End();
            }
            //Desenhando o nivel 1
            else if (State == GameState.Nivel1 || State == GameState.Nivel2 || State == GameState.Nivel3)
            {
                //Background
                _spriteBatch.Begin();
                _spriteBatch.Draw(_floor_texture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
                _spriteBatch.End();

                //sprites
                _spriteBatch.Begin();

                //sprite e posição da sombra na tela
                Sprite shadowSprite = new Sprite(_shadow_texture, 0, 0, 14, 8);
                shadowSprite.Draw(_spriteBatch, new Rectangle((int)PLAYER_POSITION_X - PLAYER_TAM_WIDTH + 6, (int)PLAYER_POSITION_Y - 8, 28, 16));

                //sprite e posição do personagem na tela
                Sprite playerSprite = new Sprite(_player_texture, 0, 0, 18, 18);
                playerSprite.Draw(_spriteBatch, new Rectangle((int)PLAYER_POSITION_X - PLAYER_TAM_WIDTH, (int)PLAYER_POSITION_Y - PLAYER_TAM_HEIGHT, PLAYER_TAM_WIDTH, PLAYER_TAM_HEIGHT));

                //sprite e posição do fantasma 1 na tela
                Sprite ghostSprite_1 = new Sprite(_ghost_texture, 0, 0, 18, 18);
                ghostSprite_1.Draw(_spriteBatch, new Rectangle((int)GHOST_1_POSITION_X, (int)GHOST_1_POSITION_Y, GHOST_TAM_WIDTH, GHOST_TAM_HEIGHT));

                //sprite e posição do fantasma 2 na tela
                Sprite ghostSprite_2 = new Sprite(_ghost_texture, 0, 0, 18, 18);
                ghostSprite_1.Draw(_spriteBatch, new Rectangle((int)GHOST_2_POSITION_X, (int)GHOST_2_POSITION_Y, GHOST_TAM_WIDTH, GHOST_TAM_HEIGHT));

                //timer
                _spriteBatch.DrawString(font, _text, position_font, Color.White);

                _spriteBatch.End();
                if(State == GameState.Nivel2 || State == GameState.Nivel3)
                {
                    _spriteBatch.Begin();
                    Sprite ghostSprite_3 = new Sprite(_ghost_texture, 0, 0, 18, 18);
                    ghostSprite_3.Draw(_spriteBatch, new Rectangle((int)GHOST_3_POSITION_X, (int)GHOST_3_POSITION_Y, GHOST_TAM_WIDTH, GHOST_TAM_HEIGHT));

                    //sprite e posição do fantasma 2 na tela
                    Sprite ghostSprite_4 = new Sprite(_ghost_texture, 0, 0, 18, 18);
                    ghostSprite_4.Draw(_spriteBatch, new Rectangle((int)GHOST_4_POSITION_X, (int)GHOST_4_POSITION_Y, GHOST_TAM_WIDTH, GHOST_TAM_HEIGHT));
                    _spriteBatch.End();
                }
            }
            //Desenhando a pausa
            else if (State == GameState.Pause)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_pause_texture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White); 
                _spriteBatch.End();
            }

            else if(State == GameState.GameOver)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_GameOver_texture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
                _spriteBatch.DrawString(font, "Voce sobreviveu por " + _surv_time + " segundos.", position_surv_time, Color.White);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
