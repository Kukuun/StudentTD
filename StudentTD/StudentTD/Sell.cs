using System.Drawing;

namespace StudentTD {
    class Sell : GameObject {
        /// <summary>
        /// Variable we use when determining what tower to sell.
        /// </summary>
        private Tower sellTarget;

        public Sell(string imagePath, PointF startPos, float animationSpeed, float scaleFactor, float speed, Tower sellTarget)
            : base(imagePath, startPos, animationSpeed, scaleFactor, speed) {
            this.sellTarget = sellTarget;
        }

        /// <summary>
        /// Method used to determine the amount of gold you get when selling a tower
        /// </summary>
        public void SellTower() {
            GameWorld.Currency += (int)(sellTarget.TotalValue * 0.75f);
            GameWorld.RemoveObjects.Add(sellTarget);
        }
    }
}
