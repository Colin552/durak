﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Durak
{
    /// <summary>
    /// Interaction logic for Rules.xaml
    /// </summary>
    public partial class RulesForm : Window
    {
        public RulesForm()
        {
            InitializeComponent();

            PrintRules();
        }

        private void PrintRules()
        {
            string rules = "";

            rules = "This game has no winner, only a loser.\n";
            rules += "A typical game is played with 36 cards, one from each suit ranked from high to low as: ace, king, queen, jack, 10, 9, 8, 7, and 6.\n";
            rules += "\nEach player starts with a hand of six cards dealt face down.\n\n";
            rules += "The next card in the deck is put in the center, face up, and determines the \"trump\" suit. The remaining cards are placed face down on top\n" +
                " of the trump card, crosswise so that you can still view the value of the trump.\n";
            rules += "In the first hand of a session, the player with the lowest trump plays first, though you do not need to play that card first.\n";
            rules += "An attacker plays a card that a defender must beat by (in the case of a non-trump card) playing a higher card of the same suit, or any trump.\n";
            rules += "Or (in the case of a trump card being played) by playing a higher trump card.\n";
            rules += "The defender can also choose to pick up the card and add it to their hand, meaning the attack has succeeded.\n\n";
            rules += "The player who makes it to 0 cards first, wins.\n";

            lblRules.Content = rules;
        }
    }
}
