using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils.structures;


[CreateAssetMenu(fileName = "Dialogue", menuName = "NPC/Interaction/Dialogue", order = 1)]
public class Dialogue : ScriptableObject {

    [SerializeField] public List<dialogueFrame> fullDialogue;

}
