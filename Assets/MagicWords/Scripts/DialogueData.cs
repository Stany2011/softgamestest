using System.Collections.Generic;
using UnityEngine;

namespace MagicWords
{
    [System.Serializable]
    public class DialogueLine
    {
        public string name;
        public string text;
    }

    [System.Serializable]
    public class Avatar
    {
        public string name;
        public string url;
        public string position;
    }

    [System.Serializable]
    public class DialogueRoot
    {
        public List<DialogueLine> dialogue;
        public List<Avatar> avatars;
    }
}
