/*
 * 
 * 
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Durak
{
    /// <summary>
    /// This class will contain the game loop and functions relating to playing the game
    /// </summary>
    public class Game
    {
        // Static variables for the deck, human and computer player
        private GUI myGUI;
        private Deck myDeck = new Deck();
        private HumanPlayer humanPlayer = new HumanPlayer("Runescape");
        private ComputerPlayer computerPlayer = new ComputerPlayer();
        public HumanPlayer HumanPlayer { get => humanPlayer; set => humanPlayer = value; }
        public ComputerPlayer ComputerPlayer { get => computerPlayer; set => computerPlayer = value; }
        public Deck MyDeck { get => myDeck; set => myDeck = value; }
        public GUI MyGUI { get => myGUI; set => myGUI = value; }
        public Card ComputerDecidedCard { get => computerDecidedCard; set => computerDecidedCard = value; }
        public Card CurrentCardInPlay { get => currentCardInPlay; set => currentCardInPlay = value; }

        public Suit trumpSuit;

        private int[] statistics = new int[3];
        private Card computerDecidedCard;
        private Card currentCardInPlay;
        private Cards cardsInPlay = new Cards();
        private Player currentPlayer;
        private bool attackTurn = true;
        private Player lastPlayer;
        private Player attackingPlayer;

        private const int indexGames = 0;
        private const int indexWins = 1;
        private const int indexLosses = 2;


        public Game(int numOfCards)
        {
            Deck newDeck = new Deck(numOfCards);
            MyDeck = newDeck;
        }

        /// <summary>
        /// Play - The main game loop
        /// </summary>
        public void Play()
        {
            ReadStatisticsFile();
            // Clear the current log text file
            File.WriteAllText("../../Log/log.txt.", string.Empty);
            // Write new game in text file
            WriteLogFile("Date: " + DateTime.Now.ToString());
            WriteLogFile("Game start, player name: " + humanPlayer.Name);
            WriteLogFile("");
            myGUI.CurrentPlayer = humanPlayer;
            InitialDraw();
            SetTrump();
            WriteLogFile("Trump suit: " + trumpSuit);
            currentPlayer = DetermineAttacker();
            WriteLogFile("Attacking first: " + currentPlayer.Name);
            WriteLogFile("");
            humanPlayer.CanPlayCard = true;
            attackTurn = true;

            EndMove();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ComputerPlayerTurn()
        {
            // Set the current player to the computer 
            myGUI.CurrentPlayer = computerPlayer;

            // Allow the computer to make a decision
            ComputerDecidedCard = computerPlayer.MakeMove(trumpSuit, CurrentCardInPlay);
            // For each of the computers cards in hand, find which one matches the returned\
            // Choice for card to play and get its index
            if (ComputerDecidedCard != null)
            {
                // Remove the card from the grid
                myGUI.RemoveCardImage(myGUI.OpponentGrid, ComputerDecidedCard.myImage);
                // Move the card to the middle
                myGUI.MoveCardImage(myGUI.CenterGrid, ComputerDecidedCard.myImage, 0);
                CurrentCardInPlay = ComputerDecidedCard;
                WriteLogFile(computerPlayer.Name + " played " + ComputerDecidedCard.ToString());
            }

            myGUI.CurrentPlayer = HumanPlayer;
            EndMove();

        }

        /// <summary>
        /// EndMove - Called when either the computer or player finishes his move
        /// </summary>
        public void EndMove()
        {
            //System.Diagnostics.Debug.WriteLine("\nNew Move");
            //System.Diagnostics.Debug.WriteLine("Attack turn: " + attackTurn);

            if (lastPlayer != null)
            {
                //System.Diagnostics.Debug.WriteLine(lastPlayer.Name + " played a card: " + lastPlayer.PlayedCard);
                if (attackTurn == false && lastPlayer.PlayedCard == false)
                {
                    EndTurn();
                }
            }

            if (CurrentCardInPlay != null)
            {
                cardsInPlay.Add(currentCardInPlay);
                System.Diagnostics.Debug.WriteLine("Card in play: " + CurrentCardInPlay.ToString());
            }

            if (currentPlayer == HumanPlayer)
            {
                humanPlayer.CanPlayCard = true;
                humanPlayer.PlayedCard = false;
                myGUI.OrderCards();
                currentPlayer = ComputerPlayer;
                lastPlayer = HumanPlayer;
                System.Diagnostics.Debug.WriteLine("Player's turn");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Computer's turn");
                currentPlayer = HumanPlayer;
                lastPlayer = ComputerPlayer;
                ComputerPlayerTurn();
            }
            attackTurn = false;
            
            if (myDeck.CardsRemaining() == 0)
            {
                myGUI.RemoveDeckImage();
                myGUI.SetDeckLabelText("NO CARDS LEFT!");
            }
            else
            {
                myGUI.SetDeckLabelText(myDeck.CardsRemaining().ToString());
            }
            Console.WriteLine("Computer card count: " + computerPlayer.Cards.Count);
            Console.WriteLine("Player card count: " + humanPlayer.Cards.Count);

            

            //if (CheckForWinner())
            //{
            //    // do stuff
            //    Console.WriteLine("Winner");
            //}
        }

        /// <summary>
        /// Method called when the bout has ended
        /// Checks to see if there is a winner,
        /// Checks to see if the defending player lost or not
        /// </summary>
        public void EndTurn()
        {
            System.Diagnostics.Debug.WriteLine("ENDING TURN");
            System.Diagnostics.Debug.WriteLine("Player taking the cards: " + lastPlayer.Name);
            System.Diagnostics.Debug.WriteLine("Center Cards");

            WriteLogFile("");
            WriteLogFile("End of bout");
            myGUI.RemoveRiver(cardsInPlay);
            // If the defending player does not win the bout
            // Move cards to players hand
            if(lastPlayer != attackingPlayer)
            {

                foreach (Card card in cardsInPlay)
                {
                    System.Diagnostics.Debug.WriteLine(card.ToString());
                    lastPlayer.Cards.Add(card);
                }

                if (lastPlayer == computerPlayer)
                {
                    WriteLogFile(computerPlayer.Name + " lost the defence.");
                    myGUI.MoveRiver(myGUI.OpponentGrid);
                    attackingPlayer = humanPlayer;
                    MyGUI.SetLabelText("You are attacking");
                    UpdatePlayers(computerPlayer);
                }
                else
                {
                    WriteLogFile(humanPlayer.Name + " lost the defence.");
                    myGUI.MoveRiver(myGUI.PlayerGrid);
                    attackingPlayer = ComputerPlayer;
                    MyGUI.SetLabelText("Computer is attacking");
                    UpdatePlayers(humanPlayer);
                }
            }
            // If the defending player won the bout
            // Move the cards to the discard pile
            else
            {
                myGUI.PlaceDiscardPile();
                //MessageBox.Show("No Draw " + attackingPlayer.Name);
                if (attackingPlayer is ComputerPlayer)
                {
                    WriteLogFile(attackingPlayer.Name + " failed the attack.");
                    MyGUI.SetLabelText("You are attacking");
                    attackingPlayer = humanPlayer;
                    WriteLogFile(humanPlayer.Name + " is now attacking...");
                }
                else
                {
                    MyGUI.SetLabelText("Computer is attacking");
                    WriteLogFile(humanPlayer.Name + " failed the attack.");
                    attackingPlayer = computerPlayer;
                    WriteLogFile(computerPlayer.Name + " is now attacking...");
                }
            }
            WriteLogFile("");
            WriteLogFile("");
            Draw(computerPlayer);
            Draw(humanPlayer);
            attackTurn = true;
            cardsInPlay.Clear();
            currentCardInPlay = null;
            CheckForWinner();
        }

        /// <summary>
        /// Calls from the GUI class so that the Player objects in the game get an updated hand
        /// When they play a card
        /// </summary>
        /// <param name="player"></param>
        public void UpdatePlayers(Player player)
        {
            if (player is HumanPlayer)
            {
                HumanPlayer.Cards = player.Cards;
            }
            else
                ComputerPlayer.Cards = player.Cards;
        }



        /// <summary>
        /// InitialDraw - Draws the initial 6 cards
        /// </summary>
        public void InitialDraw()
        {
            // Changed code from here previously
            // before it was 'Card myCard = myDeck.GetCard(i)'
            // Now is Card 'myCard = myDeck.GetTopCard()'
            WriteLogFile("Cards in " + HumanPlayer.Name + "'s starting hand:");
            Draw(HumanPlayer);
            WriteLogFile("");
            WriteLogFile("Cards in " + ComputerPlayer.Name + "'s starting hand:");
            Draw(ComputerPlayer);
            //WriteLogFile("");
            myGUI.CurrentPlayer = humanPlayer;
            WriteLogFile("");
        }

        /// <summary>
        /// Sets the trump suit for the game
        /// </summary>
        public void SetTrump()
        {
            Card trumpCard = MyDeck.GetTopCard();
            trumpSuit = trumpCard.suit;
            myGUI.PlaceTrumpCard(trumpCard);
            //System.Diagnostics.Debug.WriteLine("Trump Suit: " + trumpSuit);
        }

        /// <summary>
        /// Determines the attacking player
        /// </summary>
        /// <returns>The player who is attacking</returns>
        public Player DetermineAttacker()
        {
            //Temporary values set higher than a king.
            Rank humanLowestRank = (Rank)14;
            Rank computerLowestRank = (Rank)14;

            //Loops through the computer and human player's hands and finds their lowest rank of the trump suit
            for (int i = 1; humanPlayer.Cards.Count > i; i++)
            {
                if (humanPlayer.Cards[i - 1].rank < humanLowestRank && humanPlayer.Cards[i - 1].suit == trumpSuit)
                {
                    humanLowestRank = humanPlayer.Cards[i - 1].rank;
                }
            }

            for (int i = 1; humanPlayer.Cards.Count > i; i++)
            {
                if (computerPlayer.Cards[i - 1].rank < computerLowestRank && computerPlayer.Cards[i - 1].suit == trumpSuit)
                {
                    computerLowestRank = computerPlayer.Cards[i - 1].rank;
                }
            }

            // Returns the player with the lowest rank of the trump suit
            if (computerLowestRank > humanLowestRank)
            {
                MyGUI.SetLabelText("You are attacking");
                attackingPlayer = humanPlayer;
                currentPlayer = HumanPlayer;
                return HumanPlayer;
            }
            else if (computerLowestRank < humanLowestRank)
            {
                MyGUI.SetLabelText("Computer is attacking");
                attackingPlayer = ComputerPlayer;
                currentPlayer = ComputerPlayer;
                return ComputerPlayer;
            }
            else
            {
                MyGUI.SetLabelText("Tie, you are attacking");
                attackingPlayer = humanPlayer;
                currentPlayer = HumanPlayer;
                return humanPlayer;
            }
        }

        /// <summary>
        /// Draws a new card from the deck
        /// Sends over the card and the Player object to the GUI class
        /// Attempted to be able to send all the 
        /// </summary>
        /// <param name="player"></param>
        public void Draw(HumanPlayer player)
        {
            myGUI.CurrentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                if (myCard != null)
                {
                    //System.Diagnostics.Debug.WriteLine("New Card: " + myCard);
                    myGUI.MoveCardImage(myGUI.PlayerGrid, myCard.myImage, i, 0);
                    player.Cards.Add(myCard);
                    WriteLogFile(player.Name + " drew " + myCard.ToString());
                }
            }
            //System.Diagnostics.Debug.WriteLine("Cards: " + player.Cards.Count());
        }

        /// <summary>
        ///  Draw method for the computer player
        /// </summary>
        /// <param name="player"></param>
        public void Draw(ComputerPlayer player)
        {
            myGUI.CurrentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                if (myCard != null)
                {
                    myGUI.CurrentCard = myCard;
                    myGUI.MoveCardImage(myGUI.OpponentGrid, myCard.myImage, i, 0);
                    player.Cards.Add(myCard);
                    // Write to log file
                    WriteLogFile(player.Name + " drew " + myCard.ToString());
                    myCard.SetFaceDown();
                }
            }
        }

        /// <summary>
        /// Checks if one of the players has won
        /// </summary>
        /// <returns></returns>
        public bool CheckForWinner()
        {
            if (humanPlayer.Cards.Count == 0)
            {
                WriteLogFile("-----------------------");
                WriteLogFile(humanPlayer.Name + " HAS WON!");
                WriteLogFile("-----------------------");
                statistics[indexGames] += 1;
                statistics[indexWins] += 1;
                WriteStatisticsFile();
                DurakLose durak = new DurakLose(computerPlayer);
                durak.Show();
                return true;
            }
            else if (computerPlayer.Cards.Count == 0)
            {
                WriteLogFile("-----------------------");
                WriteLogFile(computerPlayer.Name + " HAS WON!");
                WriteLogFile("-----------------------");
                statistics[indexGames] += 1;
                statistics[indexLosses] += 1;
                DurakLose durak = new DurakLose(humanPlayer);
                durak.Show();
                WriteStatisticsFile();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Card_MouseMove - Event handler for the Card's drag and drop functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (humanPlayer.CanPlayCard)
            {
                Image cardImage = sender as Image;
                if (cardImage != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    string stringRank = cardImage.Name.Split('_')[0];
                    string stringSuit = cardImage.Name.Split('_')[2];
                    Rank cardRank = (Rank)Enum.Parse(typeof(Rank), stringRank);
                    Suit cardSuit = (Suit)Enum.Parse(typeof(Suit), stringSuit);
                    Card selectedCard = new Card(cardSuit, cardRank);


                    if (ValidMove(selectedCard))
                    {
                        CurrentCardInPlay = selectedCard;
                        // Write to the log file
                        WriteLogFile(humanPlayer.Name + " played " + selectedCard.ToString());
                        DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
                    }
                }
            }
        }

        /// <summary>
        /// Checks to see if the Human players move is valid or not
        /// </summary>
        /// <param name="cardToPlay"></param>
        /// <returns>True if the move is valid, flase if not</returns>
        public bool ValidMove(Card cardToPlay)
        {
            bool isValid = true;

            //if (currentPlayer == computerPlayer)
            //{
            if (CurrentCardInPlay != null)
            {
                if (cardToPlay.suit == CurrentCardInPlay.suit || cardToPlay.suit == trumpSuit && currentCardInPlay.suit != trumpSuit)
                {
                    if (!(cardToPlay.suit == trumpSuit && currentCardInPlay.suit != trumpSuit))
                    {
                        if ((cardToPlay.rank < CurrentCardInPlay.rank))
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            //}
            return isValid;
        }

        /// <summary>
        /// Appends a message onto the current Log File
        /// </summary>
        /// <param name="message">The message that will be written to the file</param>
        public void WriteLogFile(string message)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("../../Log/log.txt", true))
            {
                file.WriteLine(message);
            }
        }

        /// <summary>
        /// Writes the statistics file
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file"/>
        /// </summary>
        public void WriteStatisticsFile()
        {
            File.WriteAllText("../../Log/statistics.txt.", string.Empty);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("../../Log/statistics.txt", true))
            {
                file.WriteLine("Name: " + humanPlayer.Name);
                file.WriteLine();
                file.WriteLine("Number of Games");
                file.WriteLine(statistics[indexGames]);
                file.WriteLine();
                file.WriteLine("Wins");
                file.WriteLine(statistics[indexWins]);
                file.WriteLine();
                file.WriteLine("Losses");
                file.WriteLine(statistics[indexLosses]);
            }
        }

        /// <summary>
        /// Reads the statistics file to save current data
        /// <see cref="https://stackoverflow.com/questions/32500084/read-second-line-and-save-it-from-txt-c-sharp"/>
        /// </summary>
        public void ReadStatisticsFile()
        {
            int numGames;
            int numWins;
            int numLosses;
            using (var reader = new StreamReader("../../Log/statistics.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                // Skip first 3 lines
                int.TryParse(reader.ReadLine(), out numGames);
                reader.ReadLine();
                reader.ReadLine();
                // Skip next lines just to get to data
                int.TryParse(reader.ReadLine(), out numWins);
                reader.ReadLine();
                reader.ReadLine();
                // Skip lines again to access wanted data
                int.TryParse(reader.ReadLine(), out numLosses);
            }
            // Save the retireved data in the statistics array
            statistics[indexGames] = numGames;
            statistics[indexWins] = numWins;
            statistics[indexLosses] = numLosses;
        }
    }
}
