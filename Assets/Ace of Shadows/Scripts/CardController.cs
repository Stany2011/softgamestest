using System.Collections;
using UnityEngine;

namespace AceOfShadows
{
    public class CardController : MonoBehaviour
    {

        /// Moves the card to the target position and plays a flip animation (Y-axis rotation).
        public IEnumerator MoveToAndFlip(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;
            Quaternion midRotation = Quaternion.Euler(0, -90f, 0);
            Quaternion endRotation = Quaternion.Euler(0, -180f, 0);

            float halfDuration = duration / 2f;
            float elapsed = 0f;

            // First half: rotate from 0 to 90 and move halfway
            while (elapsed < halfDuration)
            {
                float t = elapsed / halfDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t * 0.5f);
                transform.rotation = Quaternion.Slerp(startRotation, midRotation, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < halfDuration)
            {
                float t = elapsed / halfDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, 0.5f + t * 0.5f);
                transform.rotation = Quaternion.Slerp(midRotation, endRotation, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap to final values
            transform.position = targetPosition;
            transform.rotation = endRotation;
        }
    }
}
