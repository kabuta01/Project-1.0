using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Data.OleDb;

using TileEngine;

namespace FinalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        int a = 0;
        int[,] emap;
        
        
        
        SoundEffect bgmusic;

        TileMap tileMap = new TileMap();

        Camera camera = new Camera();
        Texture2D rec;
        TileLayer tileLayer;
        EnemyLayer enemyLayer;
        //CollisionLayer colLayer;
        Player player;
        NPC npc = new NPC(0, "hello");
        public List<Rectangle> Recs = new List<Rectangle>();

        //Enemy enemy;

        List<Enemy> enemies = new List<Enemy>();
        

        //start Bailey code

        enum GameState
        {
            titlescreen, mainmenu, instructions, newgame, loadgame, options, playgame, gameover, castamere, victory
        }
        GameState state = GameState.titlescreen;

        //timers
        int titletimer;
        int deadtimer = 0;

        //background pictures
        Texture2D btitlepic;
        Texture2D bmenupic;
        Texture2D boptionspic;
        Texture2D binstructionspic;
        Texture2D blgamepic;
        Texture2D bngamepic;
        Texture2D bgameoverpic;
        Texture2D bgamemenupic;
        Texture2D bpgamepic;

        //pictures
        Texture2D buttonpic;
        Texture2D namebox;

        //vectors
        Vector2 optionsvec;
        Vector2 binstructionsvec;
        Vector2 lgamevec;
        Vector2 ngamevec;
        Vector2 menuvec;
        Vector2 startvec;
        Vector2 infovec;
        Vector2 instructionsvec;
        Vector2 lnamevec;
        Vector2 lIDvec;
        Vector2 loadvec;
        Vector2 newnamevec;


        //background rectangles
        Rectangle btitlerec;
        Rectangle bmenurec;
        Rectangle boptionsrec;
        Rectangle binstructionsrec;
        Rectangle blgamerec;
        Rectangle bngamerec;
        Rectangle bgameoverrec;
        Rectangle bgamemenurec;
        Rectangle bplaygamerec;


        //rectangles
        Rectangle optionsrec;
        Rectangle instructionsrec;
        Rectangle lgamerec;
        Rectangle ngamerec;
        Rectangle menurec;
        Rectangle startrec;
        Rectangle lnamerec;
        Rectangle lID;
        Rectangle loadrec;
        Rectangle newnamerec;


        /*player pics
        Texture2D cutpursepic;
        Texture2D assassinpic;
        Texture2D mercenarypic;
        Texture2D farmerpic;
        Texture2D scholarpic;
        Texture2D novicepic;

        player vecs
        Vector2 cutpursevec;
        Vector2 assassinvec;
        Vector2 mercenaryvec;
        Vector2 farmervec;
        Vector2 scholarvec;
        Vector2 novicevec;

        player recs
        Rectangle cutpurserec;
        Rectangle assassinrec;
        Rectangle mercenaryrec;
        Rectangle farmerrec;
        Rectangle scholarrec;
        Rectangle novicerec;*/

        //font
        SpriteFont text;
        SpriteFont buttontext;
        

        //database strings
        string uname = "";
        string UID = "";
        string name = "";
        string ID = "";
        string tempname = "";
        string tempID = "";

        //game variables
        int bodycount = 0;
        int plevel = 0;
        int pxp = 0;

        //other
        MouseState mouseState;
        MouseState oldmState;

        bool namedone;
        bool IDdone = false;
        bool newload;
        bool loadclick;
        bool newgame;
        bool wrongload = true;
        bool load = true;
        

        KeyboardState keyboard = Keyboard.GetState();
        KeyboardState oldkeys;

        //end Bailey code

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //start Bailey code
            IsMouseVisible = true;

            //background rectangles
            
            
            boptionsrec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            binstructionsrec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            bgameoverrec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            blgamerec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            
            bplaygamerec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //rectangles/buttons
            ngamerec = new Rectangle(500, 200, 100, 30);
            lgamerec = new Rectangle(500, 300, 110, 30);
            //optionsrec = new Rectangle(500, 190, 80, 30);
            instructionsrec = new Rectangle(500, 400, 140, 30);

            //vectors/buttons
            //optionsvec = new Vector2(500, 190);
            binstructionsvec = new Vector2(500, 400);
            ngamevec = new Vector2(500, 200);
            lgamevec = new Vector2(500, 300);

            lnamerec = new Rectangle(50, 150, 100, 15);
            lnamevec = new Vector2(50, 150);

            lIDvec = new Vector2(50, 200);
            lID = new Rectangle(50, 200, 30, 15);

            newload = true;
            newgame = true;
            
            //end Bailey code

            //create enemies
            //enemy = new Enemy(0, 200, 200, Recs);
            enemyLayer = EnemyLayer.FromFile(Content, "Content/Layers/Enemy.layer");
            tileLayer = TileLayer.FromFile(Content, "Content/Layers/Layer1.layer");
            //enemy.Initialize();

            //initialize music
            bgmusic = Content.Load<SoundEffect>("Dungeon");
            //player.Initialize();
            //npc.Initialize();

            //enemy layer

            int[,] enemymap = enemyLayer.Map;
            emap = enemymap;
            int enemyMapWidth = enemymap.GetLength(1);
            int enemyMapHeight = enemymap.GetLength(0);

            for (int x = 0; x < enemyMapWidth; x++)//enemyMapWidth
            {
                for (int y = 0; y < enemyMapHeight; y++)//enemyMapHeigh t
                {
                    int textureIndex = enemymap[y, x];
                    int tileWidth = 64;
                    int tileHeight = 64;
                    a = textureIndex;
                    /*if (textureIndex == 1)
                    {
                        Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        batch.Draw(rec, Rec, Color.White);
                        
                        continue;
                    }*/

                    if (textureIndex == -1)
                        continue;

                    if (textureIndex == 2)
                        continue;



                    if (textureIndex == 1)
                    {
                        Enemy e = new Enemy(0, x * tileWidth, y * tileHeight, Recs);
                        //Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        //batch.Draw(rec, Rec, Color.White);
                        enemies.Add(e);

                        continue;
                    }
                    if (textureIndex == 3)
                    {
                        Enemy e = new Enemy(1, x * tileWidth, y * tileHeight, Recs);
                        //Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        //batch.Draw(rec, Rec, Color.White);
                        enemies.Add(e);

                        continue;
                    }
                    if (textureIndex == 4)
                    {
                        Enemy e = new Enemy(2, x * tileWidth, y * tileHeight, Recs);
                        //Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        //batch.Draw(rec, Rec, Color.White);
                        enemies.Add(e);

                        continue;
                    }


                }
            }

            foreach (Enemy en in enemies)
                en.Initialize();




            // collisions
            int[,] map = tileLayer.map;

            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];
                    int tileWidth = 64;
                    int tileHeight = 64;

                    /*if (textureIndex == 1)
                    {
                        Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        batch.Draw(rec, Rec, Color.White);
                        Recs.Add(Rec);
                        continue;
                    }*/

                    if (textureIndex == -1)
                        continue;





                    if (textureIndex == 1)
                    {
                        Rectangle Rec = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        //batch.Draw(rec, Rec, Color.White);
                        Recs.Add(Rec);
                        continue;
                    }


                }
            }
            tileMap.Layers.Add(tileLayer);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            //start Bailey code

            //backgroundpics
            btitlepic = Content.Load<Texture2D>("TitleScreen2");
            bmenupic = Content.Load<Texture2D>("TitleScreen2");
            binstructionspic = Content.Load<Texture2D>("InstructionScreen");
            // blgamepic = Content.Load<Texture2D>("");
            bngamepic = Content.Load<Texture2D>("ChooseClassScreen");
            bgameoverpic = Content.Load<Texture2D>("GameOverScreen");
            // bgamemenupic = Content.Load<Texture2D>("");
            // bpgamepic = Content.Load<Texture2D>("Map-Placeholder");

            //pics
            buttonpic = Content.Load<Texture2D>("Button3 (1)");
            namebox = Content.Load<Texture2D>("NameScreen2");

            /*player pics 
            cutpursepic = Content.Load <Texture2D>("");
            assassinpic = Content.Load<Texture2D>("");
            mercenarypic = Content.Load<Texture2D>("");
            farmerpic = Content.Load<Texture2D>("");
            scholarpic = Content.Load<Texture2D>("");
            novicepic = Content.Load<Texture2D>("");*/

            buttontext = Content.Load<SpriteFont>("Buttonfont");
            text = Content.Load<SpriteFont>("SpriteFont1");
            //end Bailey code

            // Create a new SpriteBatch, which can be used to draw textures. 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //enemy.LoadContent(Content);

            //music
            SoundEffectInstance instance = bgmusic.CreateInstance();
            instance.IsLooped = true;
            bgmusic.Play();

            //create the player
            player = new Player(0, 0, 0, Recs);

            foreach (Enemy en in enemies)
                en.LoadContent(Content);

            //load player content
            player.LoadContent(Content);
            //npc.LoadContent(Content);

            //colLayer = CollisionLayer.FromFile(Content, "Content/Layers/Collision.layer");
            rec = Content.Load<Texture2D>("rec");

            //load font
            font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content. 
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
             // determines which screen you see
            switch (state)
            {
                case GameState.titlescreen:
                    Titleupdate();
                    break;
                case GameState.mainmenu:
                    Mainmenuupdate();
                    break;
                case GameState.options:
                    // Optionsupdate();
                    break;
                case GameState.instructions:
                    Instructionsupdate();
                    break;
                case GameState.newgame:
                    Newgameupdate();
                    break;
                case GameState.loadgame:
                    Loadgameupdate();
                    break;
                case GameState.playgame:
                    Playgameupdate(gameTime);
                    break;
                case GameState.gameover:
                    Gameoverupdate();
                    break;
                case GameState.victory:
                    
                    break;
            }
           


            base.Update(gameTime);
        }

        private void Titleupdate()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 675;
            Window.Title = "The Hero";
            graphics.ApplyChanges();
            btitlerec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //counts off time for splash screen
            titletimer++;
            if (titletimer >= 300)
                state = GameState.mainmenu;
        }

        private void Mainmenuupdate()
        {
            //changes screen size and title for the screen
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 675;
            Window.Title = "The Hero";
            graphics.ApplyChanges();
            bmenurec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            mouseState = Mouse.GetState();

            /*  if(optionsrec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
             // {
             //     state = GameState.options;
              } */

            if (instructionsrec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //goes to instructions
                state = GameState.instructions;
            }

            if (ngamerec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //goes to new game screen
                state = GameState.newgame;
            }

            if (lgamerec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //goes to load game screen
                state = GameState.loadgame;
            }

            oldmState = mouseState;
        }

        private void Instructionsupdate()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 675;
            Window.Title = "The Hero";
            graphics.ApplyChanges();

            mouseState = Mouse.GetState();
            if (menurec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //gets you back to main menu
                state = GameState.mainmenu;
            }
            oldmState = mouseState;
        }

        private void Newgameupdate()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            Window.Title = "The Hero";
            graphics.ApplyChanges();
            bngamerec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            mouseState = Mouse.GetState();
            if (newgame == true)
            {
                //initializes variables that run the typing code if you have previously exited the screen
                UID = "";
                uname = "";
                namedone = false;
                IDdone = false;
                newgame = false;
                ID = "";
                name = "";
            }

            if (namedone == false)
            {
                //type in name for new game automatically
                uname = Type();
            }
            else if (newnamerec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //type in name after click
                uname = Type();
            }
            else
            {
                //allows you to start a new game or click off to main menu
                if (startrec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
                {
                    NewGame();
                    state = GameState.playgame;
                }
            }
            if (menurec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                newgame = true;
                state = GameState.mainmenu;
            }
            oldmState = mouseState;
        }

        public void Loadgameupdate()
        {

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            Window.Title = "The Hero";
            graphics.ApplyChanges();


            mouseState = Mouse.GetState();
            if (newload == true)
            {
                //initializes variables that run the typing and database code
                UID = "";
                uname = "";
                namedone = false;
                IDdone = false;
                newload = false;
                wrongload = false;
                load = true;
                ID = "";
                name = "";
            }


            if (namedone == false)
            {
                //automatically type in name
                uname = Type();
            }
            else if (lnamerec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //types in name after click
                loadclick = true;
                if (loadclick == true)
                {
                    //allows you to type still with the reset of namedone
                    namedone = false;
                    loadclick = false;
                }
                uname = Type();
            }
            else if (lID.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                //same as above for the ID
                loadclick = true;
                if (loadclick == true)
                {
                    IDdone = false;
                    loadclick = false;
                }
                UID = IntType();
            }
            else if (IDdone == false)
            {
                //allows you to type the ID
                UID = IntType();
            }
            else
            {
                //allows you to click load or main menu after entering the values
                if (loadrec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released && (uname != "" && UID != ""))
                {
                    Loadgame();
                    if (wrongload == false)
                    {
                        state = GameState.playgame;
                        newload = true;
                    }
                    else
                        newload = true;
                }
            }
            if (menurec.Contains(new Point(mouseState.X, mouseState.Y)) && mouseState.LeftButton == ButtonState.Pressed && oldmState.LeftButton == ButtonState.Released)
            {
                state = GameState.mainmenu;
                newload = true;
            }

            oldmState = mouseState;
        }

        private void Playgameupdate(GameTime gameTime)
        {
            //music
            keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //camera.Update();
            player.Update(gameTime, npc);
            //enemy.Update(gameTime, player);
            //get screen demensions

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            //update the enemies
            foreach (Enemy en in enemies)
                en.Update(gameTime, player);


            //keep camera up with the player

            camera.Position.X = player.PlayerRec.X + (player.PlayerRec.Width) - (screenWidth / 2);
            camera.Position.Y = player.PlayerRec.Y + (player.PlayerRec.Height) - (screenHeight / 2);

            //stop player at edges

            if (player.PlayerRec.X < 0)
                player.PlayerRec.X = 0;
            if (player.PlayerRec.Y < 0)
                player.PlayerRec.Y = 0;

            if (player.PlayerRec.X > tileMap.GetWidthInPixels() - player.PlayerRec.Width)
                player.PlayerRec.X = tileMap.GetWidthInPixels() - player.PlayerRec.Width;
            if (player.PlayerRec.Y > tileMap.GetHeightInPixels() - player.PlayerRec.Height)
                player.PlayerRec.Y = tileMap.GetHeightInPixels() - player.PlayerRec.Height;

            //stop camera at edges

            if (camera.Position.X > tileLayer.WidthInPixels - screenWidth)
                camera.Position.X = tileLayer.WidthInPixels - screenWidth;
            if (camera.Position.Y > tileLayer.HeightInPixels - screenHeight)
                camera.Position.Y = tileLayer.HeightInPixels - screenHeight;

            if (camera.Position.X < 0)
                camera.Position.X = 0;
            if (camera.Position.Y < 0)
                camera.Position.Y = 0;


            base.Update(gameTime);

            if (player.HP <= 0)
            {
                state = GameState.gameover;
            }

            if ((keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl)) && keyboard.IsKeyDown(Keys.S))
            {
                //SaveGame();
            }

            if ((keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl)) && keyboard.IsKeyDown(Keys.M))
            {
                player.PlayerRec.X = 128;
                player.PlayerRec.Y = 128;
            }
        }

        private void Gameoverupdate()
        {
            bgameoverrec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            deadtimer++;
            if (deadtimer == 300)
            {
                player.HP = 20;
                player.PlayerRec.X = 128;
                player.PlayerRec.Y = 128;
                state = GameState.mainmenu;
            }
        }

        private void NewGame()
        {
            //database insert code
            using (var connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ Environment.CurrentDirectory + "/GameDatabase.accdb"))
            {
                using (var command = new OleDbCommand("INSERT into PlayerInfo (Name) values ('" + uname + "')", connection))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }

                using (var command =
          new OleDbCommand("SELECT * FROM PlayerInfo", connection))
                {
                    command.Connection.Open();


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tempname = reader["Name"].ToString();
                            if (tempname == uname)
                            {
                                UID = reader["ID"].ToString();
                            }
                        }
                    }
                }

            }
        }

       /* private void SaveGame()
        {
            //saves certain aspects of the character
            using (var connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Environment.CurrentDirectory + "/GameDatabase.accdb"))
            {
                using (OleDbCommand command = connection.CreateCommand())
                {
                    OleDbParameter field1 = new OleDbParameter("@var1",OleDbType.Char);
                    OleDbParameter field2 = new OleDbParameter("@var2",OleDbType.Integer);
                    OleDbParameter field3 = new OleDbParameter("@var3",OleDbType.Integer);
                    OleDbParameter field4 = new OleDbParameter("@var4",OleDbType.Integer);
                    field1.Value = uname;
                    field2.Value = pxp;
                    field3.Value = plevel;
                    field4.Value = bodycount;
                }
                using (var command = new OleDbCommand("UPDATE PlayerInfo SET XP = " + 3 + " WHERE Name ='" + uname + "'", connection))
                {  command.Connection.Open();
                    OleDbParameter field1 = new OleDbParameter("@var1", OleDbType.Char);
                    OleDbParameter field2 = new OleDbParameter("@var2", OleDbType.Integer);
                    field1.Value = uname;
                    field2.Value = pxp;
                  
                    command.Parameters.Add(field1);
                    command.Parameters.Add(field2);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                using (var command1 = new OleDbCommand("UPDATE PlayerInfo SET Level = " + 4 + " WHERE Name = '" + uname + "'", connection))
                {
                    command1.Connection.Open();
                    command1.ExecuteNonQuery();
                    command1.Connection.Close();
                }
                using (var command2 = new OleDbCommand("UPDATE PlayerInfo SET Enemies Killed = " + 5 + " WHERE Name = '" + uname + "'", connection))
                {
                    command2.Connection.Open();
                    command2.ExecuteNonQuery();
                    command2.Connection.Close();
                }
            }
        }*/

        private void Loadgame()
        {
            //inserts database info about the player who is being loaded
            using (var connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Environment.CurrentDirectory + "/GameDatabase.accdb"))
            {
                using (var command =
           new OleDbCommand("SELECT * FROM PlayerInfo", connection))
                {
                    command.Connection.Open();
                    
                   
                        using (var reader = command.ExecuteReader())
                        {
                            wrongload = true;
                            while (reader.Read())
                            {
                                tempname = reader["Name"].ToString();
                                tempID = reader["ID"].ToString();
                                if (tempname.Equals(uname) && tempID.Equals(UID))
                                {
                                    wrongload = false;
                                    //pxp = (int)reader["XP"];
                                    //plevel = (int)reader["Level"];
                                    bodycount = (int)reader["EnemiesKilled"];

                                }
                            }
                        }
                    }
                  
                  
                
            }
        }

        private string IntType()
        {
            //typing ID code
            keyboard = Keyboard.GetState();
            Keys key = Keys.None;
            if (keyboard.GetPressedKeys().Count() > 0)
                key = keyboard.GetPressedKeys()[0];
            if (key != Keys.None && oldkeys.IsKeyUp(key))
            {
                switch (key)
                {
                    case Keys.D1:
                        ID += "1";
                        break;
                    case Keys.D2:
                        ID += "2";
                        break;
                    case Keys.D3:
                        ID += "3";
                        break;
                    case Keys.D4:
                        ID += "4";
                        break;
                    case Keys.D5:
                        ID += "5";
                        break;
                    case Keys.D6:
                        ID += "6";
                        break;
                    case Keys.D7:
                        ID += "7";
                        break;
                    case Keys.D8:
                        ID += "8";
                        break;
                    case Keys.D9:
                        ID += "9";
                        break;
                    case Keys.D0:
                        ID += "0";
                        break;
                    case Keys.Back:
                        if (ID.Length > 0)
                        {
                            ID = ID.Substring(0, ID.Length - 1);
                        }
                        break;
                    case Keys.Enter:
                        IDdone = true;
                        break;
                }
            }
            oldkeys = keyboard;
            return ID;
        }

        private string Type()
        {
            //typing name code
            keyboard = Keyboard.GetState();
            Keys key = Keys.None;

            bool shift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);

            if (keyboard.GetPressedKeys().Count() > 0)
                key = keyboard.GetPressedKeys()[0];
            if (key != Keys.None && oldkeys.IsKeyUp(key))
            {
                switch (key)
                {
                    case Keys.A:
                        name += shift ? "A" : "a";
                        break;
                    case Keys.B:
                        name += shift ? "B" : "b";
                        break;
                    case Keys.C:
                        name += shift ? "C" : "c";
                        break;
                    case Keys.D:
                        name += shift ? "D" : "d";
                        break;
                    case Keys.E:
                        name += shift ? "E" : "e";
                        break;
                    case Keys.F:
                        name += shift ? "F" : "f";
                        break;
                    case Keys.G:
                        name += shift ? "G" : "g";
                        break;
                    case Keys.H:
                        name += shift ? "H" : "h";
                        break;
                    case Keys.I:
                        name += shift ? "I" : "i";
                        break;
                    case Keys.J:
                        name += shift ? "J" : "j";
                        break;
                    case Keys.K:
                        name += shift ? "K" : "k";
                        break;
                    case Keys.L:
                        name += shift ? "L" : "l";
                        break;
                    case Keys.M:
                        name += shift ? "M" : "m";
                        break;
                    case Keys.N:
                        name += shift ? "N" : "n";
                        break;
                    case Keys.O:
                        name += shift ? "O" : "o";
                        break;
                    case Keys.P:
                        name += shift ? "P" : "p";
                        break;
                    case Keys.Q:
                        name += shift ? "Q" : "q";
                        break;
                    case Keys.R:
                        name += shift ? "R" : "r";
                        break;
                    case Keys.S:
                        name += shift ? "S" : "s";
                        break;
                    case Keys.T:
                        name += shift ? "T" : "t";
                        break;
                    case Keys.U:
                        name += shift ? "U" : "u";
                        break;
                    case Keys.V:
                        name += shift ? "V" : "v";
                        break;
                    case Keys.W:
                        name += shift ? "W" : "w";
                        break;
                    case Keys.X:
                        name += shift ? "X" : "x";
                        break;
                    case Keys.Y:
                        name += shift ? "Y" : "y";
                        break;
                    case Keys.Z:
                        name += shift ? "Z" : "z";
                        break;
                    case Keys.Space:
                        name += " ";
                        break;
                    case Keys.Back:
                        if (name.Length > 0)
                        {
                            name = name.Substring(0, name.Length - 1);
                        }
                        break;
                    case Keys.Enter:
                        namedone = true;
                        break;
                }

            }
            oldkeys = keyboard;
            return name;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            switch (state)
            {
                //switches the screen
                case GameState.titlescreen:
                    titledraw();
                    break;
                case GameState.mainmenu:
                    mainmenudraw();
                    break;
                case GameState.instructions:
                    instructionsdraw();
                    break;
                case GameState.newgame:
                    newgamedraw();
                    break;
                case GameState.loadgame:
                    loadgamedraw();
                    break;
                case GameState.playgame:
                    playgamedraw(gameTime);
                    break;
                case GameState.gameover:
                    gameoverdraw();
                    break;
            }

            
            
            base.Draw(gameTime);
        }

        private void titledraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(btitlepic, btitlerec, Color.White);
            spriteBatch.End();
        }

        private void mainmenudraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bmenupic, bmenurec, Color.White);

            //draws buttons
            spriteBatch.Draw(buttonpic, ngamerec, Color.White);
            spriteBatch.Draw(buttonpic, lgamerec, Color.White);
            spriteBatch.Draw(buttonpic, instructionsrec, Color.White);

            //draws button text
            spriteBatch.DrawString(buttontext, "New Game", ngamevec, Color.Black);
            spriteBatch.DrawString(buttontext, "Load Game", lgamevec, Color.Black);
            spriteBatch.DrawString(buttontext, "Instructions", binstructionsvec, Color.Black);

            spriteBatch.End();
        }

        /* private void optionsdraw()
         {
             GraphicsDevice.Clear(Color.Green);
             menurec = new Rectangle(50, 50, 50, 30);
             menuvec = new Vector2(50, 50);
             spriteBatch.Draw(buttonpic, menurec, Color.White);
             spriteBatch.DrawString(font, "Menu", menuvec, Color.Black);
         }*/

        private void instructionsdraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(binstructionspic, binstructionsrec, Color.White);

            menurec = new Rectangle(50, 50, 50, 30);
            menuvec = new Vector2(50, 50);

            Vector2 inswalk = new Vector2(50, 120);
            Vector2 insfight = new Vector2(50, 150);
            Vector2 insinven = new Vector2(50, 180);
            Vector2 insheal = new Vector2(50, 210);
            Vector2 inssave = new Vector2(50, 240);
            Vector2 insmenu = new Vector2(50, 270);

            instructionsvec = new Vector2(50, 90);


            spriteBatch.Draw(buttonpic, menurec, Color.White);
            spriteBatch.DrawString(buttontext, "Menu", menuvec, Color.Black);

            //instructions
            spriteBatch.DrawString(text, "Once the game begins you will use the keys of the keyboard to control your character.", instructionsvec, Color.White);
            spriteBatch.DrawString(text, "Move your character with the arrow keys.", inswalk, Color.White);
            spriteBatch.DrawString(text, "To attack press the space key on the keyboard.", insfight, Color.White);
            spriteBatch.DrawString(text, "Kill Big Blue, and all that stand in your way.", insinven, Color.White);
            spriteBatch.DrawString(text, "Stand near the fountains to heal yourself.", insheal, Color.White);
            spriteBatch.DrawString(text, "To exit the game simply press control and m at the same time.", insmenu,Color.White);

            spriteBatch.End();

        }

        private void newgamedraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bngamepic, bngamerec, Color.White);

            menurec = new Rectangle(50, 50, 50, 30);
            menuvec = new Vector2(50, 50);

            startrec = new Rectangle(50, 100, 60, 30);
            startvec = new Vector2(50, 100);

            newnamevec = new Vector2(50, 150);
            newnamerec = new Rectangle(50, 150, 100, 20);

            infovec = new Vector2(200, 150);


            spriteBatch.Draw(buttonpic, menurec, Color.White);
            spriteBatch.DrawString(buttontext, "Menu", menuvec, Color.Black);

            spriteBatch.Draw(buttonpic, startrec, Color.White);
            spriteBatch.DrawString(buttontext, "Start", startvec, Color.Black);

            spriteBatch.Draw(namebox, newnamerec, Color.White);
            spriteBatch.DrawString(text, uname, newnamevec, Color.Black);

            spriteBatch.DrawString(text, "Kill Big Blue, and all that stand in your way.", infovec, Color.White);

            spriteBatch.End();

        }

        private void loadgamedraw()
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Gray);

            menurec = new Rectangle(50, 50, 50, 30);
            menuvec = new Vector2(50, 50);
            loadrec = new Rectangle(50, 100, 60, 30);
            loadvec = new Vector2(50, 100);





            spriteBatch.Draw(buttonpic, menurec, Color.White);
            spriteBatch.DrawString(buttontext, "Menu", menuvec, Color.Black);

            spriteBatch.Draw(buttonpic, loadrec, Color.White);
            spriteBatch.DrawString(buttontext, "Start", loadvec, Color.Black);

            spriteBatch.Draw(namebox, lnamerec, Color.White);
            spriteBatch.DrawString(text, uname, lnamevec, Color.Black);

            spriteBatch.Draw(namebox, lID, Color.White);
            spriteBatch.DrawString(text, UID, lIDvec, Color.Black);

            spriteBatch.End();

        }

        private void playgamedraw(GameTime gameTime)
        {



            GraphicsDevice.Clear(Color.CornflowerBlue);

            tileMap.Draw(spriteBatch, camera, rec);
            //colLayer.Draw(spriteBatch, camera, rec);
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, camera.TransformMatrix);
            player.Draw(spriteBatch, gameTime);
            //npc.Draw(spriteBatch, gameTime);
            //enemy.Draw(spriteBatch, gameTime);

            foreach (Enemy en in enemies)
                en.Draw(spriteBatch, gameTime);

            //draw the hud
            spriteBatch.DrawString(font, "HP: " + player.HP, new Vector2(camera.Position.X + 670, camera.Position.Y + 20), Color.White);
            //if (hudshow == true)
            {

                spriteBatch.DrawString(font, "Lvl: " + plevel, new Vector2(camera.Position.X + 670, camera.Position.Y + 40), Color.White);
                spriteBatch.DrawString(font, "EXP: " + pxp, new Vector2(camera.Position.X + 670, camera.Position.Y + 60), Color.White);
                spriteBatch.DrawString(font, "Name: " + uname, new Vector2(camera.Position.X + 670, camera.Position.Y + 80), Color.White);
                spriteBatch.DrawString(font, "ID: " + UID, new Vector2(camera.Position.X + 670, camera.Position.Y + 100), Color.White);
            }

            spriteBatch.End();
           
        }

        private void gameoverdraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgameoverpic, bgameoverrec, Color.White);
            spriteBatch.End();
        }
    }
}
