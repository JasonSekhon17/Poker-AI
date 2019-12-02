using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCards : MonoBehaviour
{
    public Deck deck;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public Card c1;
    public Card c2;
    public Card c3;
    public Card c4;
    public Card c5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Preflop() {

    }

    public void Flop() {
        c1 = deck.PullFromDeck();
        c2 = deck.PullFromDeck();
        c3 = deck.PullFromDeck();

        c1.transform.parent = card1.transform;
        c1.transform.localPosition = new Vector3(0,0,0);
        c1.transform.localRotation = Quaternion.Euler(0,0,0);
        c1.flipped = false;

        c2.transform.parent = card2.transform;
        c2.transform.localPosition = new Vector3(0,0,0);
        c2.transform.localRotation = Quaternion.Euler(0,0,0);
        c2.flipped = false;

        c3.transform.parent = card3.transform;
        c3.transform.localPosition = new Vector3(0,0,0);
        c3.transform.localRotation = Quaternion.Euler(0,0,0);
        c3.flipped = false;
    }

    public void River() {
        c4 = deck.PullFromDeck();

        c4.transform.parent = card4.transform;
        c4.transform.localPosition = new Vector3(0,0,0);
        c4.transform.localRotation = Quaternion.Euler(0,0,0);
        c4.flipped = false;
    }

    public void Turn() {
        c5 = deck.PullFromDeck();

        c5.transform.parent = card5.transform;
        c5.transform.localPosition = new Vector3(0,0,0);
        c5.transform.localRotation = Quaternion.Euler(0,0,0);
        c5.flipped = false;
    }

    public void PutInDeck() {
        deck.PutInDeck(c1);
        deck.PutInDeck(c2);
        deck.PutInDeck(c3);
        deck.PutInDeck(c4);
        deck.PutInDeck(c5);
    }
}
