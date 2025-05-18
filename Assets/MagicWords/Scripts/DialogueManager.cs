using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;

namespace MagicWords
{
    public class DialogueManager : MonoBehaviour
    {
        public string endpointUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
        public Transform contentParent;
        public GameObject dialogueEntryPrefab;

        private Dictionary<string, Avatar> avatarLookup = new();




        void Start()
        {
            StartCoroutine(LoadDialogueData());
        }
        

        IEnumerator LoadDialogueData()
        {
            UnityWebRequest request = UnityWebRequest.Get(endpointUrl);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load dialogue data: " + request.error);
                yield break;
            }

            var data = JsonConvert.DeserializeObject<DialogueRoot>(request.downloadHandler.text);

            // Prepare avatar lookup
            foreach (var avatar in data.avatars)
			{
			    string key = $"{avatar.name}_{avatar.position}";
			    if (!avatarLookup.ContainsKey(key))
			        avatarLookup[key] = avatar;
			}



            for (int i = 0; i < data.dialogue.Count; i++)
			{
			    var line = data.dialogue[i];

			    GameObject entry = Instantiate(dialogueEntryPrefab, contentParent);
			    DialogueEntryUI ui = entry.GetComponent<DialogueEntryUI>();

			    // Alternate position: even = right, odd = left
			    bool isRight = (i % 2 == 0);
			    string position = isRight ? "right" : "left";

			    // Lookup key: name + position
			    string avatarKey = $"{line.name}_{position}";
			    string avatarUrl = "";

			    if (avatarLookup.TryGetValue(avatarKey, out var avatarData))
			    {
			        // If there is a valid URL, we use it
			        if (!string.IsNullOrWhiteSpace(avatarData.url) && !avatarData.url.Contains("timeout"))
			        {
			            avatarUrl = avatarData.url;
			        }
			    }

			    ui.Setup(line.name, line.text, avatarUrl, isRight);
			}




        }
    }
}
