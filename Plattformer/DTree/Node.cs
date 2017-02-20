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
        public BaseEnemy enemy;
        public State behaviour;
        public Node(int value, bool question, BaseEnemy enemy, State behaviour)
        {
            this.value = value;
            this.question = question;
            this.enemy = enemy;
            this.behaviour = behaviour;
        }



    }
}
