using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            Name = "Computer";
            Cards = new Cards();
        }
        public override void Attack()
        {

        }
        public override void Defend()
        {

        }
    }
}
