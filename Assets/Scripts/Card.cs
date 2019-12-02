using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public bool flipped = false;
    
    public enum Suit {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }

    public Suit suit;

    //Jack = 11
    //Queen = 12
    //King = 13
    [Range(1, 13)]
    public int value = 1;
    
    public GameObject front;
    
    public Sprite clubSprite;
    
    public Sprite spadeSprite;
    
    public Sprite heartSprite;
    
    public Sprite diamondSprite;
    
    public SpriteRenderer[] suits;
    
    public TextMeshPro[] values;

    void Start() {
        
    }

    void Update() {
        
        if (flipped && front.activeSelf) {
            front.SetActive(false);
        } else if (!flipped && !front.activeSelf) {
            front.SetActive(true);
        }

        for (int i = 0; i < suits.Length; i++) {
            if (suit == Suit.Spades) {
                suits[i].sprite = spadeSprite;
            } else if(suit == Suit.Diamonds) {
                suits[i].sprite = diamondSprite;
            } else if(suit == Suit.Hearts) {
                suits[i].sprite = heartSprite;
            } else if(suit == Suit.Clubs) {
                suits[i].sprite = clubSprite;
            }
        }
        for (int i = 0; i < values.Length; i++) {
            if (suit == Suit.Spades || suit == Suit.Clubs) {
                values[i].color = Color.black;
            } else {
                values[i].color = Color.red;
            }

            if (value == 11) {
                values[i].text = "J";
            } else if (value == 12) {
                values[i].text = "Q";
            } else if (value == 13) {
                values[i].text = "K";
            } else if (value == 1) {
                values[i].text = "A";
            } else {
                values[i].text = value + "";
            }
        }
    }
}
