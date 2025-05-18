using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AceOfShadows
{
    public class DeckManager : MonoBehaviour
    {
        [Header("Card Settings")]
        public GameObject cardPrefab;
        public int totalCards = 144;
        public float yOffset = 0.015f; // Add to DeckManager header
        public float zOffset = 0.001f;

        [Header("Stack References")]
        public StackController startStack;
        public StackController targetStack;

        [Header("Timing Settings")]
        public float moveDuration = 0.5f;

        [Header("Speed Settings")]
        public float moveInterval = 1f;
        public float minInterval = 0.1f;
        public float maxInterval = 2f;
        public float speedStep = 0.1f;

        [Header("UI")]
        public GameObject finalMessageText;
        public TextMeshProUGUI speedDisplayText;

        private Coroutine moveRoutine;

        public AudioSource flipSound;

        private void OnEnable()
        {
            ResetDeck(); // always restart fresh
            StartCoroutine(GenerateDeckCoroutine());
        }

        private void OnDisable()
        {
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }
        }

        private void ResetDeck()
        {
            // Stop coroutine
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }

            // Remove cards from both stacks
            startStack.ClearAllCards();
            targetStack.ClearAllCards();

            // Hide final message if visible
            if (finalMessageText != null)
                finalMessageText.gameObject.SetActive(false);
        }


        private void UpdateSpeedDisplay()
        {
            if (speedDisplayText != null)
                speedDisplayText.text = $"Speed: {moveInterval:0.0}s";
        }


        public void IncreaseSpeed()
        {
            moveInterval = Mathf.Max(minInterval, moveInterval - speedStep);
            UpdateSpeedDisplay();
        }

        public void DecreaseSpeed()
        {
            moveInterval = Mathf.Min(maxInterval, moveInterval + speedStep);
            UpdateSpeedDisplay();
        }

        /// Instantiates cards one by one with position offsets to build the visual stack.
        IEnumerator GenerateDeckCoroutine()
        {
            List<CardController> cards = new List<CardController>();

            for (int i = 0; i < totalCards; i++)
            {
                GameObject cardGO = Instantiate(cardPrefab);
                cardGO.name = $"Card_{i + 1}";
                CardController card = cardGO.GetComponent<CardController>();

                float currentY = -yOffset * i; // Increasing Y as we go deeper
                float currentZ = zOffset * (totalCards - i - 1); // Top card has Z=0

                Vector3 cardPosition = startStack.cardAnchor.position + new Vector3(0f, currentY, currentZ);
                card.transform.position = cardPosition;

                cards.Add(card);
                yield return null; // Frame delay
            }

            startStack.LoadCards(cards);
            StartCoroutine(MoveCardsRoutine());
        }


        /// Moves cards one by one from the start stack to the target stack with a flip animation.
        IEnumerator MoveCardsRoutine()
        {
            while (startStack.CardCount > 0)
            {
                CardController card = startStack.RemoveTopCard();
                if (card != null)
                {
                    int index = targetStack.CardCount;

                    float targetY = yOffset * (index - totalCards + 1);
                    float targetZ = zOffset * index;

                    Vector3 targetPosition = targetStack.cardAnchor.position + new Vector3(0, targetY, targetZ);

                    flipSound.Play();

                    StartCoroutine(card.MoveToAndFlip(targetPosition, moveDuration));
                    targetStack.AddCard(card);
                }

                yield return new WaitForSeconds(moveInterval);
            }

            yield return new WaitForSeconds(0.5f);
            if (finalMessageText != null)
                finalMessageText.SetActive(true);
        }
    }
}
