using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Data;

public class PlayerAI : MonoBehaviour
{
    struct BayesMove {
        public string moves;
        public bool won;
        public double moves010;
        public double moves1020;
        public double moves2030;
        public double moves3040;
        public double moves4050;
        public double moves5060;
        public double moves6070;
        public double moves7080;
        public double moves8090;
        public double moves90100;
        public double won010;
        public double won1020;
        public double won2030;
        public double won3040;
        public double won4050;
        public double won5060;
        public double won6070;
        public double won7080;
        public double won8090;
        public double won90100;
    }
    public StandardPokerHandEvaluator.PreflopHandsTable rankingTable;
    public StandardPokerHandEvaluator.Deck deck;
    public NaiveBayesTable naiveBayesTable;
    public Game game;
    public Player player;
    public int handRank;
    int maxBet;
    int minBet;
    int betAmount;

    public List<MoveRecord> playerMoveRecords = new List<MoveRecord>();

    public Selector m_rootNode;
    public Sequence m_node2A;
    public Sequence m_node2B;
    public ActionNode m_node3AA;
    public Selector m_node3AB;
    public ActionNode m_node3BA;
    public Selector m_node3BB;
    public Sequence m_node4ABA;
    public Sequence m_node4ABB;
    public Sequence m_node4ABC;
    public ActionNode m_node4ABD;
    public Sequence m_node4BBA;
    public Sequence m_node4BBB;
    public ActionNode m_node4BBC;
    public ActionNode m_node5ABAA;
    public Selector m_node5ABAB;
    public ActionNode m_node5ABBA;
    public ActionNode m_node5ABBB;
    public ActionNode m_node5ABCA;
    public ActionNode m_node5ABCB;
    public ActionNode m_node5BBAA;
    public ActionNode m_node5BBAB;
    public ActionNode m_node5BBBA;
    public ActionNode m_node5BBBB;
    public ActionNode m_node5BBBC;
    public Sequence m_node6ABABA;
    public ActionNode m_node6ABABB;
    public ActionNode m_node7ABABAA;
    public ActionNode m_node7ABABAB;
    List<BayesMove> bayesMoves;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        game = player.game;
        rankingTable = new StandardPokerHandEvaluator.PreflopHandsTable();
        naiveBayesTable = new NaiveBayesTable();
        deck = new StandardPokerHandEvaluator.Deck(false);
        bayesMoves = new List<BayesMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decisionTreePreflop() {
        int str = GetPreflopStrength(player.hand.c1, player.hand.c2);

        game.logUI.text += "Preflop Rank " + str + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        PlayerDecisionProfile pdp = new PlayerDecisionProfile{
            strength = str,
            bigblind = player.bigBlind,
            smallblind = player.smallBlind,
            player = this.player
        };

        var trunk = MainDecisionTree();
        trunk.Evaluate(pdp);
    }

    public void Flop() {
        handRank = (int)((1 - (GetFlopHandRank() / 7462.0)) * 100);

        game.logUI.text += "Flop Rank " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);

        string move = PredictNextMove(player.record.movesMade);

        game.logUI.text += "Predicted Move: " + move + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        if (move.Equals("C"))
            handRank += (int)(handRank * .1);
        if (move.Equals("F"))
            handRank += (int)(handRank * .2);
        if (move.Equals("B"))
            handRank -= (int)(handRank * .05);
        
