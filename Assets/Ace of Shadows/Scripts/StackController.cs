using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AceOfShadows
{
    public class StackController : MonoBehaviour
    {
        [Header("UI Elements")]
        public TMP_Text counterText;

        [Header("Card Anchor")]
        public Transform cardAnchor;

        private readonly List<CardController> cards = new List<CardController>();


        /// Adds a card to the stack and updates the counter.
        public void AddCard(CardController card)
        {
            if (card == null) return;

            card.transform.SetParent(cardAnchor);
            cards.Add(card);
            UpdateCounter();
        }


        public void ClearAllCards()
        {
            foreach (var card in cards)
            {
                if (card != null)
                    Destroy(card.gameObject);
            }

            cards.Clear();
            UpdateCounter();
        }


        /// Removes the top card from the stack (last added). 
        public CardController RemoveTopCard()
        {
            if (cards.Count == 0) return null;

            CardController topCard = cards[0]; // First = top of the visual stack
            cards.RemoveAt(0);
            UpdateCounter();
            return topCard;
        }



        /// Updates the UI text to reflect the current card count.
        public void UpdateCounter()
        {
            if (counterText != null)
                counterText.text = cards.Count.ToString();
        }


        /// Initializes the card stack with a given list.
        public void LoadCards(List<CardController> initialCards)
        {
            cards.Clear();
            cards.AddRange(initialCards);

            foreach (var card in cards)
            {
                card.transform.SetParent(cardAnchor);
            }

            UpdateCounter();
        }

        /// Number of cards currently in the stack.
        public int CardCount => cards.Count;
    }
}
