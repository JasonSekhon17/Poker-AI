using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassifier
{
    public bool won;
    public string handStrength;
    public string preflop;
    public string flop;
    public string river;
    public string turn;
    public string moves;

    public PlayerClassifier() {
        won = false;
        handStrength = "";
        preflop = "";
        flop = "";
        turn = "";
        river = "";
    }

    public string calculateHandStrength(int str) {
        if (str >= 0 && str < 10) {
            return "0,10";
        } else if (str >= 10 && str < 20) {
            return "10,20";
        } else if (str >= 20 && str < 30) {
            return "20,30";
        } else if (str >= 30 && str < 40) {
            return "30,40";
        } else if (str >= 40 && str < 50) {
            return "40,50";
        } else if (str >= 50 && str < 60) {
            return "50,60";
        } else if (str >= 60 && str < 70) {
            return "60,70";
        } else if (str >= 70 && str < 80) {
            return "70,80";
        } else if (str >= 80 && str < 90) {
            return "80,90";
        } else if (str >= 90 && str < 100) {
            return "90,100";
        }
        return "";
    }

    public void setHandStrength(int str) {
        handStrength = calculateHandStrength(str);
    }

    public void setMoves() {
        moves = preflop + flop + river + turn;
    }
}
