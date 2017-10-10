using System.Drawing;
using System.Linq;

namespace StudentTD {
    class PlayerCursor : GameObject {
        /// <summary>
        /// Cursor Variables
        /// </summary> 
        private bool click;             // Clicked or not
        private bool placing;           // Placing a tower or not
        private bool canPlace;          // Tower can be placed or not
        private PointF clickPos;        // Clicked position

        /// <summary>
        /// Constructor for the cursor.
        /// </summary>
        /// <param name="imagePath">Gets the path of the cursor</param>
        /// <param name="startPos">Cursor position</param>
        /// <param name="animationSpeed">The speed for animations</param>
        /// <param name="scaleFactor">A factor to scale sprites</param>
        /// <param name="speed">The speed for which the cursor travel</param> 
        public PlayerCursor(string imagePath, PointF startPos, float animationSpeed, float scaleFactor, float speed)
            : base(imagePath, startPos, animationSpeed, scaleFactor, speed) {
            // On start, the cursor is not placing a tower
            placing = false;
        }

        /// <summary>
        /// Method to handle MousePosition
        /// </summary> 
        public void HandleMousePos() {
            // Sets the mouse position
            this.Position = GameWorld.MousePos;
        }

        /// <summary>
        /// Method to handle the mouse click
        /// </summary>
        /// <param name="click"></param>
        /// <param name="deltaTime"></param> 
        public void HandleMouseClick(bool click, float deltaTime) {
            if (click) {
                // If mouse is clicked, the position is saved & collision will be checked

                this.click = true;
                clickPos = GameWorld.MousePos;

                base.Update(deltaTime);

                this.click = false;
            }
        }

