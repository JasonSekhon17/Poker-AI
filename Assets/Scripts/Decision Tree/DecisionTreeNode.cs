using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionTreeNode
{
    public abstract void Evaluate(PlayerDecisionProfile pdp);
}
