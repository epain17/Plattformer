using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FuSMState
    {
        public FuSMState()
        { }

        float activationLevel;
        protected virtual void Update(GameTime gameTime) { }
        protected virtual void Enter() { }
        protected virtual void Exit() { }
        protected virtual void Init() { }
        float CalculateActivation()
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
        void CheckBounds(float lb = 0.0f, float ub = 1.0f)
        {
            CheckLowerBound(lb);
            CheckHigherBound(ub);
        }


    }
}
