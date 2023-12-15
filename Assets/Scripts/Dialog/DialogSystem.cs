using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;

    public event Action onDialogStart;
    public event Action onDialogEnd;

    [SerializeField]
    GameObject dialogPanel;
    [SerializeField]
    TextMeshProUGUI textComponent;

    string[] dialogText;
    int index = 0;
    bool finishedLine;

    public float delayBetweenLetters;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartDialog(string[] dialog)
    {
        if (!dialogPanel.activeInHierarchy)
        {
            if (dialog.Length > 0)
            {
                textComponent.text = "";
                index = 0;

                dialogText = dialog;
                dialogPanel.SetActive(true);

                onDialogStart?.Invoke();

                StartCoroutine(ShowText());
            }
        }
    }

    void EndDialog()
    {
        textComponent.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        finishedLine = false;

        onDialogEnd?.Invoke();
    }

    public void SkipPressed()
    {
        if (dialogPanel.activeInHierarchy)
        {
            if (finishedLine)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogText[index];
                finishedLine = true;
            }
        }
    }

    public void NextLine()
    {
        if (index < dialogText.Length - 1)
        {
            finishedLine = false;
            index++;
            textComponent.text = "";
            StartCoroutine(ShowText());
        }
        else
        {
            EndDialog();
        }
    }

    IEnumerator ShowText()
    {
        foreach (char c in dialogText[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        finishedLine = true;
    }
}
