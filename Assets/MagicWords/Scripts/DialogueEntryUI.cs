using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

namespace MagicWords
{
    public class DialogueEntryUI : MonoBehaviour
    {
        public TMP_Text nameText;
        public TMP_Text messageText;
        public RawImage avatarImage;
        public HorizontalLayoutGroup layoutGroup;
        public RectTransform textPanel;
        public TextAlignmentOptions leftAlign = TextAlignmentOptions.Left;
        public TextAlignmentOptions rightAlign = TextAlignmentOptions.Right;

        public void Setup(string name, string message, string avatarUrl, bool avatarOnRight)
        {
            nameText.text = name;
            messageText.text = EmojiDictionary.ReplacePlaceholders(message);

            StartCoroutine(LoadImageCoroutine(avatarUrl));

            // Reorder layout: left or right
            if (avatarOnRight)
            {
                avatarImage.transform.SetSiblingIndex(1); // move avatar to the right
                layoutGroup.childAlignment = TextAnchor.MiddleRight;
                textPanel.pivot = new Vector2(1, 0.5f);
                messageText.alignment = rightAlign;
                nameText.alignment = rightAlign;
            }
            else
            {
                avatarImage.transform.SetSiblingIndex(0); // move avatar to the left
                layoutGroup.childAlignment = TextAnchor.MiddleLeft;
                textPanel.pivot = new Vector2(0, 0.5f);
                messageText.alignment = leftAlign;
                nameText.alignment = leftAlign;
            }
        }

        private IEnumerator LoadImageCoroutine(string url)
		{
		    if (string.IsNullOrEmpty(url))
		    {
		        avatarImage.texture = Resources.Load<Texture2D>("PlaceholderAvatar");
		        yield break;
		    }

		    using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
		    {
		        yield return www.SendWebRequest();

		        if (www.result == UnityWebRequest.Result.Success)
		        {
		            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
		            avatarImage.texture = tex;
		        }
		        else
		        {
		            Debug.LogWarning("Failed to load avatar: " + url);
		            avatarImage.texture = Resources.Load<Texture2D>("PlaceholderAvatar");
		        }
		    }
		}




    }
}
