/*
 * 
 * 
 * 
 * 
 */
using System;

namespace Durak
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(String name = "Player", int numCards = 36)
        {
            Name = name;
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
