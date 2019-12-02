using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chips : MonoBehaviour
{
    public int currentChips;
    public int currentBetAmount;
    public int maxWinnings;
    public TextMeshProUGUI currentPot;
    public TextMeshProUGUI playerChips;
    // Start is called before the first frame update
    void Start()
    {
        currentPot = GameObject.Find("CurrentPot").GetComponent<TextMeshProUGUI>();
        playerChips = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        playerChips.enabled = true;
        currentPot.text = 0 + "";
    }

    // Update is called once per frame
    void Update()
    {
        playerChips.text = currentChips + "";
    }

    public void AddChipsToPlayer(int amount) {
        currentChips += amount;
    }

    public void AddToPot(int amount) {
        currentPot.text = int.Parse(currentPot.text) + amount + "";
    }

    public void ClearPot() {
        currentPot.text = 0 + "";
    }

    public void GivePotToPlayer() {
        currentChips += int.Parse(currentPot.text);
        ClearPot();
    }
}
