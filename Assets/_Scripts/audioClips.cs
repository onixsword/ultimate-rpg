using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace audioManagement
{
    [RequireComponent(typeof(AudioSource))]
    [Serializable]
    public class audioClips
    {
        [SerializeField] List<AudioClip> clips;
        /*AudioSource aud;

        public AudioSource Aud
        {
            get
            {
                return aud;
            }

            set
            {
                aud = value;
            }
        }*/


        public AudioClip GetAudio(int index)
        {
            return (index >= 0 || index < clips.Count) ? clips[index] : clips[0];
        }

        /*public void PlayClip(int index)
        {
            aud.clip = clips[index];
            aud.Play();
        }*/
    }
}


