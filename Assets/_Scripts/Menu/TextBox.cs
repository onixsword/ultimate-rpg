using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUtils.structures;

public class TextBox : MonoBehaviour {

    [SerializeField] private Image actorImage_img;
    [SerializeField] private Text actorName_txt;
    [SerializeField] private Text dialogue_txt;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private Sprite unknownCharacter;

    private void Awake()
    {
        actorImage_img.sprite = unknownCharacter;
        audioEffects.clip = null;
        actorName_txt.text = "";
        dialogue_txt.text = "";
    }

    public void showDialogue(dialogueFrame frame)
    {
        this.gameObject.SetActive(true);
        actorImage_img.sprite = frame.imageTalker == null ? unknownCharacter : frame.imageTalker;
        audioEffects.clip = frame.audio;
        actorName_txt.text = frame.talker;
        dialogue_txt.text = frame.conversation;
        if (frame.audio != null) audioEffects.Play();
    }

    public void closeTextBox()
    {
        actorImage_img.sprite = unknownCharacter;
        audioEffects.clip = null;
        actorName_txt.text = "";
        dialogue_txt.text = "";
        this.gameObject.SetActive(false);
    }
}
