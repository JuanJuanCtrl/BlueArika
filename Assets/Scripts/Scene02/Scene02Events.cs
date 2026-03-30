using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene02Event : MonoBehaviour
{
    [Header("Character Expressions")]
    public GameObject charNeutral;
    public GameObject charSurprised;
    public GameObject charSmirk;
    public GameObject charSmile;
    public GameObject charEmbarrassed;

    public GameObject fadeScreenIn;
    public GameObject textBox;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeOut;

    [SerializeField] GameObject treeInteract;
    [SerializeField] GameObject houseInteract;

    int introIndex = 0;
    int searchIndex = 0;

    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    void HideAllExpressions()
    {
        charNeutral.SetActive(false);
        charSurprised.SetActive(false);
        charSmirk.SetActive(false);
        charSmile.SetActive(false);
        charEmbarrassed.SetActive(false);
    }

    void ShowExpression(string expression)
    {
        HideAllExpressions();

        switch (expression)
        {
            case "neutral":
                charNeutral.SetActive(true);
                break;
            case "surprised":
                charSurprised.SetActive(true);
                break;
            case "smirk":
                charSmirk.SetActive(true);
                break;
            case "smile":
                charSmile.SetActive(true);
                break;
            case "embarrassed":
                charEmbarrassed.SetActive(true);
                break;
        }
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2f);
        fadeScreenIn.SetActive(false);
        ShowExpression("neutral");
        yield return new WaitForSeconds(2f);

        mainTextObject.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";

        introIndex = 0;
        yield return StartCoroutine(PlayIntroLine(introIndex));

        eventPos = 0;
    }

    IEnumerator PlayIntroLine(int index)
    {
        nextButton.SetActive(false);

        switch (index)
        {
            case 0:
                ShowExpression("surprised");
                textToSpeak = "Let's start looking around.";
                break;
            case 1:
                ShowExpression("smile");
                textToSpeak = "Maybe there's something near the tree.";
                break;
            case 2:
                ShowExpression("neutral");
                textToSpeak = "Nope, nothing here.";
                break;
            case 3:
                ShowExpression("smirk");
                textToSpeak = "Maybe the house is worth checking.";
                break;
        }

        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
    }

    IEnumerator PlaySearchLine(int index)
    {
        nextButton.SetActive(false);

        switch (index)
        {
            case 0:
                ShowExpression("neutral");
                charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
                textToSpeak = "Let's start looking around.";
                break;
            case 1:
                ShowExpression("smile");
                charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
                textToSpeak = "Maybe there's something near the tree.";
                break;
            case 2:
                ShowExpression("neutral");
                charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
                textToSpeak = "Nope, nothing here.";
                break;
            case 3:
                ShowExpression("smirk");
                charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
                textToSpeak = "Maybe the house is worth checking.";
                break;
        }

        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
    }

    IEnumerator EventFour()
    {
        nextButton.SetActive(false);
        ShowExpression("smile");
        textBox.SetActive(true);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(3);
    }

    public void TreeInteract()
    {
        StartCoroutine(TreeInteractSeq());
    }

    public void BuildingInteract()
    {
        StartCoroutine(BuildingInteractSeq());
    }

    IEnumerator TreeInteractSeq()
    {
        treeInteract.SetActive(false);
        houseInteract.SetActive(false);

        ShowExpression("neutral");
        yield return new WaitForSeconds(1f);

        mainTextObject.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textToSpeak = "Nope, nothing behind the tree.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);

        houseInteract.SetActive(true);
    }

    IEnumerator BuildingInteractSeq()
    {
        treeInteract.SetActive(false);
        houseInteract.SetActive(false);

        ShowExpression("smile");
        yield return new WaitForSeconds(0.5f);

        mainTextObject.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textToSpeak = "Oh, you found me. Lets all go somewhere else.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);

        eventPos = 4;
        StartCoroutine(EventFour());
    }

    public void NextButton()
    {
        if (eventPos == 0)
        {
            introIndex++;
            if (introIndex <= 3)
            {
                StartCoroutine(PlayIntroLine(introIndex));
            }
            else
            {
                eventPos = 1;
                searchIndex = 0;
                StartCoroutine(PlaySearchLine(searchIndex));
            }
            return;
        }

        if (eventPos == 1)
        {
            searchIndex++;
            if (searchIndex <= 3)
            {
                StartCoroutine(PlaySearchLine(searchIndex));
            }
            else
            {
                eventPos = 4;
                StartCoroutine(EventFour());
            }
            return;
        }

        if (eventPos == 4)
        {
            StartCoroutine(EventFour());
        }
    }
}