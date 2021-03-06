﻿using System.Drawing;

namespace StudentTD {
    public class Wave : GameObject {
        /// <summary>
        /// Variable for managing waves
        /// </summary> 
        private int waveCounter;
        private int currentWave;
        private float difficulty;
        private float difficultyIncrease;
        private int monsterHealth;

        /// <summary>
        /// Currency Management
        /// </summary> 
        private bool earnInterest;
        private int interest;

        /// <summary>
        /// Variable for managing spawn
        /// </summary> 
        private bool spawning;
        private int spawnCounter;
        private float spawnAccumilator = 0.0f;
        private float spawnPerSec = 2f;

        /// <summary>
        /// Properties
        /// </summary> 
        public int CurrentWave {
            get { return currentWave; }
            set { currentWave = value; }
        }

        /// <summary>
        /// Constructor for the creep wave.
        /// </summary>
        /// <param name="imagePath">Gets the path of the creep images</param>
        /// <param name="startPos">Wave position</param>
        /// <param name="animationSpeed">The speed for animations</param>
        /// <param name="scaleFactor">A factor to scale sprites</param>
        /// <param name="speed">The speed for which the wave moves</param> 
        public Wave(string imagePath, PointF startPos, float animationSpeed, float scaleFactor, float speed)
            : base(imagePath, startPos, animationSpeed, scaleFactor, speed) {
            // Resets waves
            waveCounter = 0;
            currentWave = 0;
            interest = 10;
            difficulty = 1.175f;
            difficultyIncrease = 10;
        }

        /// <summary>
        /// Method to handle next wave
        /// </summary> 
        public void NextWave() {
            if (!spawning) {
                // Enables spawning
                spawning = true;

                // Checks to see if any monsters are still present from last wave
                foreach (GameObject obj in GameWorld.Objects) {
                    if ((obj as Enemy) != null) {
                        spawning = false;
                    }
                }

                if (spawning) {
                    // if there is no cooldown, the next wave will begin to spawn in next update
                    // Resets spawncounter
                    spawnCounter = 0;

                    earnInterest = true;

                    // Checks if the wavecounter is higher than total waves
                    if (waveCounter >= 5) {
                        // Resets waves
                        waveCounter = 0;
                    }

                    // Adds to the currentWave and waveCounter
                    waveCounter++;
                    currentWave++;

                    monsterHealth += (int)(difficultyIncrease);

                    difficultyIncrease *= difficulty;
                }
            }
        }

        /// <summary>
        /// Update Method
        /// </summary>
        /// <param name="deltaTime"></param> 
        public override void Update(float deltaTime) {
            // Calculates with framerate how many monsters to spawn this update
            spawnAccumilator += spawnPerSec * deltaTime;
            int toSpawn = (int)spawnAccumilator;
            spawnAccumilator -= (float)toSpawn;

            // Runs the spawn loop
            for (int i = 0; i < toSpawn && spawnCounter < 25 && spawning; i++) {
                // Adds to the spawn for each loop
                spawnCounter++;

                // Switch case to manage what wave to spawn
                switch (waveCounter) {
                    case 1:
                        // |                ObjectName |Sprites                        |Position        |ASpd|SclFac|Health  |Spd |Gold drop                                  |Flying|Immune  |
                        GameWorld.NewObjects.Add(new Enemy(@"sprites\enemies\normalmob.png", new PointF(130, -40), 0, 1, monsterHealth, 100, 1 + (int)(difficultyIncrease * 0.04), false, false));
                        break;
                    case 2:
                        GameWorld.NewObjects.Add(new Enemy(@"sprites\enemies\fastmob.png", new PointF(130, -40), 0, 1, monsterHealth * 0.8f, 150, 1 + (int)(difficultyIncrease * 0.04), false, false));
                        break;
                    case 3:
                        GameWorld.NewObjects.Add(new Enemy(@"sprites\enemies\airmob.png", new PointF(130, -40), 0, 1, monsterHealth, 100, 1 + (int)(difficultyIncrease * 0.04), true, false));
                        break;
                    case 4:
                        GameWorld.NewObjects.Add(new Enemy(@"sprites\enemies\immunemob.png", new PointF(130, -40), 0, 1, monsterHealth * 0.8f, 100, 1 + (int)(difficultyIncrease * 0.04), false, true));
                        break;
                    case 5:
                        GameWorld.NewObjects.Add(new Enemy(@"sprites\enemies\bossmob.png", new PointF(130, -40), 0, 1, (monsterHealth * 8), 75, 10 + (int)(difficultyIncrease * 0.4), false, false));

                        // Boss fix
                        spawnCounter = 25;

                        break;
                    default:
                        break;
                }
            }

            // Stops spawning when 25 monsters have spawned
            if (spawnCounter >= 25) {
                spawning = false;
            }

            if (earnInterest && !spawning) {
                bool completionChecker = true;

                // Checks to see if any monsters are still present from last wave
                foreach (GameObject obj in GameWorld.Objects) {
                    if ((obj as Enemy) != null) {
                        completionChecker = false;
                        break;
                    }
                }
                foreach (GameObject obj in GameWorld.NewObjects) {
                    if ((obj as Enemy) != null) {
                        completionChecker = false;
                        break;
                    }
                }

                if (earnInterest && completionChecker) {
                    GameWorld.Currency += interest + (int)(difficultyIncrease * 0.2);

                    earnInterest = false;
                }
            }

            // Runs base.Update
            base.Update(deltaTime);
        }

        /// <summary>
        /// Method for that draws the text that announces current and next creep wave
        /// </summary>
        /// <param name="dc"></param>
        public override void Draw(Graphics dc) {
            Font font = new Font("Arial", 12);

            if (waveCounter == 0) {
                dc.DrawString("First creep type: Normal", font, Brushes.Black, 580, 540);
            }
            if (waveCounter == 1) {
                dc.DrawString("Creep type: Normal", font, Brushes.Black, 561, 53);
                dc.DrawString("Next creep type: Fast", font, Brushes.Black, 580, 540);
            }
            if (waveCounter == 2) {
                dc.DrawString("Creep type: Fast", font, Brushes.Black, 561, 53);
                dc.DrawString("Next creep type: Air", font, Brushes.Black, 580, 540);
            }
            if (waveCounter == 3) {
                dc.DrawString("Creep type: Air", font, Brushes.Black, 561, 53);
                dc.DrawString("Next creep type: Immune", font, Brushes.Black, 580, 540);
            }
            if (waveCounter == 4) {
                dc.DrawString("Creep type: Immune", font, Brushes.Black, 561, 53);
                dc.DrawString("Next creep type: Boss", font, Brushes.Black, 580, 540);
            }
            if (waveCounter == 5) {
                dc.DrawString("Creep type: Boss", font, Brushes.Black, 561, 53);
                dc.DrawString("Next creep type: Normal", font, Brushes.Black, 580, 540);
            }

            base.Draw(dc);

#if DEBUG

            // Sets a font
            Font f = new Font("Arial", 18);

            // DEBUG STUFF!
            // FPS
            dc.DrawString("BaseHP: " + monsterHealth.ToString(), f, Brushes.Red, 0, 300);
#endif
        }
    }
}