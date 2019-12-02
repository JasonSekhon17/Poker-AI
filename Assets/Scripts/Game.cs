using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    public enum Stages {
        RoundStart,
        Preflop,
        Flop,
        Turn,
        River,
        RoundEnd
    };

    public Stages stage;

    public bool waiting;

    [Range(2, 6)]
    public int numPlayers = 2;

    public Player playerPrefab;

    public List<Player> players;

    public List<GameObject> playerSlots;

    public int currentPlayerIndex;

    public Player currentPlayer;
    public Player humanPlayer;

    public SharedCards sharedCards;

    public Deck deck;

    public StandardPokerHandEvaluator.PokerHandRankingTable rankingTable;

    public List<StandardPokerHandEvaluator.Hand> handsEval;
    
    // Start is called before the first frame update
    void Start()
    {
        rankingTable = new StandardPokerHandEvaluator.PokerHandRankingTable();
        handsEval = new List<StandardPokerHandEvaluator.Hand>();
        Player temp = Instantiate(playerPrefab, transform.position, transform.rotation, playerSlots[0].transform);
        temp.transform.localPosition = new Vector3(0,0,0);
        temp.transform.localRotation = Quaternion.Euler(0,0,0);
        temp.playerIsAI = false;
        players.Add(temp);
        for (int i = 1; i < numPlayers; i++) {
            temp = Instantiate(playerPrefab, transform.position, transform.rotation, playerSlots[i].transform);
            temp.transform.localPosition = new Vector3(0,0,0);
            temp.transform.localRotation = Quaternion.Euler(0,0,0);
            temp.playerIsAI = true;
            players.Add(temp);
        }
        currentPlayerIndex = Random.Range(0, numPlayers - 1);
        currentPlayer = players[currentPlayerIndex];
        currentPlayer.bigBlind = true;
        if (currentPlayerIndex <= 0)
            players.Last().smallBlind = true;
        else
            players[currentPlayerIndex - 1].smallBlind = true;
        humanPlayer = players[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer.state == Player.States.Active )
            waiting = true;

        if (!waiting) {
            Debug.Log(currentPlayerIndex);
            if (stage == Stages.RoundStart) {
                RoundStart();
            } else if (stage == Stages.Preflop) {
                Preflop();
            } else if (stage == Stages.Flop) {
                Flop();
            } else if (stage == Stages.River) {
                River();
            } else if (stage == Stages.Turn) {
                Turn();
            } else if (stage == Stages.RoundEnd) {
                RoundEnd();
            }
        }
    }

    void RoundStart() {
        Debug.Log("Round start");
        foreach(Player player in players) {
            player.hand.DrawCards();
            if (player.playerIsAI)
                player.record = new MoveRecord();
            else
                player.classifier = new PlayerClassifier();
            if (player.bigBlind)
                player.BetFlop(4);
        }
        stage = Stages.Preflop;
    }

    void Preflop() {
        Debug.Log("Preflop for player " + (currentPlayerIndex + 1));
        currentPlayer.state = Player.States.Active;
    }

    void Flop() {
        Debug.Log("Flop for player " + (currentPlayerIndex + 1));
        currentPlayer.state = Player.States.Active;
    }

    void River() {
        Debug.Log("River for player " + (currentPlayerIndex + 1));
        currentPlayer.state = Player.States.Active;
    }

    void Turn() {
        Debug.Log("Turn for player " + (currentPlayerIndex + 1));
        currentPlayer.state = Player.States.Active;
    }

    void RoundEnd() {
        Debug.Log("Round end");
        FindWinners();
        waiting = true;
        int rank = (int)((1 - (humanPlayer.GetFinalHandRank() / 7462.0)) * 100);
        bool won = CheckIfPlayerWon();
        humanPlayer.classifier.setHandStrength(rank);
        humanPlayer.classifier.won = won;
        foreach(Player player in players) {
            if (player.playerIsAI) {
                humanPlayer.classifier.setMoves();
                player.playerAI.AddToPlayerMoveRecords(new MoveRecord(player.record));
                player.playerAI.naiveBayesTable.AddRecord(humanPlayer.classifier);
            }
            if (player.state == Player.States.Won)
                player.chips.GivePotToPlayer();
        }
    }

    public bool CheckIfAllPlayersMeetState(Player.States s, bool checkEliminated) {
        foreach(Player player in players) {
            if (checkEliminated) {
                if (!(player.state == s || player.state == Player.States.Eliminated))
                    return false;
            } else if (player.state != s)
                return false;
        }
        return true;
    }

    public void NextPlayer() {
        if (CheckIfAllPlayersMeetState(Player.States.TurnCompleted, true)){
            waiting = false;
            NextStage();
        } else {
            currentPlayerIndex += 1;
            if (currentPlayerIndex >= players.Count)
                currentPlayerIndex = 0;
            currentPlayer = players[currentPlayerIndex];
            if (currentPlayer.state == Player.States.Eliminated)
                NextPlayer();
            else
                waiting = false;
        }
    }

    public void NextStage() {
        if (stage == Stages.Preflop) {
            stage = Stages.Flop;
            sharedCards.Flop();
        } else if (stage == Stages.Flop) {
            stage = Stages.River;
            sharedCards.River();
        } else if (stage == Stages.River) {
            stage = Stages.Turn;
            sharedCards.Turn();
        } else if (stage == Stages.Turn) {
            stage = Stages.RoundEnd;
            foreach(Player player in players) {
                player.hand.cardsVisible = true;
            }
        } else if (stage == Stages.RoundEnd) {
            sharedCards.PutInDeck();
            foreach(Player player in players) {
                player.hand.PutInDeck();
                if (player.playerIsAI)
                    player.hand.cardsVisible = false;
            }
            foreach(Player player in players) {
                if (player.bigBlind) {
                    player.bigBlind = false;
                    player.smallBlind = true;
                    if(players.IndexOf(player) == 0) {
                        players.Last().smallBlind = false;
                        players[1].bigBlind = true;
                        break;
                    } else if (players.Last() == player) {
                        players[players.Count-2].smallBlind = false;
                        players[0].bigBlind = true;
                        break;
                    } else {
                        players[players.IndexOf(player)-1].smallBlind = false;
                        players[players.IndexOf(player)+1].bigBlind = true;
                        break;
                    }
                }
            }
            deck.Shuffle();
            stage = Stages.RoundStart;
        }
        SetAllPlayersToState(Player.States.Waiting, false);
    }

    public int PlayersRemaining() {
        int retVal = 0;
        foreach (Player player in players) {
            if (player.state != Player.States.Eliminated)
                retVal++;
        }
        return retVal;
    }

    public void SetAllPlayersToState(Player.States s, bool includeEliminated) {
        foreach(Player player in players) {
            if(includeEliminated)
                player.state = s;
            else if(player.state != Player.States.Eliminated)
                player.state = s;
        }
    }

    public void DestroyLosers() {
        List<Player> tempPlayers = new List<Player>(players);
        foreach (Player player in tempPlayers) {
            if (player.state == Player.States.ToBeDestroyed) {
                player.chips.playerChips.enabled = false;
                Destroy(player.transform.parent.gameObject);
                players.Remove(player);
            }
        }
    }

    public bool CheckIfPlayerWon() {
        foreach(Player player in players)
            if (!player.playerIsAI)
                if (player.state == Player.States.Won)
                    return true;
        return false;
    }

    public void FindWinners() {

        List<StandardPokerHandEvaluator.Hand> bestHands = new List<StandardPokerHandEvaluator.Hand>();
        int winner = 0;

        for(int i = 0; i < players.Count; i++) {
            if (players[i].enabled && players[i].state != Player.States.Eliminated) {
                StandardPokerHandEvaluator.Card cEval1 = BuildCardForEval(players[i].hand.c1);
                StandardPokerHandEvaluator.Card cEval2 = BuildCardForEval(players[i].hand.c2);
                StandardPokerHandEvaluator.Card cEval3 = BuildCardForEval(sharedCards.c1);
                StandardPokerHandEvaluator.Card cEval4 = BuildCardForEval(sharedCards.c2);
                StandardPokerHandEvaluator.Card cEval5 = BuildCardForEval(sharedCards.c3);
                StandardPokerHandEvaluator.Card cEval6 = BuildCardForEval(sharedCards.c4);
                StandardPokerHandEvaluator.Card cEval7 = BuildCardForEval(sharedCards.c5);

                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval5, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval6, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval5, cEval6, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval5, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval5, cEval6, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval5, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval5, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval5, cEval6, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval5, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval5, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval4, cEval5, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval5, cEval6, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval5, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval5, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval4, cEval5, cEval6, cEval7, rankingTable));
                handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval3, cEval4, cEval5, cEval6, cEval7, rankingTable));

                handsEval = handsEval.OrderBy(h => h.Rank).ToList<StandardPokerHandEvaluator.Hand>();

                bestHands.Add(handsEval[0]);
                bestHands = bestHands.OrderBy(h => h.Rank).ToList<StandardPokerHandEvaluator.Hand>();

                players[i].state = Player.States.Lost;
                if (handsEval[0].Rank <= bestHands[0].Rank) {
                    winner = i;
                }
                handsEval.Clear();
            }
        }
        players[winner].state = Player.States.Won;
        Debug.Log("Winner is player " + winner + " with " + bestHands[0].ToString());
    }

    public StandardPokerHandEvaluator.Card BuildCardForEval(Card card) {
        StandardPokerHandEvaluator.CardEnum cEnum = new StandardPokerHandEvaluator.CardEnum();
        StandardPokerHandEvaluator.SuitEnum sEnum = new StandardPokerHandEvaluator.SuitEnum();

        if (card.value == 1)
            cEnum = StandardPokerHandEvaluator.CardEnum.Ace;
        else if (card.value == 2)
            cEnum = StandardPokerHandEvaluator.CardEnum.Two;
        else if (card.value == 3)
            cEnum = StandardPokerHandEvaluator.CardEnum.Three;
        else if (card.value == 4)
            cEnum = StandardPokerHandEvaluator.CardEnum.Four;
        else if (card.value == 5)
            cEnum = StandardPokerHandEvaluator.CardEnum.Five;
        else if (card.value == 6)
            cEnum = StandardPokerHandEvaluator.CardEnum.Six;
        else if (card.value == 7)
            cEnum = StandardPokerHandEvaluator.CardEnum.Seven;
        else if (card.value == 8)
            cEnum = StandardPokerHandEvaluator.CardEnum.Eight;
        else if (card.value == 9)
            cEnum = StandardPokerHandEvaluator.CardEnum.Nine;
        else if (card.value == 10)
            cEnum = StandardPokerHandEvaluator.CardEnum.Ten;
        else if (card.value == 11)
            cEnum = StandardPokerHandEvaluator.CardEnum.Jack;
        else if (card.value == 12)
            cEnum = StandardPokerHandEvaluator.CardEnum.Queen;
        else if (card.value == 13)
            cEnum = StandardPokerHandEvaluator.CardEnum.King;
        
        if (card.suit == Card.Suit.Clubs)
            sEnum = StandardPokerHandEvaluator.SuitEnum.Clubs;
        else if (card.suit == Card.Suit.Spades)
            sEnum = StandardPokerHandEvaluator.SuitEnum.Spades;
        else if (card.suit == Card.Suit.Hearts)
            sEnum = StandardPokerHandEvaluator.SuitEnum.Hearts;
        else if (card.suit == Card.Suit.Diamonds)
            sEnum = StandardPokerHandEvaluator.SuitEnum.Diamonds;

        StandardPokerHandEvaluator.Card retVal = new StandardPokerHandEvaluator.Card(cEnum, sEnum);
        return retVal;
    }
}
