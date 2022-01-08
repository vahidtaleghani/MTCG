using MTCG.repository;
using MTCG.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MTCG.endpoints.battle
{
    public class Battle
    {
        private static Battle instance = null;
        private static String Lock = "lock";
        private List<String> playerList = new List<String>();
        DateTime startTime;
        static Random random = new Random();
        private String lastResult = null;
        private String player1;
        private String player2;
        public int i = 0;
        private int eloPlayer1BeforeFight;
        private int eloPlayer2BeforeFight;
        private bool is_played = false;


        private Battle(){ }
        public static Battle getInstance()
        {
            lock (Lock)
            {
                if (instance == null)
                    instance = new Battle();
            }
            return instance;
        }
        public bool addPlayer(String username)
        {
            lock (playerList)
            {
                if (playerList.Contains(username))
                    return false;
                playerList.Add(username);
                if(startTime == DateTime.MinValue)
                    startTime = DateTime.Now;
                return true;
            } 
        }
        public bool isInProgress()
        {
            if ((startTime.AddSeconds(15) > DateTime.Now) && startTime != DateTime.MinValue )
            {
                if (playerList.Count == 2)
                {
                    this.is_played = true;

                    this.player1 = playerList.ElementAt(0);
                    this.player2 = playerList.ElementAt(1);
                    eloPlayer1BeforeFight = new StatReps().getStatsByUsername(this.player1).elo;
                    eloPlayer2BeforeFight = new StatReps().getStatsByUsername(this.player2).elo;

                    Console.WriteLine("| " + player1 + "        |        " + player2);
                    //Battle
                    List<Card> allCardPlayer1 = new CardReps().getDeckByUsername(this.player1);
                    List<Card> allCardPlayer2 = new CardReps().getDeckByUsername(this.player2);

                    while (allCardPlayer1.Count !=0 && allCardPlayer2.Count !=0 && i<100)
                    {
                        int randomCardPlayer1 = getRandomCardNumber(allCardPlayer1);
                        int randomCardPlayer2 = getRandomCardNumber(allCardPlayer2);
                        Card CardOfPlayer1 = allCardPlayer1.ElementAt(randomCardPlayer1);
                        Card CardOfPlayer2 = allCardPlayer2.ElementAt(randomCardPlayer2);

                        Console.WriteLine("| " + CardOfPlayer1.name +":"+ CardOfPlayer1.damage + " | " + CardOfPlayer2.name + ":" + CardOfPlayer2.damage );

                        bool result=false;
                        try
                        {
                            result = Program.fightController.getResult(CardOfPlayer1, CardOfPlayer2);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine("| Error: " + exc.Message);
                        }
                        if (!result)
                            i++;
                        else
                        {
                            deleteCardFromList(randomCardPlayer1, allCardPlayer1);
                            deleteCardFromList(randomCardPlayer2, allCardPlayer2); 
                        }
                    }
                    startTime = DateTime.MinValue;
                    return false;
                }
                return true;
            }
            startTime = DateTime.MinValue;
            playerList.Clear();
            return false;
        }

        public int getRandomCardNumber(List<Card> cardList)
        {
            return random.Next(cardList.Count);
        }
        public void deleteCardFromList(int index, List<Card> cardList)
        {
            cardList.RemoveAt(index);
        }
        public String getLastResult()
        {
            int eloPlayer1AfterFight = new StatReps().getStatsByUsername(this.player1).elo;
            int eloPlayer2AfterFight = new StatReps().getStatsByUsername(this.player2).elo;
            if (eloPlayer1AfterFight - this.eloPlayer1BeforeFight > eloPlayer2AfterFight - this.eloPlayer2BeforeFight)
                this.lastResult = this.player1 + " Is Winner";
            else if(eloPlayer1AfterFight - this.eloPlayer1BeforeFight < eloPlayer2AfterFight - this.eloPlayer2BeforeFight)
                this.lastResult = this.player2 + " Is Winner";
            else
                this.lastResult = "DRAW";
            return this.lastResult;
        }
        public bool isPlayed()
        {
            return this.is_played;
        }
    }
}
