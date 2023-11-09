using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{

    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>(); // so we dont have to drag and drop it in
        dialogueStarts();
    }

    void Update()
    {   // Wihtout that it would be in the middle of our gameview and not above the character it belongs to
       /* Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 0; // the hight of it
        ChatBackGround.position = Pos;*/
    }

    public void dialogueStarts()
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        //if ((other.gameObject.tag == "Player") /*&& Input.GetKeyDown(KeyCode.F*/)
        
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem .NPCName();
        
    }

    //public void OnTriggerExit()
    //{
    //    FindObjectOfType<DialogueSystem>().OutOfRange();
    //    this.gameObject.GetComponent<NPC>().enabled = false;
    //}
}