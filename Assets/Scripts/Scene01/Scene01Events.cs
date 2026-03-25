using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Events : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charKasumi;
    public GameObject charHaruka;
    public GameObject textBox;
    [SerializeField] AudioSource girlSigh;
    [SerializeField] AudioSource girlGasp;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeOut;

    // index within the intro dialogue sequence
    int introIndex = 0;
    // index within the Koda dialogue sequence (Event 1)
    int kodaIndex = 0;

    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        // initial fade & character appear
        yield return new WaitForSeconds(2f);
        fadeScreenIn.SetActive(false);
        charKasumi.SetActive(true);
        yield return new WaitForSeconds(2f);

        mainTextObject.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";

        // start first intro line
        introIndex = 0;
        yield return StartCoroutine(PlayIntroLine(introIndex));

        // after the first line, we'll rely on NextButton to advance
        eventPos = 0; // 0 = still in intro sequence
    }

    IEnumerator PlayIntroLine(int index)
    {
        nextButton.SetActive(false);

        switch (index)
        {
            case 0:
                textToSpeak = "Oh! My bad, this always happens on this bump.";
                break;
            case 1:
                textToSpeak = "[He laughs, brushing his hair back]";
                break;
            case 2:
                textToSpeak = "Wait… You're the new transfer student, right? No way! I actually got to meet you first.";
                break;
            case 3:
                textToSpeak = "[He grins] Guess that means I've got a head start on being your first friend!";
                break;
        }

        // Set character name for intro lines
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";

        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        // optional: play a sound only on the first line
        if (index == 0 && girlSigh != null)
        {
            girlSigh.Play();
        }

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);

        // wait for typewriter to finish
        yield return new WaitUntil(() => textLength >= currentTextLength);

        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
    }

    IEnumerator PlayKodaLine(int index)
    {
        nextButton.SetActive(false);

        // Set character name based on line type - "You" for bracketed narration (0,6,7), "Koda" for dialogue
        bool isNarration = (index == 0 || index == 6 || index == 7);
        charName.GetComponent<TMPro.TMP_Text>().text = isNarration ? "You" : "Koda";

        switch (index)
        {
            case 0:
                textToSpeak = "[You feel a surge of energy coursing through you. He facepalms]";
                break;
            case 1:
                textToSpeak = "Oh, I almost forgot to introduce myself. My name is Koda Sable, but you can just call me Koda.";
                break;
            case 2:
                textToSpeak = "Anyways, we're close to school. Bummer…";
                break;
            case 3:
                textToSpeak = "I hope we have classes together, we can sneak notes and stuff!";
                break;
            case 4:
                textToSpeak = "Wait no, that's what lovers do.";
                break;
            case 5:
                textToSpeak = "You know what, let's hang out after school!";
                break;
            case 6:
                textToSpeak = "[You felt determined to make the most out of this situation. You agreed to hanging out with him after school.]";
                break;
            case 7:
                textToSpeak = "What could be the worst that could happen?]";
                break;
        }

        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);

        // wait for typewriter to finish
        yield return new WaitUntil(() => textLength >= currentTextLength);

        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
    }

    IEnumerator EventTwo()
    {
        // event 2
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textToSpeak = "Oh, you startled me. I didn't expect you there.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator EventThree()
    {
        // event 3
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Haruka";
        textToSpeak = "I'm sorry, I didn't meant to... Let's go the park and look for Akane.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator EventFour()
    {
        // event 4
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(3);
    }

    public void NextButton()
    {
        // still in intro sequence
        if (eventPos == 0)
        {
            introIndex++;

            // we have 4 intro lines: 0,1,2,3
            if (introIndex <= 3)
            {
                StartCoroutine(PlayIntroLine(introIndex));
            }
            else
            {
                // intro finished, move to Koda dialogue
                eventPos = 1;
                kodaIndex = 0;
                StartCoroutine(PlayKodaLine(kodaIndex));
            }
            return;
        }

        // in Koda dialogue sequence (eventPos == 1)
        if (eventPos == 1)
        {
            kodaIndex++;

            // 8 Koda lines: 0-7
            if (kodaIndex <= 7)
            {
                StartCoroutine(PlayKodaLine(kodaIndex));
            }
            else
            {
                // Koda dialogue finished, move to main events
                eventPos = 2;
                StartCoroutine(EventTwo());
            }
            return;
        }

        // main events
        if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        else if (eventPos == 3)
        {
            StartCoroutine(EventThree());
        }
        else if (eventPos == 4)
        {
            StartCoroutine(EventFour());
        }
    }
}