        /// <summary>
        /// OnCollision Method
        /// </summary>
        /// <param name="other"></param> 
        public override void OnCollision(GameObject other) {
            // if the collided object is a Tower & And the other tower is placed & a click is detected & cursor is not placing a tower & the position is on gamefield
            if ((other as Tower) != null && (other as Tower).Placed && click && !placing && GameWorld.MousePos.X < 520) {
                // if the colliede tower is not selected
                if (!(other as Tower).Selected) {
                    // Removes selection from every other tower
                    foreach (GameObject obj in GameWorld.Objects) {
                        if ((obj as Tower) != null) {
                            if ((obj as Tower).Placed && (obj as Tower).Selected) {
                                (obj as Tower).OnDeselection();
                            }
                        }
                    }

                    // Add selected to collided tower
                    (other as Tower).OnSelection();

                }
                else {
                    // Remove selection from tower
                    (other as Tower).OnDeselection();
                }
            }


            // if collided object is a NormalTower
            if ((other as Tower) != null) {
                // if a click is detected & cursor is not placing & position is outside gamefield
                if (click && !placing && GameWorld.MousePos.X > 540) {
                    if ((other as NormalTower) != null) {
                        if (GameWorld.Currency >= ((NormalTower)other).Price) {
                            // Adds a tower to the cursor for the player to place
                            placing = true;
                            GameWorld.TowerToPlace = new NormalTower(@"Sprites\Towers\normaltower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 1, false, true, false);
                            GameWorld.NewObjects.Add(GameWorld.TowerToPlace);
                        }
                    }
                    if ((other as AirTower) != null) {
                        if (GameWorld.Currency >= ((AirTower)other).Price) {
                            // Adds a tower to the cursor for the player to place
                            placing = true;
                            GameWorld.TowerToPlace = new AirTower(@"sprites\towers\airtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 1, false, true, false);
                            GameWorld.NewObjects.Add(GameWorld.TowerToPlace);
                        }
                    }
                    if ((other as SplashTower) != null) {
                        if (GameWorld.Currency >= ((SplashTower)other).Price) {
                            // Adds a tower to the cursor for the player to place
                            placing = true;
                            GameWorld.TowerToPlace = new SplashTower(@"sprites\towers\splashtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 1, false, true, false);
                            GameWorld.NewObjects.Add(GameWorld.TowerToPlace);
                        }
                    }
                    if ((other as SlowTower) != null) {
                        if (GameWorld.Currency >= ((SlowTower)other).Price) {
                            // Adds a tower to the cursor for the player to place
                            placing = true;
                            GameWorld.TowerToPlace = new SlowTower(@"sprites\towers\slowtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 1, false, true, false);
                            GameWorld.NewObjects.Add(GameWorld.TowerToPlace);
                        }
                    }
                }
                else if (click && placing && GameWorld.MousePos.X > 540) {
                    GameWorld.RemoveObjects.Add(GameWorld.TowerToPlace);
                    GameWorld.TowerToPlace = null;
                    placing = false;
                }

                // if the player is currently placing a tower and clicks on the gamefield
                if (click && placing && GameWorld.MousePos.X < 520) {
                    // Tower can be placed unless another tower is colliding
                    canPlace = true;

                    // Creates a CollisionBox for the cursor
                    RectangleF newTowerCollision = new RectangleF(clickPos.X - 16, clickPos.Y - 16, 32, 32);

                    foreach (GameObject obj in GameWorld.Objects) {
                        // Checks Tower in gameobjects to see if it's colliding with the tower being placed
                        if ((obj as Tower) != null &&
                            ((Tower)obj).Placed) {
                            // The rectangle of an already placed tower
                            RectangleF oldTowerCollision = obj.CollisionBox;

                            if (newTowerCollision.IntersectsWith(oldTowerCollision)) {
                                // If the new tower collides with an already placed tower, the tower cannot be placed!
                                canPlace = false;
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < (GameWorld.Checkpoints.Count() - 1); i++) {
                        // The rectangle of a checkpoint 'tile'
                        RectangleF roadCollision;

                        if (GameWorld.Checkpoints[(i + 1)].X - GameWorld.Checkpoints[i].X < 0 || GameWorld.Checkpoints[(i + 1)].Y - GameWorld.Checkpoints[i].Y < 0) {
                            roadCollision = new RectangleF(GameWorld.Checkpoints[(i + 1)].X, GameWorld.Checkpoints[(i + 1)].Y, ((GameWorld.Checkpoints[i].X - GameWorld.Checkpoints[(i + 1)].X) + 32), ((GameWorld.Checkpoints[i].Y - GameWorld.Checkpoints[(i + 1)].Y) + 32));
                        }
                        else {
                            roadCollision = new RectangleF(GameWorld.Checkpoints[i].X, GameWorld.Checkpoints[i].Y, ((GameWorld.Checkpoints[(i + 1)].X - GameWorld.Checkpoints[i].X) + 32), ((GameWorld.Checkpoints[(i + 1)].Y - GameWorld.Checkpoints[i].Y) + 32));
                        }


                        if (newTowerCollision.IntersectsWith(roadCollision)) {
                            // If the new tower collides with an already placed tower, the tower cannot be placed!
                            canPlace = false;
                            break;
                        }
                    }

                    // if the tower is not colliding with any already placed towers
                    if (canPlace) {
                        // Places the new tower and removes $XX from the player
                        if ((other as NormalTower) != null) {
                            GameWorld.NewObjects.Add(new NormalTower(@"Sprites\Towers\normaltower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 400, true, false, false));

                            GameWorld.Currency -= ((NormalTower)other).Price;
                        }
                        if ((other as AirTower) != null) {
                            GameWorld.NewObjects.Add(new AirTower(@"sprites\towers\airtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 400, true, false, false));

                            GameWorld.Currency -= ((AirTower)other).Price;
                        }
                        if ((other as SplashTower) != null) {
                            GameWorld.NewObjects.Add(new SplashTower(@"sprites\towers\splashtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 1000, true, false, false));

                            GameWorld.Currency -= ((SplashTower)other).Price;

                        }
                        if ((other as SlowTower) != null) {
                            GameWorld.NewObjects.Add(new SlowTower(@"sprites\towers\slowtower.png", new PointF(clickPos.X - 16, clickPos.Y - 16), 0, 1, 500, true, false, false));

                            GameWorld.Currency -= ((SlowTower)other).Price;

                            foreach (SlowTower pTower in GameWorld.NewObjects) {
                                if ((pTower as SlowTower) != null) {
                                    ((SlowTower)pTower).Price = 16;
                                }
                            }
                        }
                        GameWorld.RemoveObjects.Add(GameWorld.TowerToPlace);
                        GameWorld.TowerToPlace = null;
                        placing = false;
                    }
                }
            }

            // if collided object is Upgradebutton
            if ((other as Upgrade) != null) {
                // if a click is detected & cursor is not placing & position is outside gamefield
                if (click && !placing && GameWorld.MousePos.X > 540) {
                    // Upgrades tower if currency is high enough
                    ((Upgrade)other).UpgradeTower();

                }
                if (click && placing && GameWorld.MousePos.X > 540) {
                    GameWorld.RemoveObjects.Add(GameWorld.TowerToPlace);
                    GameWorld.TowerToPlace = null;
                    placing = false;

                }
            }

            // if collided object is Sellbutton
            if ((other as Sell) != null) {
                // if a click is detected & cursor is not placing & position is outside gamefield
                if (click && !placing && GameWorld.MousePos.X > 540) {
                    // Adds a tower to the cursor for the player to place
                    ((Sell)other).SellTower();

                    foreach (GameObject obj in GameWorld.Objects) {
                        if ((obj as Upgrade) != null || (obj as Sell) != null) {
                            GameWorld.RemoveObjects.Add(obj);
                        }
                    }

                }
                if (click && placing && GameWorld.MousePos.X > 540) {
                    GameWorld.RemoveObjects.Add(GameWorld.TowerToPlace);
                    GameWorld.TowerToPlace = null;
                    placing = false;

                }
            }
        }

        /// <summary>
        /// Draw Method
        /// </summary>
        /// <param name="dc"></param> 
        public override void Draw(Graphics dc) {
#if DEBUG
            // DEBUG STUFF!!!!

            // Draws out the cursor position
            Font f = new Font("Arial", 25);
            dc.DrawString("PointerPos: " + (Position.X + " : " + Position.Y), f, Brushes.Gold, 160, 0);
#endif

            // Runs base.Draw
            base.Draw(dc);
        }
    }
}
