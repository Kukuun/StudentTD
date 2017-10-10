﻿using System.Drawing;

namespace StudentTD {
    class NormalTower : Tower {
        /// <summary>
        /// Sets the damage, range, price, totalvalue and speed for the NormalTower. 
        /// </summary>
        /// <param name="imagePath">Gets the path to a single sprite or to multiple sprites</param>
        /// <param name="startPos">All NormalTowers position</param>
        /// <param name="animationSpeed">The speed for animations</param>
        /// <param name="scaleFactor">A factor to scale sprites</param>
        /// <param name="speed">The speed for which the tower shoots</param>
        /// <param name="placed">A bool which checks if a tower is placed</param>
        /// <param name="beingPlaced">A bool that checks if a tower is beingplaced</param>
        /// <param name="selected">A bool that checks if a tower is selected</param>
        public NormalTower(string imagePath, PointF startPos, float animationSpeed, float scaleFactor, float speed, bool placed, bool beingPlaced, bool selected)
            : base(imagePath, startPos, animationSpeed, scaleFactor, speed, placed, beingPlaced, selected) {
            damage = 6;
            range = 75;
            price = 7;
            TotalValue = price;
        }

        /// <summary>
        /// Overrides the method that is created in GameObject. Updates the sprite for NormalTower depending on players amount of currency. 
        /// </summary>
        /// <param name="fps">Used to get a more stabile game speed</param>
        public override void UpdateAnimation(float fps) {
            if (beingPlaced || (!placed && GameWorld.Currency < price)) {
                Sprite = Image.FromFile(@"sprites\towers\normaltowerOpac.png");
            }
            else if (!placed && GameWorld.Currency >= price) {
                Sprite = Image.FromFile(@"sprites\towers\normaltower.png");
            }

            base.UpdateAnimation(fps);
        }

        /// <summary>
        /// Overrides the absract SetNextUpgrade method from the Tower class
        /// </summary>
        public override void SetNextUpgrade() {
            if (upgradeLevel < 20) {
                GameWorld.Currency -= price;
                upgradeLevel++;
                TotalValue += price;


                damage = (int)(damage * 1.6);
                range = 75;
                speed = 400;
                price = (int)(price * 1.6);
            }
        }

        /// <summary>
        /// Has no function in this class
        /// </summary>
        /// <param name="other">Is what the tower collides with, for instance an enemy</param>
        public override void OnCollision(GameObject other) {

        }
    }
}