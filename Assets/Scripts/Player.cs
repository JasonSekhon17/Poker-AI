using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Player : MonoBehaviour
{
    public enum States{
        Waiting,
        Active,
        TurnCompleted,
        Eliminated,
        Lost,
        Won,
        ToBeDestroyed
    };
    public int startingChips;
    public Game game;
    public States state;
    public Hand hand;
    public Chips chips;
    public bool bigBlind;
    public bool smallBlind;
    public bool playerIsAI;
    public Button callBtn;
    public Button foldBtn;
    public Button betBtn;
    public Slider betSlider;
    public Button nextRoundBtn;
    public TextMeshProUGUI betNumber;
    public PlayerAI playerAI;
    public MoveRecord record;
    public PlayerClassifier classifier;

    void Awake() {
        chips = GetComponent<Chips>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        callBtn = GameObject.Find("Call").GetComponent<Button>();
        foldBtn = GameObject.Find("Fold").GetComponent<Button>();
        betBtn = GameObject.Find("Bet").GetComponent<Button>();
        nextRoundBtn = GameObject.Find("NextRound").GetComponent<Button>();
        betSlider = GameObject.Find("BetSlider").GetComponent<Slider>();
        betSlider.minValue = 1;
        betSlider.maxValue = startingChips;
        betNumber = GameObject.Find("BetNumber").GetComponent<TextMeshProUGUI>();
        playerAI = GetComponent<PlayerAI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        chips.currentChips = startingChips;
        state = States.Waiting;
        if (!playerIsAI) {
            callBtn.onClick.AddListener(CallBtn);
            foldBtn.onClick.AddListener(FoldBtn);
            betBtn.onClick.AddListener(BetBtn);
            nextRoundBtn.onClick.AddListener(NextRound);
            hand.cardsVisible = true;
        } else {

        }
        EnableUI(false);
        EnableRoundOverUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsAI)
            betSlider.maxValue = chips.currentChips;
        
        if (state == States.Active) {
            if (chips.currentChips <= 0)
                CompleteTurn(States.TurnCompleted);
            else {
                EnableUI(!playerIsAI);
                betNumber.text = (int)(betSlider.value) + "";
                if (playerIsAI){
                    if (game.stage == Game.Stages.Preflop) {
                        playerAI.decisionTreePreflop();
                    } else if (game.stage == Game.Stages.Flop) {
                        playerAI.Flop();
                    } else if (game.stage == Game.Stages.River) {
                        playerAI.River();
                    } else if (game.stage == Game.Stages.Turn) {
                        playerAI.Turn();
                    }
                }
            }
        } else if (game.stage == Game.Stages.RoundEnd) {
            EnableUI(false);
            EnableRoundOverUI(true);
        }
    }

    public void DrawCards() {
        Debug.Log("Draw Cards");
        hand.DrawCards();
    }

    public void Call() {
        Debug.Log("Call");
        chips.AddToPot(chips.currentBetAmount);
        chips.AddChipsToPlayer(-chips.currentBetAmount);
        chips.currentBetAmount = 0;
        CompleteTurn(States.TurnCompleted);
    }

    public void Fold() {
        Debug.Log("Fold");
        CompleteTurn(States.Eliminated);
    }

    public void Bet(int amount) {
        Debug.Log("Bet " + amount);
        chips.AddToPot(chips.currentBetAmount + amount);
        chips.AddChipsToPlayer(-(amount + chips.currentBetAmount));
        foreach(Player player in game.players) {
            player.chips.currentBetAmount += amount;
        }
        chips.currentBetAmount = 0;
        game.SetAllPlayersToState(States.Waiting, false);
        CompleteTurn(States.TurnCompleted);
    }

    public void Bet() {
        Debug.Log("Bet");
        Bet(int.Parse(betNumber.text));
    }

    public void BetFlop(int amount) {
        chips.AddToPot(amount);
        chips.AddChipsToPlayer(-amount);
        foreach(Player player in game.players) {
            if (!(player.smallBlind || player.bigBlind))
                player.chips.currentBetAmount += amount;
            else if (player.smallBlind) {
                player.chips.AddToPot(amount/2);
                player.chips.AddChipsToPlayer(-(amount/2));
                player.chips.currentBetAmount += amount/2;
            }
        }
        chips.currentBetAmount = 0;
    }

    public void NextRound() {
        Debug.Log("Next Round");
        foreach (Player player in game.players) {
            if (player.state == Player.States.Lost && player.chips.currentChips <= 0)
                player.state = States.ToBeDestroyed;
            Debug.Log(playerAI.GetPlayerMoveRecords());
        }
        game.DestroyLosers();
        game.SetAllPlayersToState(States.TurnCompleted, true);
        EnableRoundOverUI(false);
        game.NextPlayer();
        game.waiting = false;
    }

    public void CompleteTurn(States s) {
        Debug.Log("Complete Turn");
        state = s;
        game.NextPlayer();
    }

    public void EnableUI(bool enable) {
        callBtn.gameObject.SetActive(enable);
        foldBtn.gameObject.SetActive(enable);
        betBtn.gameObject.SetActive(enable);
        betSlider.gameObject.SetActive(enable);
        betNumber.gameObject.SetActive(enable);
    }

    public void EnableRoundOverUI(bool enable) {
        nextRoundBtn.gameObject.SetActive(enable);
    }

    private void CallBtn() {
        foreach(Player player in game.players)
            if (player.playerIsAI)
                player.record.movesMade += "C";
        if (game.stage == Game.Stages.Preflop)
                classifier.preflop += "C";
            else if(game.stage == Game.Stages.Flop)
                classifier.flop += "C";
            else if(game.stage == Game.Stages.River)
                classifier.river += "C";
            else if(game.stage == Game.Stages.Turn)
                classifier.turn += "C";
        Call();
    }

    private void FoldBtn() {
        foreach(Player player in game.players)
            if (player.playerIsAI)
                player.record.movesMade += "F";
        if (game.stage == Game.Stages.Preflop)
                classifier.preflop += "F";
            else if(game.stage == Game.Stages.Flop)
                classifier.flop += "F";
            else if(game.stage == Game.Stages.River)
                classifier.river += "F";
            else if(game.stage == Game.Stages.Turn)
                classifier.turn += "F";
        Fold();
    }

    private void BetBtn() {
        foreach(Player player in game.players)
            if (player.playerIsAI)
                player.record.movesMade += "B";
        if (game.stage == Game.Stages.Preflop)
                classifier.preflop += "B";
            else if(game.stage == Game.Stages.Flop)
                classifier.flop += "B";
            else if(game.stage == Game.Stages.River)
                classifier.river += "B";
            else if(game.stage == Game.Stages.Turn)
                classifier.turn += "B";
        Bet();
    }

    public int GetFinalHandRank() {
        List<StandardPokerHandEvaluator.Hand> handsEval = new List<StandardPokerHandEvaluator.Hand>();
        StandardPokerHandEvaluator.Card cEval1 = game.BuildCardForEval(hand.c1);
        StandardPokerHandEvaluator.Card cEval2 = game.BuildCardForEval(hand.c2);
        StandardPokerHandEvaluator.Card cEval3 = game.BuildCardForEval(game.sharedCards.c1);
        StandardPokerHandEvaluator.Card cEval4 = game.BuildCardForEval(game.sharedCards.c2);
        StandardPokerHandEvaluator.Card cEval5 = game.BuildCardForEval(game.sharedCards.c3);
        StandardPokerHandEvaluator.Card cEval6 = game.BuildCardForEval(game.sharedCards.c4);
        StandardPokerHandEvaluator.Card cEval7 = game.BuildCardForEval(game.sharedCards.c5);

        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval5, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval5, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval5, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval5, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval5, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval5, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval4, cEval5, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval5, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval5, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval4, cEval5, cEval6, cEval7, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval3, cEval4, cEval5, cEval6, cEval7, game.rankingTable));

        handsEval = handsEval.OrderBy(h => h.Rank).ToList<StandardPokerHandEvaluator.Hand>();

        return handsEval[0].Rank;
    }
}
