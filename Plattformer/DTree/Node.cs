using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.DTree
{
    class Node
    {
        public int value;
        public bool question;
        public Node left, right;
        public DTenemy enemy;
        public State behaviour;
        public Node(int value, bool question, DTenemy enemy, State behaviour)
        {
            this.enemy = enemy;
            this.value = value;
            this.question = question;
            this.behaviour = behaviour;
        }

        //public bool FindEnemy()
        //{
        //    return enemy.FoundPlayer(enemy.)
        //}



    }
}
