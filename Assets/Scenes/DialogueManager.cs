using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public DialogueTrigger dialogueTrigger;
    public Animator animator;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueTrigger.TriggerDialogue();
        
    }

    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence(){
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
           dialogueText.text += letter;
           yield return null; 
        }
    }
    void EndDialogue(){
        animator.SetBool("isOpen", false);
        StartCoroutine(WaitForChangeScene());
    }

    IEnumerator WaitForChangeScene()
    {
        yield return new WaitForSeconds(5f);
        Loader.Load(Loader.Scene.SelectCharacterScene);
    }
}
