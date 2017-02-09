using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FuSMenemy : BaseEnemy
    {
        enum State
        {
            idel,
            attack,
            hide
        }
        State currentState = State.idel;

        public FuSMenemy(Texture2D texture, int startPositionX, int startPositionY, Point sleep) :
            base(texture, startPositionX, startPositionY, sleep)
        {

        }


    }
}
