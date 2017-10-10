using System.Drawing;

namespace StudentTD {
    class Upgrade : GameObject {
        /// <summary>
        /// Variable used when wanting to upgrade a tower.
        /// </summary>
        private Tower upgradeTarget;

        /// <summary>
        /// Sets the values for the different variables.
        /// </summary>
        /// <param name="imagePath">Gets the path to a single sprite or to multiple sprites</param>
        /// <param name="startPos">Position for the upgrade button</param>
        /// <param name="animationSpeed">The speed for animations</param>
        /// <param name="scaleFactor">A factor to scale sprites</param>
        /// <param name="speed">The speed for which the tower shoots</param>
        /// <param name="upgradeTarget"></param>
        public Upgrade(string imagePath, PointF startPos, float animationSpeed, float scaleFactor, float speed, Tower upgradeTarget)
            : base(imagePath, startPos, animationSpeed, scaleFactor, speed) {
            this.upgradeTarget = upgradeTarget;
        }

        /// <summary>
        /// Our method when we want to upgrade a tower.
        /// </summary>
        public void UpgradeTower() {
            if (GameWorld.Currency >= upgradeTarget.Price) {
                if ((upgradeTarget as NormalTower) != null) {
                    ((NormalTower)upgradeTarget).SetNextUpgrade();
                }
                if ((upgradeTarget as AirTower) != null) {
                    ((AirTower)upgradeTarget).SetNextUpgrade();
                }
                if ((upgradeTarget as SplashTower) != null) {
                    ((SplashTower)upgradeTarget).SetNextUpgrade();
                }
                if ((upgradeTarget as SlowTower) != null) {
                    ((SlowTower)upgradeTarget).SetNextUpgrade();

                    if (upgradeTarget.UpgradeLevel >= 5) {
                        GameWorld.RemoveObjects.Add(this);
                    }
                }
            }
            if (upgradeTarget.UpgradeLevel >= 20) {
                GameWorld.RemoveObjects.Add(this);
            }
        }

        /// <summary>
        /// A method that updates the Upgrade image used depending if you can afford it or not.
        /// </summary>
        /// <param name="fps"></param>
        public override void UpdateAnimation(float fps) {
            if (GameWorld.Currency < upgradeTarget.Price) {
                Sprite = Image.FromFile(@"sprites\buttons\upgradeOpac.png");
            }
            else if (GameWorld.Currency >= upgradeTarget.Price) {
                Sprite = Image.FromFile(@"sprites\buttons\upgrade.png");
            }

            base.UpdateAnimation(fps);
        }
    }
}