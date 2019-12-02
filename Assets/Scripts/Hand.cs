using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Deck deck;
    public bool cardsVisible;
    public GameObject card1;
    public GameObject card2;
    public Card c1;
    public Card c2;

    void Awake() {
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (card1.transform.childCount > 0) {
            if (cardsVisible) {
                card1.GetComponentInChildren<Card>().flipped = false;
                card2.GetComponentInChildren<Card>().flipped = false;
            } else {
                card2.GetComponentInChildren<Card>().flipped = true;
                card1.GetComponentInChildren<Card>().flipped = true;
            }
        }
    }

    public void DrawCards() {
        c1 = deck.PullFromDeck();
        c2 = deck.PullFromDeck();
        c1.transform.parent = card1.transform;
        c1.transform.localPosition = new Vector3(0,0,0);
        c1.transform.localRotation = Quaternion.Euler(0,0,0);
        c2.transform.parent = card2.transform;
        c2.transform.localPosition = new Vector3(0,0,0);
        c2.transform.localRotation = Quaternion.Euler(0,0,0);
    }

    public void PutInDeck() {
        deck.PutInDeck(c1);
        deck.PutInDeck(c2);
    }
}
