using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : DecisionTreeNode
{
    public string title { get; set; }
    public DecisionTreeNode positive { get; set; }
    public DecisionTreeNode negative { get; set; }
    public System.Func<PlayerDecisionProfile, bool> test { get; set; }

    public override void Evaluate(PlayerDecisionProfile pdp) {
        bool result = this.test(pdp);
        string resultAsString = result ? "yes" : "no";

        if (result)
            this.positive.Evaluate(pdp);
        else
            this.negative.Evaluate(pdp);
    }
}