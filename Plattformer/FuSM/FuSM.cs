using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FuSM
    {
        
        List<FuSMState> states = new List<FuSMState>();
        List<FuSMState> activeStates = new List<FuSMState>();

        public FuSM(AiFuSMControl control)
        {

        }

        public void UpdateMachine(GameTime gameTime)
        {
            if(states.Count() == 0)
            {
                return;
            }

            activeStates.Clear();
            List<FuSMState> nonActiveStates = new List<FuSMState>();
            for (int i = 0; i < states.Count(); i++)
            {
                if(states[i].CalculateActivation() > 0)
                {
                    activeStates.Add(states[i]);
                }
                else
                {
                    nonActiveStates.Add(states[i]);
                }
            }

            if(nonActiveStates.Count() != 0)
            {
                for (int i = 0; i < nonActiveStates.Count(); i++)
                {
                    nonActiveStates[i].Exit();
                }
            }

            if(activeStates.Count() != 0)
            {
                for (int i = 0; i < activeStates.Count(); i++)
                {
                    activeStates[i].Update(gameTime);
                }
            }
        }

        public void AddState(FuSMState state)
        {
            states.Add(state);
        }

        public bool IsActive(FuSMState state)
        {
            if(activeStates.Count() != 0)
            {
                for (int i = 0; i < activeStates.Count(); i++)
                {
                    if(activeStates[i] == state)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            for (int i = 0; i < states.Count(); i++)
            {
                states[i].Exit();
                states[i].Init();
            }
        }
    }
}
