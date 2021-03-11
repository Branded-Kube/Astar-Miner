using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Vupa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D cellImage;

        private Texture2D agentTexture;
        public static SpriteFont font;

        private List<Button> buttonlist;
        private List<Button> buttonlistDel;
        private List<Button> buttonlistAdd;
        private Button buttonDFS;
        private Button buttonBFS;
        private Button buttonSearchMethod;
        private Button buttonStartSearch;
        private Button buttonStartAstar;

        private string chosenOption;
        private bool options = true;
        private Texture2D button;

        

        private A_Star a_star;

        private Unit agent;

        private MouseState mClick;

        public static Cell[,] cellInfo = new Cell[10, 10];

        public Point startLoc = new Point(3, 4);
        public Point endLoc = new Point(9, 9);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 500;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            buttonlist = new List<Button>();
            buttonlistDel = new List<Button>();
            buttonlistAdd = new List<Button>();
        }

        protected override void Initialize()
        {
            // Create the tile objects in the array
            for (int x = 0; x < cellInfo.GetLength(0); x++)
            {
                for (int y = 0; y < cellInfo.GetLength(0); y++)
                {
                    cellInfo[x, y] = new Cell();
                }
            }

            // Change some tiles to walls
            cellInfo[4, 0].cellType = Cell.CellType.Wall;
            cellInfo[4, 1].cellType = Cell.CellType.Wall;
            cellInfo[4, 2].cellType = Cell.CellType.Wall;
            cellInfo[4, 3].cellType = Cell.CellType.Wall;
            cellInfo[4, 4].cellType = Cell.CellType.Wall;
            cellInfo[4, 5].cellType = Cell.CellType.Wall;
            cellInfo[3, 5].cellType = Cell.CellType.Wall;
            cellInfo[2, 5].cellType = Cell.CellType.Wall;
            cellInfo[1, 5].cellType = Cell.CellType.Wall;
            cellInfo[1, 4].cellType = Cell.CellType.Wall;
            cellInfo[1, 3].cellType = Cell.CellType.Wall;
            cellInfo[1, 2].cellType = Cell.CellType.Wall;

            cellInfo[7, 6].cellType = Cell.CellType.Wall;
            cellInfo[7, 7].cellType = Cell.CellType.Wall;
            cellInfo[7, 8].cellType = Cell.CellType.Wall;
            cellInfo[7, 9].cellType = Cell.CellType.Wall;


            // Pass the tile information and a weight for the H.
            // The lower the H weight value shorter the path
            // the higher it is the less number of checks it take to determine
            // a path.  Less checks might be useful for a very large number of agents.
            int hWeight = 2;
            a_star = new A_Star(cellInfo, hWeight);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cellImage = Content.Load<Texture2D>("dirt");
            agentTexture = Content.Load<Texture2D>("worker");
            font = Content.Load<SpriteFont>("font");
            button = Content.Load<Texture2D>("Btn");


            agent = new Unit(agentTexture, startLoc, a_star);


            buttonSearchMethod = new Button(550, 400, "How do ye wish to search?", button);
            buttonDFS = new Button(600, 300, "DFS", button);
            buttonBFS = new Button(600, 350, "BFS", button);
            buttonStartSearch = new Button(550, 50, "Start search", button);
            buttonStartAstar = new Button(550, 100, "Start A*", button);

            buttonDFS.Click += DFS_Click;
            buttonBFS.Click += BFS_Click;
            buttonSearchMethod.Click += ButtonSearchMethod_Click;

            buttonStartSearch.Click += ButtonStartSearch_Click;
            buttonStartAstar.Click += ButtonStartAStar_Click;

            buttonlist.Add(buttonSearchMethod);
        }

        private void ButtonStartAStar_Click(object sender, EventArgs e)
        {
            //TODO AStar starter
            Debug.WriteLine("A* starting");
            AgentMoving();
        }

        private void ButtonStartSearch_Click(object sender, EventArgs e)
        {
        
            if (chosenOption == "DFS")
            {
                //TODO DFS stuff
                Debug.WriteLine("Starting DFS search");
                buttonlistAdd.Add(buttonStartAstar);
            }

            else if (chosenOption == "BFS")
            {
                //TODO BFS stuff
                Debug.WriteLine("Starting BFS search");
                buttonlistAdd.Add(buttonStartAstar);
            }

            else
            {
                Debug.WriteLine("No searching method chosen");
            }
        }

        private void ButtonSearchMethod_Click(object sender, EventArgs e)
        {
            if (options == true)
            {
                buttonlistAdd.Add(buttonDFS);
                buttonlistAdd.Add(buttonBFS);
                Debug.WriteLine("Options unlocked");

                options = false;
            }

            else if(options == false)
            {
                buttonlistDel.Add(buttonDFS);
                buttonlistDel.Add(buttonBFS);

                buttonlistDel.Add(buttonStartSearch);

                buttonlistAdd.Clear();
                options = true;
            }
            

        }
        private void DFS_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Using DFS");
            chosenOption = "DFS";
            buttonlistAdd.Add(buttonStartSearch);
        }
        private void BFS_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Using BFS");
            chosenOption = "BFS";
            buttonlistAdd.Add(buttonStartSearch);
        }

       private void AgentMoving()
        {
              
            agent.setDestination(startLoc.X, startLoc.Y, endLoc.X, endLoc.Y);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mClick = Mouse.GetState();
            if (mClick.LeftButton == ButtonState.Pressed)
            {
                startLoc.X = (int)mClick.X / 50;
                startLoc.Y = (int)mClick.Y / 50;
            }
            if (mClick.RightButton == ButtonState.Pressed)
            {
                endLoc.X = (int)mClick.X / 50;
                endLoc.Y = (int)mClick.Y / 50;
            }

       

           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            foreach (var item in buttonlistDel)
            {
                buttonlist.Remove(item);
            }

            foreach (var item in buttonlistAdd)
            {
                buttonlist.Add(item);

            }

            foreach (var item in buttonlist)
            {
                item.Update();

            }

            

            base.Update(gameTime);
            agent.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int x = 0; x < cellInfo.GetLength(0); x++)
            {
                for (int y = 0; y < cellInfo.GetLength(0); y++)
                {
                    if (cellInfo[x, y].cellType == Cell.CellType.Floor)
                        spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.White);
                    else if (cellInfo[x, y].cellType == Cell.CellType.Wall)
                        spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.DarkGray);
                }
            }
            foreach (var item in buttonlist)
            {
                item.Draw(spriteBatch);
            }

            for (int i = 0; i < agent.a_star.Path.Count; i++)
            {
                spriteBatch.Draw(cellImage, new Vector2(agent.a_star.Path[i].X * 50, agent.a_star.Path[i].Y * 50), Color.Yellow);
            }
            spriteBatch.DrawString(font, $"Selected search method: {chosenOption}", new Vector2(530, 0), Color.Black);

            spriteBatch.Draw(cellImage, new Vector2(startLoc.X * 50, startLoc.Y * 50), Color.Green);
            spriteBatch.Draw(cellImage, new Vector2(endLoc.X * 50, endLoc.Y * 50), Color.Red);
            agent.Draw(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}