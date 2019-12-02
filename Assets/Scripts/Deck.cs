using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Card card;
    public List<Card> cards;
    
    void Awake()
    {
        cards = new List<Card>();
        for (int i = 0; i < 13; i++) {
            Card c = Instantiate(card, new Vector3(transform.position.x, transform.position.y, i * .01f), transform.rotation, transform);
            c.suit = Card.Suit.Clubs;
            c.value = i + 1;
            c.flipped = true;
            cards.Add(c);
        }
        for (int i = 0; i < 13; i++) {
            Card c = Instantiate(card, new Vector3(transform.position.x, transform.position.y, (13 + i) * .01f), transform.rotation, transform);
            c.suit = Card.Suit.Diamonds;
            c.value = i + 1;
            c.flipped = true;
            cards.Add(c);
        }
        for (int i = 0; i < 13; i++) {
            Card c = Instantiate(card, new Vector3(transform.position.x, transform.position.y, (26 + i) * .01f), transform.rotation, transform);
            c.suit = Card.Suit.Hearts;
            c.value = i + 1;
            c.flipped = true;
            cards.Add(c);
        }
        for (int i = 0; i < 13; i++) {
            Card c = Instantiate(card, new Vector3(transform.position.x, transform.position.y, (39 + i) * .01f), transform.rotation, transform);
            c.suit = Card.Suit.Spades;
            c.value = i + 1;
            c.flipped = true;
            cards.Add(c);
        }
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shuffle() {
        for (int i = 0; i < cards.Count; i++) {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card PullFromDeck() {
        Card c;
        c = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return c;
    }

    public void PutInDeck(Card c) {
        cards.Add(c);
        c.transform.parent = transform;
        c.transform.position = new Vector3(transform.position.x, transform.position.y, cards.Count * .01f);
        c.transform.localRotation = Quaternion.Euler(0,0,0);
        c.flipped = true;
    }
}
