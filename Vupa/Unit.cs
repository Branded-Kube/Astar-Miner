using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Vupa
{
    public class Unit
    {
        public bool isPlayer;
        enum UnitState { Pathing, Stopped };
        private UnitState unitState = UnitState.Stopped;

        private Point cellLocation;  // Which tile the agent is currently on
        private Vector2 drawLocation;  // Where the agent is being drawn on screen

        private Vector2 velocity; // Speed X Direction of the agent
        private float speed = 3;  // rate the agent moves
        private float distanceCheckValue; // distance to the nextPathLoc that is used to switch

        public A_Star a_star;  // Our pathfinding class instance

        private Vector2 drawOrigin;  // The origin used to draw the agent
        private Texture2D texture; // The image of the agent

        private Vector2 nextPathLoc;  // The next location in the path we are trying to reach
        private Point destination;  // the final destination tile we are trying to reach

        private int setDestX;
        private int setDestY;
        private int setStartX;
        private int setStartY;

        public void setDestination(int w, int x, int y, int z)
        {
            setStartX = w;
            setStartY = x;
            setDestX = y;
            setDestY = z;
        }

        public Unit(Texture2D tex, Point loc, A_Star A)
        {
            drawLocation = getDrawLoc(loc);
            UpdateTileLoc();
            velocity = Vector2.Zero;
            texture = tex;
            drawOrigin = new Vector2(texture.Width, texture.Height) * 0.5f;
            a_star = A;
            distanceCheckValue = speed * 1.75f;
        }

        Vector2 getDrawLoc(Point loc)
        {
            // Convert tile location to onscreen location
            return new Vector2(loc.X * 50 + 25, loc.Y * 50 + 25);
        }

        Vector2 getDrawLoc(Vector2 loc)
        {
            // Convert tile location from the path that is a vector to onscreen location
            return new Vector2(loc.X * 50 + 25, loc.Y * 50 + 25);
        }

        void UpdateTileLoc()
        {
            // Update the tile location based on the onscreen location
            Point tileLocationTmp = new Point((int)drawLocation.X / 50, (int)drawLocation.Y / 50);

            // It the agent has moved from one location to tile to another remove the reference to the agent from
            // the old tile, update the location and set a reference to the agent on the new tile            
            if (tileLocationTmp != cellLocation)
            {
                Game1.cellInfo[cellLocation.X, cellLocation.Y].unitOnCell = null;
                cellLocation = tileLocationTmp;
                Game1.cellInfo[cellLocation.X, cellLocation.Y].unitOnCell = this;
            }
        }

        public Point GetDestintation()
        {
            // This is a function that would find the goal of the path
            // For example this could be a function to find the closest player and set the enemy to move towards him.
            if (cellLocation == new Point(setDestX, setDestY))
                return new Point(setStartX, setStartY);
            else
                return new Point(setDestX, setDestY);
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (unitState == UnitState.Stopped)
            {
                destination = GetDestintation();

                if (destination != cellLocation)
                {
                    a_star.start(cellLocation.X, cellLocation.Y, destination.X, destination.Y);
                    if (a_star.pathAvailable)
                    {
                        // Start in the direction of the first part of the path
                        unitState = UnitState.Pathing;
                        nextPathLoc = getDrawLoc(a_star.Path[0]);
                        velocity = nextPathLoc - drawLocation;
                        velocity.Normalize();
                        velocity *= speed;

                    }
                }
            }

            if (unitState == UnitState.Pathing)
            {
                if (a_star.pathAvailable && a_star.Path.Count > 1)
                {
                    // Check if we need to start moving toward the next path part
                    if ((nextPathLoc - drawLocation).Length() < distanceCheckValue)
                    {
                        // remove the path part we are currently moving towards
                        a_star.Path.RemoveAt(0);
                        // set the location in screen co-ordinates for the new path part
                        nextPathLoc = getDrawLoc(a_star.Path[0]);

                        // Set the velocity pointing towards the next path part
                        velocity = nextPathLoc - drawLocation;
                        velocity.Normalize();
                        velocity *= speed;
                    }
                }

                // Update the draw location and tile location
                drawLocation += velocity;
                UpdateTileLoc();

                // Check if we have reached the overall goal of the path
                if (a_star.Path.Count == 1 && (nextPathLoc - drawLocation).Length() < distanceCheckValue)
                {
                    // Stop the agent and make sure it is on the exact location of the goal
                    drawLocation = nextPathLoc;
                    velocity = Vector2.Zero;
                    unitState = UnitState.Stopped;
                }
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawLocation, null, Color.White, 0, drawOrigin, 1, SpriteEffects.None, 0);
        }
    }
}