        game.logUI.text += "Adjusted Rank: " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        BuildBehaviourTree(handRank);
    }

    public void River() {
        handRank = (int)((1 - (GetRiverHandRank() / 7462.0)) * 100);

        game.logUI.text += "River Rank " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);

        string move = PredictNextMove(player.record.movesMade);

        game.logUI.text += "Predicted Move: " + move + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);

        if (move.Equals("C"))
            handRank += (int)(handRank * .1);
        if (move.Equals("F"))
            handRank += (int)(handRank * .2);
        if (move.Equals("B"))
            handRank -= (int)(handRank * .05);
        
        game.logUI.text += "Adjusted Rank: " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        BuildBehaviourTree(handRank);
    }

    public void Turn() {
        handRank = (int)((1 - (GetTurnHandRank() / 7462.0)) * 100);

        game.logUI.text += "Turn Rank " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);

        string move = PredictNextMove(player.record.movesMade);

        game.logUI.text += "Predicted Move: " + move + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        if (move.Equals("C"))
            handRank += (int)(handRank * .1);
        if (move.Equals("F"))
            handRank += (int)(handRank * .2);
        if (move.Equals("B"))
            handRank -= (int)(handRank * .05);
        
        game.logUI.text += "Adjusted Rank: " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        
        string movesMade = PredictHandStrength(player.record.movesMade);

        game.logUI.text += "Predicted Hand Strength: " + movesMade + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);

        string[] range = movesMade.Split(',');
        int max = 0;
        int min = 0;
        if (range.Count() > 1) {
            min = int.Parse(range[0]);
            max = int.Parse(range[1]);
        }
        if (handRank > max)
            handRank += (int)(handRank * .1);
        else if (handRank < min)
            handRank -= (int)(handRank * .1);
        
        game.logUI.text += "Adjusted Rank: " + handRank + "\n";
        game.logLines++;
        if (game.logLines > game.maxLines)
            game.logUI.rectTransform.sizeDelta = new Vector2(game.logUI.rectTransform.sizeDelta.x, game.logUI.rectTransform.sizeDelta.y + 23);
        

        BuildBehaviourTree(handRank);
    }

    int GetPreflopStrength(Card c1, Card c2) {
        StandardPokerHandEvaluator.Card cEval1 = game.BuildCardForEval(c1);
        StandardPokerHandEvaluator.Card cEval2 = game.BuildCardForEval(c2);
        StandardPokerHandEvaluator.PreflopHand hand = new StandardPokerHandEvaluator.PreflopHand(cEval1, cEval2, rankingTable);
        
        return hand.Rank;
    }

    int GetFlopHandRank() {
        StandardPokerHandEvaluator.Card cEval1 = game.BuildCardForEval(player.hand.c1);
        StandardPokerHandEvaluator.Card cEval2 = game.BuildCardForEval(player.hand.c2);
        StandardPokerHandEvaluator.Card cEval3 = game.BuildCardForEval(game.sharedCards.c1);
        StandardPokerHandEvaluator.Card cEval4 = game.BuildCardForEval(game.sharedCards.c2);
        StandardPokerHandEvaluator.Card cEval5 = game.BuildCardForEval(game.sharedCards.c3);
        StandardPokerHandEvaluator.Hand myHand = new StandardPokerHandEvaluator.Hand(cEval1, cEval2,
                cEval3, cEval4, cEval5, game.rankingTable);
        return myHand.Rank;
    }

    int GetRiverHandRank() {
        List<StandardPokerHandEvaluator.Hand> handsEval = new List<StandardPokerHandEvaluator.Hand>();
        StandardPokerHandEvaluator.Card cEval1 = game.BuildCardForEval(player.hand.c1);
        StandardPokerHandEvaluator.Card cEval2 = game.BuildCardForEval(player.hand.c2);
        StandardPokerHandEvaluator.Card cEval3 = game.BuildCardForEval(game.sharedCards.c1);
        StandardPokerHandEvaluator.Card cEval4 = game.BuildCardForEval(game.sharedCards.c2);
        StandardPokerHandEvaluator.Card cEval5 = game.BuildCardForEval(game.sharedCards.c3);
        StandardPokerHandEvaluator.Card cEval6 = game.BuildCardForEval(game.sharedCards.c4);

        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval5, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval4, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval3, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval2, cEval4, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval1, cEval3, cEval4, cEval5, cEval6, game.rankingTable));
        handsEval.Add(new StandardPokerHandEvaluator.Hand(cEval2, cEval3, cEval4, cEval5, cEval6, game.rankingTable));

        handsEval = handsEval.OrderBy(h => h.Rank).ToList<StandardPokerHandEvaluator.Hand>();

        return handsEval[0].Rank;
    }

    public int GetTurnHandRank() {
        List<StandardPokerHandEvaluator.Hand> handsEval = new List<StandardPokerHandEvaluator.Hand>();
        StandardPokerHandEvaluator.Card cEval1 = game.BuildCardForEval(player.hand.c1);
        StandardPokerHandEvaluator.Card cEval2 = game.BuildCardForEval(player.hand.c2);
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

    void BuildBehaviourTree(int handRank) {
        betAmount = (int)(player.chips.currentChips * (handRank / 100.0));
        maxBet = player.chips.currentChips / 8;
        minBet = player.chips.currentChips / 4;

        List<Node> rootChildren = new List<Node>();

        m_node3AA = new ActionNode(CheckIfHandStrengthIsGreater50);
        m_node4ABD = new ActionNode(Call);
        m_node5ABAA = new ActionNode(CheckifHighBetWasMade);
        m_node5ABBA = new ActionNode(CheckifLowBetWasMade);
        m_node5ABBB = new ActionNode(Call);
        m_node5ABCA = new ActionNode(CheckIfHandStrengthIsGreater70);
        m_node5ABCB = new ActionNode(Bet);
        m_node6ABABB = new ActionNode(Fold);
        m_node7ABABAA = new ActionNode(CheckIfHandStrengthIsGreater70);
        m_node7ABABAB = new ActionNode(Call);

        m_node3BA = new ActionNode(CheckIfHandStrengthIsLesser50);
        m_node4BBC = new ActionNode(Call);
        m_node5BBAA = new ActionNode(CheckifHighBetWasMade);
        m_node5BBAB = new ActionNode(Fold);
        m_node5BBBA = new ActionNode(CheckifLowBetWasMade);
        m_node5BBBB = new ActionNode(CheckIfHandStrengthIsLess40);
        m_node5BBBC = new ActionNode(Fold);

        //Build Node 2A
        rootChildren.Add(m_node7ABABAA);
        rootChildren.Add(m_node7ABABAB);
        m_node6ABABA = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node6ABABA);
        rootChildren.Add(m_node6ABABB);
        m_node5ABAB = new Selector(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node5ABAA);
        rootChildren.Add(m_node5ABAB);
        m_node4ABA = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node5ABBA);
        rootChildren.Add(m_node5ABBB);
        m_node4ABB = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node5ABCA);
        rootChildren.Add(m_node5ABCB);
        m_node4ABC = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node4ABA);
        rootChildren.Add(m_node4ABB);
        rootChildren.Add(m_node4ABC);
        rootChildren.Add(m_node4ABD);
        m_node3AB = new Selector(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node3AA);
        rootChildren.Add(m_node3AB);
        m_node2A = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        //Build Node 2B
        rootChildren.Add(m_node5BBBA);
        rootChildren.Add(m_node5BBBB);
        rootChildren.Add(m_node5BBBC);
        m_node4BBB = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node5BBAA);
        rootChildren.Add(m_node5BBAB);
        m_node4BBA = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node4BBA);
        rootChildren.Add(m_node4BBB);
        rootChildren.Add(m_node4BBC);
        m_node3BB = new Selector(new List<Node>(rootChildren));
        rootChildren.Clear();

        rootChildren.Add(m_node3BA);
        rootChildren.Add(m_node3BB);
        m_node2B = new Sequence(new List<Node>(rootChildren));
        rootChildren.Clear();

        //Build Root Node
        rootChildren.Add(m_node2A);
        rootChildren.Add(m_node2B);
        m_rootNode = new Selector(new List<Node>(rootChildren));
        rootChildren.Clear();

        m_rootNode.Evaluate();
    }

    NodeStates CheckIfHandStrengthIsLess40() {
        if (handRank < 40)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }
    NodeStates CheckIfHandStrengthIsGreater50() {
        if (handRank >= 50)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    NodeStates CheckIfHandStrengthIsGreater70() {
        if (handRank >= 70)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    NodeStates CheckIfHandStrengthIsLesser50() {
        if (handRank < 50)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    NodeStates CheckifLowBetWasMade() {
        if (player.chips.currentBetAmount <= maxBet && player.chips.currentBetAmount != 0)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    NodeStates CheckifHighBetWasMade() {
        if (player.chips.currentBetAmount >= minBet)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    NodeStates Call() {
        player.Call();
        return NodeStates.SUCCESS;
    }

    NodeStates Fold() {
        player.Fold();
        return NodeStates.SUCCESS;
    }

    NodeStates Bet() {
        player.Bet(betAmount);
        return NodeStates.SUCCESS;
    }

    public void AddToPlayerMoveRecords(MoveRecord record) {
        playerMoveRecords.Add(new MoveRecord(record));
    }

    public List<MoveRecord> GetPlayerMoveRecords() {
        return playerMoveRecords;
    }

    public string PredictNextMove(string pattern) {
        int cCount = 0;
        int fCount = 0;
        int bCount = 0;
        string seq;
        
        for (int i = 0; i < playerMoveRecords.Count; i++){
            seq = playerMoveRecords[i].movesMade;
            
            List<int> indexes = new List<int>();
            for (int index = 0;; index += pattern.Length) {
                index = seq.IndexOf(pattern, index);
                if (index != -1)
                    indexes.Add(index + pattern.Length);
                else
                    break;
            }

            foreach(int x in indexes) {
                if (x < seq.Length) {
                    switch (seq[x]) {
                        case 'C':
                            cCount++;
                            break;
                        case 'F':
                            fCount++;
                            break;
                        case 'B':
                            bCount++;
                            break;
                    }
                }
            }
        }
        if (cCount > fCount && cCount > bCount)
            return "C";
        else if (fCount > bCount && fCount > cCount)
            return "F";
        else if (bCount > fCount && bCount > cCount)
            return "B";
        else
            return "";
    }

    public string PredictHandStrength(string pattern) {
        List<DataRow> rows = (naiveBayesTable.dataTable.Rows.OfType<DataRow>()).ToList();
        Dictionary<string, int> strengths = new Dictionary<string, int>();
        strengths.Add("0,10", 0);
        strengths.Add("10,20", 0);
        strengths.Add("20,30", 0);
        strengths.Add("30,40", 0);
        strengths.Add("40,50", 0);
        strengths.Add("50,60", 0);
        strengths.Add("60,70", 0);
        strengths.Add("70,80", 0);
        strengths.Add("80,90", 0);
        strengths.Add("90,100", 0);

        for (int i = 0; i < rows.Count(); i++) {
            int currentCount;
            string strength = rows[i]["Strength"].ToString();
            strengths.TryGetValue(strength, out currentCount);
            strengths[strength] = currentCount + 1;
        }

        BayesMove bayesMove;
        bayesMove.moves = pattern;
        bayesMove.won = game.CheckIfPlayerWon();
        bayesMove.moves010 = 0;
        bayesMove.moves1020 = 0;
        bayesMove.moves2030 = 0;
        bayesMove.moves3040 = 0;
        bayesMove.moves4050 = 0;
        bayesMove.moves5060 = 0;
        bayesMove.moves6070 = 0;
        bayesMove.moves7080 = 0;
        bayesMove.moves8090 = 0;
        bayesMove.moves90100 = 0;
        bayesMove.won010 = 0;
        bayesMove.won1020 = 0;
        bayesMove.won2030 = 0;
        bayesMove.won3040 = 0;
        bayesMove.won4050 = 0;
        bayesMove.won5060 = 0;
        bayesMove.won6070 = 0;
        bayesMove.won7080 = 0;
        bayesMove.won8090 = 0;
        bayesMove.won90100 = 0;

        double num010   = 0;
        double num1020  = 0;
        double num2030  = 0;
        double num3040  = 0;
        double num4050  = 0;
        double num5060  = 0;
        double num6070  = 0;
        double num7080  = 0;
        double num8090  = 0;
        double num90100 = 0;

        foreach (DataRow row in rows) {
            if (row["Moves"].ToString() == pattern)
                if (row["Strength"].ToString() == "0,10")
                    num010++;
                if (row["Strength"].ToString() == "10,20")
                    num1020++;
                if (row["Strength"].ToString() == "20,30")
                    num2030++;
                if (row["Strength"].ToString() == "30,40")
                    num3040++;
                if (row["Strength"].ToString() == "40,50")
                    num4050++;
                if (row["Strength"].ToString() == "50,60")
                    num5060++;
                if (row["Strength"].ToString() == "60,70")
                    num6070++;
                if (row["Strength"].ToString() == "70,80")
                    num7080++;
                if (row["Strength"].ToString() == "80,90")
                    num8090++;
                if (row["Strength"].ToString() == "90,100")
                    num90100++;       
        }

        if (strengths["0,10"] > 0)
            bayesMove.moves010   = num010 / strengths["0,10"];
        if (strengths["10,20"] > 0)
            bayesMove.moves1020  = num1020 / strengths["10,20"];
        if (strengths["20,30"] > 0)
            bayesMove.moves2030  = num2030 / strengths["20,30"];
        if (strengths["30,40"] > 0)
            bayesMove.moves3040  = num3040 / strengths["30,40"];
        if (strengths["40,50"] > 0)
            bayesMove.moves4050  = num4050 / strengths["40,50"];
        if (strengths["50,60"] > 0)
            bayesMove.moves5060  = num5060 / strengths["50,60"];
        if (strengths["60,70"] > 0)
            bayesMove.moves6070  = num6070 / strengths["60,70"];
        if (strengths["70,80"] > 0)
            bayesMove.moves7080  = num7080 / strengths["70,80"];
        if (strengths["80,90"] > 0)
            bayesMove.moves8090  = num8090 / strengths["80,90"];
        if (strengths["90,100"] > 0)
            bayesMove.moves90100 = num90100 / strengths["90,100"];

        num010   = 0;
        num1020  = 0;
        num2030  = 0;
        num3040  = 0;
        num4050  = 0;
        num5060  = 0;
        num6070  = 0;
        num7080  = 0;
        num8090  = 0;
        num90100 = 0;

        foreach (DataRow row in rows) {
            if (((bool)row["Won"]) == game.CheckIfPlayerWon())
                if (row["Strength"].ToString() == "0,10")
                    num010++;
                if (row["Strength"].ToString() == "10,20")
                    num1020++;
                if (row["Strength"].ToString() == "20,30")
                    num2030++;
                if (row["Strength"].ToString() == "30,40")
                    num3040++;
                if (row["Strength"].ToString() == "40,50")
                    num4050++;
                if (row["Strength"].ToString() == "50,60")
                    num5060++;
                if (row["Strength"].ToString() == "60,70")
                    num6070++;
                if (row["Strength"].ToString() == "70,80")
                    num7080++;
                if (row["Strength"].ToString() == "80,90")
                    num8090++;
                if (row["Strength"].ToString() == "90,100")
                    num90100++;   
        }

        if (strengths["0,10"] > 0)
            bayesMove.won010   = num010 / strengths["0,10"];
        if (strengths["10,20"] > 0)
            bayesMove.won1020  = num1020 / strengths["10,20"];
        if (strengths["20,30"] > 0)
            bayesMove.won2030  = num2030 / strengths["20,30"];
        if (strengths["30,40"] > 0)
            bayesMove.won3040  = num3040 / strengths["30,40"];
        if (strengths["40,50"] > 0)
            bayesMove.won4050  = num4050 / strengths["40,50"];
        if (strengths["50,60"] > 0)
            bayesMove.won5060  = num5060 / strengths["50,60"];
        if (strengths["60,70"] > 0)
            bayesMove.won6070  = num6070 / strengths["60,70"];
        if (strengths["70,80"] > 0)
            bayesMove.won7080  = num7080 / strengths["70,80"];
        if (strengths["80,90"] > 0)
            bayesMove.won8090  = num8090 / strengths["80,90"];
        if (strengths["90,100"] > 0)
            bayesMove.won90100 = num90100 / strengths["90,100"];

        double pStrTotal = strengths["0,10"] + strengths["10,20"] + strengths["20,30"] + 
                        strengths["30,40"] + strengths["40,50"] + strengths["50,60"] + 
                        strengths["60,70"] + strengths["70,80"] + strengths["80,90"] + 
                        strengths["90,100"];
        
        double p010   = (strengths["0,10"] / pStrTotal) * bayesMove.moves010   * bayesMove.won010;
        double p1020  = (strengths["10,20"] / pStrTotal) * bayesMove.moves1020  * bayesMove.won1020;
        double p2030  = (strengths["20,30"] / pStrTotal) * bayesMove.moves2030  * bayesMove.won2030;
        double p3040  = (strengths["30,40"] / pStrTotal) * bayesMove.moves3040  * bayesMove.won3040;
        double p4050  = (strengths["40,50"] / pStrTotal) * bayesMove.moves4050  * bayesMove.won4050;
        double p5060  = (strengths["50,60"] / pStrTotal) * bayesMove.moves5060  * bayesMove.won5060;
        double p6070  = (strengths["60,70"] / pStrTotal) * bayesMove.moves6070  * bayesMove.won6070;
        double p7080  = (strengths["70,80"] / pStrTotal) * bayesMove.moves7080  * bayesMove.won7080;
        double p8090  = (strengths["80,90"] / pStrTotal) * bayesMove.moves8090  * bayesMove.won8090;
        double p90100 = (strengths["90,100"] / pStrTotal) * bayesMove.moves90100 * bayesMove.won90100;

        if (p010 > p1020 &&
            p010 > p2030 &&
            p010 > p3040 &&
            p010 > p4050 &&
            p010 > p5060 &&
            p010 > p6070 &&
            p010 > p7080 &&
            p010 > p8090 &&
            p010 > p90100)
        {
            return "0,10";
        } else if (p1020 > p2030 &&
            p1020 > p3040 &&
            p1020 > p4050 &&
            p1020 > p5060 &&
            p1020 > p6070 &&
            p1020 > p7080 &&
            p1020 > p8090 &&
            p1020 > p90100)
        {
            return "10,20";
        } else if (p2030 > p3040 &&
            p2030 > p4050 &&
            p2030 > p5060 &&
            p2030 > p6070 &&
            p2030 > p7080 &&
            p2030 > p8090 &&
            p2030 > p90100)
        {
            return "20,30";
        } else if (p3040 > p4050 &&
            p3040 > p5060 &&
            p3040 > p6070 &&
            p3040 > p7080 &&
            p3040 > p8090 &&
            p3040 > p90100)
        {
            return "30,40";
        } else if (p4050 > p5060 &&
            p4050 > p6070 &&
            p4050 > p7080 &&
            p4050 > p8090 &&
            p4050 > p90100)
        {
            return "40,50";
        } else if (p5060 > p6070 &&
            p5060 > p7080 &&
            p5060 > p8090 &&
            p5060 > p90100)
        {
            return "50,60";
        } else if (p6070 > p7080 &&
            p6070 > p8090 &&
            p6070 > p90100)
        {
            return "60,70";
        } else if (p7080 > p8090 &&
            p7080 > p90100)
        {
            return "70,80";
        } else if (p8090 > p90100)
        {
            return "80,90";
        } else if (p90100 > 0)
        {
            return "90,100";
        } else
            return "";
    }

    public static Decision MainDecisionTree() {

        Decision call = new Decision {
            title = "call",
            test = (pdp) => {
                pdp.player.Call();
                return true;
            },
            positive = new Action { result = true },
            negative = new Action { result = false }
        };

        Decision fold = new Decision {
            title = "fold",
            test = (pdp) => {
                pdp.player.Fold();
                return true;
            },
            positive = new Action { result = true },
            negative = new Action { result = false }
        };

        Decision bet = new Decision {
            title = "bet",
            test = (pdp) => {
                pdp.player.Bet((int)(pdp.player.chips.currentChips * .05));
                return true;
            },
            positive = new Action { result = true },
            negative = new Action { result = false }
        };

        Decision greaterThan40 = new Decision {
            title = "Greater than 40",
            test = (pdp) => pdp.strength >= 40,
            positive = call,
            negative = fold
        };

        Decision greaterThan50 = new Decision {
            title = "Greater than 50",
            test = (pdp) => pdp.strength >= 50,
            positive = call,
            negative = fold
        };

        Decision greaterThan70 = new Decision {
            title = "Greater than 70",
            test = (pdp) => pdp.strength >= 70,
            positive = bet,
            negative = call
        };

        Decision smallBlind = new Decision {
            title = "Is small blind",
            test = (pdp) => pdp.smallblind,
            positive = greaterThan40,
            negative = greaterThan50
        };

        Decision bigBlind = new Decision {
            title = "Is big blind",
            test = (pdp) => pdp.bigblind,
            positive = greaterThan70,
            negative = smallBlind
        };

        return bigBlind;
    }
}