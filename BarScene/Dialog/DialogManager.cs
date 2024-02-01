using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public Animator dialogAnimator;
    private Queue<string> sentences;
    public bool isStartDialog;
    private Move _move;

    private void Awake()
    {
        _move = FindObjectOfType<Move>();
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "TutorialScene" && SceneManager.GetActiveScene().name != "BarScene")
        {
            if (Input.GetKeyDown(KeyCode.R)) DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog)
    {
        _move.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _move.enabled = false;
        
        isStartDialog = true;
        dialogAnimator.SetBool("Start",true);
        sentences.Clear();
        foreach (var sentence in dialog.senteces)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (var letter in sentence.ToCharArray() )
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

   
    public void EndDialog()
    {
        _move.enabled = true;
        isStartDialog = false;
        dialogAnimator.SetBool("Start",false);
    }
}
