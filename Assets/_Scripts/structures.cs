using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils
{
    namespace structures
    {
        [System.Serializable]
        public struct dialogueFrame
        {
            [SerializeField] public Sprite imageTalker;
            [SerializeField] public AudioClip audio;
            [SerializeField] public string talker;
            [SerializeField, TextArea(3, 8)] public string conversation;
        }
    }
}
