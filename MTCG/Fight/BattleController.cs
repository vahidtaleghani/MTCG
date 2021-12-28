using MTCG.endpoints.battle.play;
using MTCG.helper;
using MTCG.Play;
using MTCG.repository;
using MTCG.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static MTCG.endpoints.battle.MonsterType;

namespace MTCG.endpoints.battle
{
    public class BattleController
    {
        private static BattleController instance = null;
        private static String Lock = "lock";
        private List<String> playerList = new List<String>();
        DateTime startTime;
        static Random random = new Random();
        public int i = 0;

        private BattleController(){ }
        public static BattleController getInstance()
        {
            lock (Lock)
            {
                if (instance == null)
                    instance = new BattleController();
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
            if (startTime.AddSeconds(15) > DateTime.Now && startTime != DateTime.MinValue)
            {
                if (playerList.Count == 2)
                {
                    String player1 = playerList.ElementAt(0);
                    String player2 = playerList.ElementAt(1);

                    Console.WriteLine("| " + player1 + "        |        " + player2);

                    //Battle
                    List<Card> allCardPlayer1 = new CardReps().getAllCardInDeckByUsername(player1);
                    List<Card> allCardPlayer2 = new CardReps().getAllCardInDeckByUsername(player2);

                    while (allCardPlayer1.Count !=0 && allCardPlayer2.Count !=0)
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
                        if (result)
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
        
        private String lastResult = "OK";
        public String getLastResult()
        {
            return this.lastResult;
        }
    }
}
