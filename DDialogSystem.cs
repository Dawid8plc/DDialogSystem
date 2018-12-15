using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DDialogSystem : MonoBehaviour {

    public Text DialogText;
    public Text CharacterText;

    public TMPro.TextMeshProUGUI NewDialogText;
    public TMPro.TextMeshProUGUI NewCharacterText;

    public GameObject ChatWindow;


    public float Delay = 0.1f;
    public string FullText;
    public string[,] FullTextA;
    public string CurrentText;

    public Image CharacterImage;



    public GameController GController;

    public int DialogShown = 0;
    
    //0 = No dialog is shown on the screen
    //1 = The dialog starts showing up
    //2 = The dialog is currently on the screen

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Animate();
        AnimateBack();
        


    }

    private void AnimateDialog()
    {
        throw new NotImplementedException();
    }

    public bool ShowDialog(string[,] DText)
    {
        FullTextA = DText;


        //NewDialogText.text = DText;
        //CharacterText.text = Character;

        DialogShown = 1;
        return true;
    }

    void Animate()
    {
        if (DialogShown == 1)
        {
            if (ChatWindow.transform.position.y >= 687.0f)
            {
                ChatWindow.transform.Translate(new Vector3(0f, -10f, 0f));
            }
            else
            {
                DialogShown = 2;
                StartCoroutine(ShowText());

            }
        }
    }

    void AnimateBack()
    {
        if (DialogShown == 3)
        {
            if (ChatWindow.transform.position.y <= 849.0f)
            {
                ChatWindow.transform.Translate(new Vector3(0f, +10f, 0f));
            }
            else
            {
                DialogShown = 0;
            }
        }
    }

    public int CurDialogPart = 0;

    public int i = 0;

    IEnumerator ShowText()
    {

        if (CurDialogPart > FullTextA.GetLength(0) - 1)
        {
            AnimateBack();
            StopAllCoroutines();
            i = 0;
            CurDialogPart = 0;
            DialogShown = 3;
            
        }

        for (i = 0; i < FullTextA[CurDialogPart, 1].Length + 1; i++)
        {
            NewCharacterText.text = FullTextA[CurDialogPart, 0];
            CurrentText = FullTextA[CurDialogPart, 1].Substring(0, i);
            NewDialogText.text = CurrentText;
            //Debug.Log(CurrentText);
            yield return new WaitForSeconds(Delay);
            if (i == FullTextA[CurDialogPart, 1].Length)
            {
                StopAllCoroutines();
                Invoke("ContinueDialog", 1f);
                //i = 0;
            }
        }
    }

    void ContinueDialog()
    {
        Debug.Log(CurDialogPart);
        CancelInvoke();
        i = 0;
        CurDialogPart += 1;
        StartCoroutine(ShowText());
        
    }
}
