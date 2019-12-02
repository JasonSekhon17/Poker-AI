using System;
using System.Collections.Generic;
using UnityEngine;

namespace StandardPokerHandEvaluator
{
    public class PreflopHandsTable
    {
        public SortedList<int, EvalHand> EvalHands = new SortedList<int, EvalHand>();

        public PreflopHandsTable() {
            LoadTable();
        }

        private void LoadTable() {
            EvalHands.Add(-1681, new EvalHand(-1681, 86, "a/a suited"));
            EvalHands.Add(-1517, new EvalHand(-1517, 68, "k/a suited"));
            EvalHands.Add(-1369, new EvalHand(-1369, 84, "k/k suited"));
            EvalHands.Add(-1271, new EvalHand(-1271, 67, "q/a suited"));
            EvalHands.Add(-1189, new EvalHand(-1189, 66, "j/a suited"));
            EvalHands.Add(-1147, new EvalHand(-1147, 64, "q/k suited"));
            EvalHands.Add(-1073, new EvalHand(-1073, 64, "j/k suited"));
            EvalHands.Add(-961, new EvalHand(-961, 81, "q/q suited"));
            EvalHands.Add(-943, new EvalHand(-943, 66, "10/a suited"));
            EvalHands.Add(-899, new EvalHand(-899, 61, "j/q suited"));
            EvalHands.Add(-851, new EvalHand(-851, 63, "10/k suited"));
            EvalHands.Add(-841, new EvalHand(-841, 79, "j/j suited"));
            EvalHands.Add(-779, new EvalHand(-779, 64, "9/a suited"));
            EvalHands.Add(-713, new EvalHand(-713, 61, "10/q suited"));
            EvalHands.Add(-703, new EvalHand(-703, 61, "9/k suited"));
            EvalHands.Add(-697, new EvalHand(-697, 63, "8/a suited"));
            EvalHands.Add(-667, new EvalHand(-667, 59, "10/j suited"));
            EvalHands.Add(-629, new EvalHand(-629, 60, "8/k suited"));
            EvalHands.Add(-589, new EvalHand(-589, 59, "9/q suited"));
            EvalHands.Add(-551, new EvalHand(-551, 57, "9/j suited"));
            EvalHands.Add(-533, new EvalHand(-533, 63, "7/a suited"));
            EvalHands.Add(-529, new EvalHand(-529, 76, "10/10 suited"));
            EvalHands.Add(-527, new EvalHand(-527, 58, "8/q suited"));
            EvalHands.Add(-493, new EvalHand(-493, 56, "8/j suited"));
            EvalHands.Add(-481, new EvalHand(-481, 59, "7/k suited"));
            EvalHands.Add(-451, new EvalHand(-451, 62, "6/a suited"));
            EvalHands.Add(-437, new EvalHand(-437, 56, "9/10 suited"));
            EvalHands.Add(-407, new EvalHand(-407, 58, "6/k suited"));
            EvalHands.Add(-403, new EvalHand(-403, 56, "7/q suited"));
            EvalHands.Add(-391, new EvalHand(-391, 54, "8/10 suited"));
            EvalHands.Add(-377, new EvalHand(-377, 54, "7/j suited"));
            EvalHands.Add(-361, new EvalHand(-361, 73, "9/9 suited"));
            EvalHands.Add(-341, new EvalHand(-341, 53, "6/q suited"));
            EvalHands.Add(-323, new EvalHand(-323, 53, "8/9 suited"));
            EvalHands.Add(-319, new EvalHand(-319, 53, "6/j suited"));
            EvalHands.Add(-299, new EvalHand(-299, 53, "7/10 suited"));
            EvalHands.Add(-289, new EvalHand(-289, 70, "8/8 suited"));
            EvalHands.Add(-287, new EvalHand(-287, 62, "5/a suited"));
            EvalHands.Add(-259, new EvalHand(-259, 58, "5/k suited"));
            EvalHands.Add(-253, new EvalHand(-253, 51, "6/10 suited"));
            EvalHands.Add(-247, new EvalHand(-247, 51, "7/9 suited"));
            EvalHands.Add(-221, new EvalHand(-221, 50, "7/8 suited"));
            EvalHands.Add(-217, new EvalHand(-217, 55, "5/q suited"));
            EvalHands.Add(-209, new EvalHand(-209, 50, "6/9 suited"));
            EvalHands.Add(-205, new EvalHand(-205, 61, "4/a suited"));
            EvalHands.Add(-203, new EvalHand(-203, 52, "5/j suited"));
            EvalHands.Add(-187, new EvalHand(-187, 49, "6/8 suited"));
            EvalHands.Add(-185, new EvalHand(-185, 57, "4/k suited"));
            EvalHands.Add(-169, new EvalHand(-169, 68, "7/7 suited"));
            EvalHands.Add(-161, new EvalHand(-161, 49, "5/10 suited"));
            EvalHands.Add(-155, new EvalHand(-155, 54, "4/q suited"));
            EvalHands.Add(-145, new EvalHand(-145, 51, "4/j suited"));
            EvalHands.Add(-143, new EvalHand(-143, 48, "6/7 suited"));
            EvalHands.Add(-133, new EvalHand(-133, 48, "5/9 suited"));
            EvalHands.Add(-123, new EvalHand(-123, 60, "3/a suited"));
            EvalHands.Add(-121, new EvalHand(-121, 65, "6/6 suited"));
            EvalHands.Add(-119, new EvalHand(-119, 47, "5/8 suited"));
            EvalHands.Add(-115, new EvalHand(-115, 49, "4/10 suited"));
            EvalHands.Add(-111, new EvalHand(-111, 56, "3/k suited"));
            EvalHands.Add(-95, new EvalHand(-95, 46, "4/9 suited"));
            EvalHands.Add(-93, new EvalHand(-93, 53, "3/q suited"));
            EvalHands.Add(-91, new EvalHand(-91, 46, "5/7 suited"));
            EvalHands.Add(-87, new EvalHand(-87, 50, "3/j suited"));
            EvalHands.Add(-85, new EvalHand(-85, 45, "4/8 suited"));
            EvalHands.Add(-82, new EvalHand(-82, 59, "2/a suited"));
            EvalHands.Add(-77, new EvalHand(-77, 46, "5/6 suited"));
            EvalHands.Add(-74, new EvalHand(-74, 55, "2/k suited"));
            EvalHands.Add(-69, new EvalHand(-69, 48, "3/10 suited"));
            EvalHands.Add(-65, new EvalHand(-65, 45, "4/7 suited"));
            EvalHands.Add(-62, new EvalHand(-62, 52, "2/q suited"));
            EvalHands.Add(-58, new EvalHand(-58, 50, "2/j suited"));
            EvalHands.Add(-57, new EvalHand(-57, 46, "3/9 suited"));
            EvalHands.Add(-55, new EvalHand(-55, 44, "4/6 suited"));
            EvalHands.Add(-51, new EvalHand(-51, 43, "3/8 suited"));
            EvalHands.Add(-49, new EvalHand(-49, 62, "5/5 suited"));
            EvalHands.Add(-46, new EvalHand(-46, 47, "2/10 suited"));
            EvalHands.Add(-39, new EvalHand(-39, 43, "3/7 suited"));
            EvalHands.Add(-38, new EvalHand(-38, 45, "2/9 suited"));
            EvalHands.Add(-35, new EvalHand(-35, 44, "4/5 suited"));
            EvalHands.Add(-34, new EvalHand(-34, 43, "2/8 suited"));
            EvalHands.Add(-33, new EvalHand(-33, 42, "3/6 suited"));
            EvalHands.Add(-26, new EvalHand(-26, 41, "2/7 suited"));
            EvalHands.Add(-25, new EvalHand(-25, 59, "4/4 suited"));
            EvalHands.Add(-22, new EvalHand(-22, 40, "2/6 suited"));
            EvalHands.Add(-21, new EvalHand(-21, 43, "3/5 suited"));
            EvalHands.Add(-15, new EvalHand(-15, 42, "3/4 suited"));
            EvalHands.Add(-14, new EvalHand(-14, 41, "2/5 suited"));
            EvalHands.Add(-10, new EvalHand(-10, 40, "2/4 suited"));
            EvalHands.Add(-9, new EvalHand(-9, 56, "3/3 suited"));
            EvalHands.Add(-6, new EvalHand(-6, 39, "2/3 suited"));
            EvalHands.Add(-4, new EvalHand(-4, 52, "2/2 suited"));
            EvalHands.Add(4, new EvalHand(4, 51, "2/2 off"));
            EvalHands.Add(6, new EvalHand(6, 35, "2/3 off"));
            EvalHands.Add(9, new EvalHand(9, 55, "3/3 off"));
            EvalHands.Add(10, new EvalHand(10, 36, "2/4 off"));
            EvalHands.Add(14, new EvalHand(14, 37, "2/5 off"));
            EvalHands.Add(15, new EvalHand(15, 38, "3/4 off"));
            EvalHands.Add(21, new EvalHand(21, 39, "3/5 off"));
            EvalHands.Add(22, new EvalHand(22, 37, "2/6 off"));
            EvalHands.Add(25, new EvalHand(25, 58, "4/4 off"));
            EvalHands.Add(26, new EvalHand(26, 37, "2/7 off"));
            EvalHands.Add(33, new EvalHand(33, 39, "3/6 off"));
            EvalHands.Add(34, new EvalHand(34, 40, "2/8 off"));
            EvalHands.Add(35, new EvalHand(35, 41, "4/5 off"));
            EvalHands.Add(38, new EvalHand(38, 42, "2/9 off"));
            EvalHands.Add(39, new EvalHand(39, 39, "3/7 off"));
            EvalHands.Add(46, new EvalHand(46, 44, "2/10 off"));
            EvalHands.Add(49, new EvalHand(49, 61, "5/5 off"));
            EvalHands.Add(51, new EvalHand(51, 40, "3/8 off"));
            EvalHands.Add(55, new EvalHand(55, 41, "4/6 off"));
            EvalHands.Add(57, new EvalHand(57, 43, "3/9 off"));
            EvalHands.Add(58, new EvalHand(58, 47, "2/j off"));
            EvalHands.Add(62, new EvalHand(62, 49, "2/q off"));
            EvalHands.Add(65, new EvalHand(65, 41, "4/7 off"));
            EvalHands.Add(69, new EvalHand(69, 45, "3/10 off"));
            EvalHands.Add(74, new EvalHand(74, 53, "2/k off"));
            EvalHands.Add(77, new EvalHand(77, 43, "5/6 off"));
            EvalHands.Add(82, new EvalHand(82, 57, "2/a off"));
            EvalHands.Add(85, new EvalHand(85, 42, "4/8 off"));
            EvalHands.Add(87, new EvalHand(87, 48, "3/j off"));
            EvalHands.Add(91, new EvalHand(91, 43, "5/7 off"));
            EvalHands.Add(93, new EvalHand(93, 50, "3/q off"));
            EvalHands.Add(95, new EvalHand(95, 43, "4/9 off"));
            EvalHands.Add(111, new EvalHand(111, 54, "3/k off"));
            EvalHands.Add(115, new EvalHand(115, 46, "4/10 off"));
            EvalHands.Add(119, new EvalHand(119, 44, "5/8 off"));
            EvalHands.Add(121, new EvalHand(121, 64, "6/6 off"));
            EvalHands.Add(123, new EvalHand(123, 58, "3/a off"));
            EvalHands.Add(133, new EvalHand(133, 45, "5/9 off"));
            EvalHands.Add(143, new EvalHand(143, 45, "6/7 off"));
            EvalHands.Add(145, new EvalHand(145, 48, "4/j off"));
            EvalHands.Add(155, new EvalHand(155, 51, "4/q off"));
            EvalHands.Add(161, new EvalHand(161, 47, "5/10 off"));
            EvalHands.Add(169, new EvalHand(169, 67, "7/7 off"));
            EvalHands.Add(185, new EvalHand(185, 54, "4/k off"));
            EvalHands.Add(187, new EvalHand(187, 46, "6/8 off"));
            EvalHands.Add(203, new EvalHand(203, 49, "5/j off"));
            EvalHands.Add(205, new EvalHand(205, 59, "4/a off"));
            EvalHands.Add(209, new EvalHand(209, 47, "6/9 off"));
            EvalHands.Add(217, new EvalHand(217, 52, "5/q off"));
            EvalHands.Add(221, new EvalHand(221, 47, "7/8 off"));
            EvalHands.Add(247, new EvalHand(247, 48, "7/9 off"));
            EvalHands.Add(253, new EvalHand(253, 48, "6/10 off"));
            EvalHands.Add(259, new EvalHand(259, 55, "5/k off"));
            EvalHands.Add(287, new EvalHand(287, 60, "5/a off"));
            EvalHands.Add(289, new EvalHand(289, 69, "8/8 off"));
            EvalHands.Add(299, new EvalHand(299, 50, "7/10 off"));
            EvalHands.Add(319, new EvalHand(319, 50, "6/j off"));
            EvalHands.Add(323, new EvalHand(323, 50, "8/9 off"));
            EvalHands.Add(341, new EvalHand(341, 53, "6/q off"));
            EvalHands.Add(361, new EvalHand(361, 72, "9/9 off"));
            EvalHands.Add(377, new EvalHand(377, 52, "7/j off"));
            EvalHands.Add(391, new EvalHand(391, 52, "8/10 off"));
            EvalHands.Add(403, new EvalHand(403, 54, "7/q off"));
            EvalHands.Add(407, new EvalHand(407, 56, "6/k off"));
            EvalHands.Add(437, new EvalHand(437, 53, "9/10 off"));
            EvalHands.Add(451, new EvalHand(451, 59, "6/a off"));
            EvalHands.Add(481, new EvalHand(481, 57, "7/k off"));
            EvalHands.Add(493, new EvalHand(493, 53, "8/j off"));
            EvalHands.Add(527, new EvalHand(527, 55, "8/q off"));
            EvalHands.Add(529, new EvalHand(529, 75, "10/10 off"));
            EvalHands.Add(533, new EvalHand(533, 60, "7/a off"));
            EvalHands.Add(551, new EvalHand(551, 55, "9/j off"));
            EvalHands.Add(589, new EvalHand(589, 57, "9/q off"));
            EvalHands.Add(629, new EvalHand(629, 58, "8/k off"));
            EvalHands.Add(667, new EvalHand(667, 57, "10/j off"));
            EvalHands.Add(697, new EvalHand(697, 61, "8/a off"));
            EvalHands.Add(703, new EvalHand(703, 59, "9/k off"));
            EvalHands.Add(713, new EvalHand(713, 59, "10/q off"));
            EvalHands.Add(779, new EvalHand(779, 62, "9/a off"));
            EvalHands.Add(841, new EvalHand(841, 78, "j/j off"));
            EvalHands.Add(851, new EvalHand(851, 61, "10/k off"));
            EvalHands.Add(899, new EvalHand(899, 59, "j/q off"));
            EvalHands.Add(943, new EvalHand(943, 64, "10/a off"));
            EvalHands.Add(961, new EvalHand(961, 80, "q/q off"));
            EvalHands.Add(1073, new EvalHand(1073, 62, "j/k off"));
            EvalHands.Add(1147, new EvalHand(1147, 62, "q/k off"));
            EvalHands.Add(1189, new EvalHand(1189, 65, "j/a off"));
            EvalHands.Add(1271, new EvalHand(1271, 65, "q/a off"));
            EvalHands.Add(1369, new EvalHand(1369, 83, "k/k off"));
            EvalHands.Add(1517, new EvalHand(1517, 66, "k/a off"));
            EvalHands.Add(1681, new EvalHand(1681, 85, "a/a off"));
        }
    }

    public class PreflopHand {
        private Card mC1;
        private Card mC2;
        private EvalHand mEvalHand;
        public PreflopHand(Card c1, Card c2, PreflopHandsTable table) {
            int key;
            this.mC1 = c1;
            this.mC2 = c2;

            key = (int)mC1.CardValue * (int)mC2.CardValue;
            if (mC1.CardSuit == mC2.CardSuit)
                key *= -1;
            Debug.Log("c1: " + (int)mC1.CardValue + ", c2:" + (int)mC2.CardValue);
            Debug.Log("key: " + key);
            mEvalHand = table.EvalHands[key];
        }

        public int Key
        {
            get
            {
                return mEvalHand.Key;
            }
        }

        public int Rank
        {
            get
            {
                return mEvalHand.Rank;
            }
        }
    }
}