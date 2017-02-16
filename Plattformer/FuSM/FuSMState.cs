using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FuSMState
    {
        protected Point target;
        protected TileGrid grid;
        public FuSMState(Point target, TileGrid grid)
        {
            this.target = target;
            this.grid = grid;
        }

        protected float activationLevel;
        public virtual void Update(GameTime gameTime) { }
        public virtual void Exit() { }
        public virtual void Init() { }
        public virtual float CalculateActivation()
        {
            return activationLevel;
        }

        void CheckLowerBound(float lbound = 0.0f)
        {
            if (activationLevel < lbound)
            {
                activationLevel = lbound;
            }
        }
        void CheckHigherBound(float ubound = 1.0f)
        {
            if (activationLevel > ubound)
            {
                activationLevel = ubound;
            }
        }
        protected virtual void CheckBounds(float lb = 0.0f, float ub = 1.0f)
        {
            CheckLowerBound(lb);
            CheckHigherBound(ub);
        }


    }
}
