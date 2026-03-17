using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02Event : MonoBehaviour
{
    public GameObject textBox;
    [SerializeField] GameObject fadeScreenIn;
    [SerializeField] GameObject charKasumi;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject treeInteract;
    [SerializeField] GameObject houseInteract;
    [SerializeField] GameObject charAkane;
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject charName;
    //these are for the randomized scene
    [SerializeField] GameObject parkDay;
    [SerializeField] GameObject parkNight;
    [SerializeField] GameObject dayBGM;
    [SerializeField] GameObject nightBGM;
    [SerializeField] int randomScene;

    void Start()
    {
        PlayerPrefs.SetInt("LoadState", 2);
        randomScene = Random.Range(1, 3);
        if(randomScene == 1)
        {
            parkDay.SetActive(true);
            dayBGM.SetActive(true);
        }
        else
        {
            parkNight.SetActive(true);
            nightBGM.SetActive(true);
        }
        StartCoroutine(EventStarter());
    }



    void Update()
    {
        textLength = TextCreator.charCount;
    }

    IEnumerator EventStarter()
    {
        // event 0
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        fadeScreenIn.SetActive(false);
        charKasumi.SetActive(true);
        yield return new WaitForSeconds(2);
        // this is where our text function will go in future tutorial
        mainTextObject.SetActive(true);
        textToSpeak = "Let's start looking for Akane.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        //nextButton.SetActive(true);
        eventPos = 1;
        // auto start looking for Akane
        yield return new WaitForSeconds(2);
        charKasumi.SetActive(false);
        mainTextObject.SetActive(false);
        treeInteract.SetActive(true);
        houseInteract.SetActive(true);

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
        charKasumi.SetActive(true);
        yield return new WaitForSeconds(2);
        // this is where our text function will go in future tutorial
        mainTextObject.SetActive(true);
        textToSpeak = "Nope, Akane is not behind the tree.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        //nextButton.SetActive(true);
        eventPos = 1;
        // auto start looking for Akane
        yield return new WaitForSeconds(2);
        charKasumi.SetActive(false);
        mainTextObject.SetActive(false);
        houseInteract.SetActive(true);

    }

    IEnumerator BuildingInteractSeq()
    {
        treeInteract.SetActive(false);
        houseInteract.SetActive(false);
        charAkane.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        charName.GetComponent<TMPro.TMP_Text>().text = "Akane";
        // this is where our text function will go in future tutorial
        mainTextObject.SetActive(true);
        textToSpeak = "Oh you found me. Hehe. Lets all go somewhere else.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        //nextButton.SetActive(true);
        eventPos = 3;
        // auto start looking for Akane
        yield return new WaitForSeconds(2);
        charAkane.SetActive(false);
        mainTextObject.SetActive(false);
        fadeOut.SetActive(true);

    }

}
