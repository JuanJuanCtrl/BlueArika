using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroSequenceEvents : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject textBox;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeOut;

    [Header("Text System")]
    [SerializeField] string textToSpeak;
    [SerializeField] float charDelay = 0.04f;   // typing speed (seconds per character)

    int introIndex = 0;
    bool isPlayingLine = false;
    bool isSkipping = false;

    TMP_Text tmp;

    void Start()
    {
        tmp = textBox.GetComponent<TMP_Text>();
        StartCoroutine(EventStarter());
    }

    void Update()
    {
        // No typing here anymore — typing is handled by a coroutine.
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2f);

        mainTextObject.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMP_Text>().text = "You";

        introIndex = 0;
        yield return StartCoroutine(PlayIntroLine(introIndex));
    }

    IEnumerator PlayIntroLine(int index)
    {
        isPlayingLine = true;
        isSkipping = false;
        nextButton.SetActive(false);

        switch (index)
        {
            case 0:
                textToSpeak = "New day, new life.";
                break;
            case 1:
                textToSpeak = "My name is Asa Webb. Starting today, I’ll be attending Blue Sisters High School.";
                break;
            case 2:
                textToSpeak = "It’s in Kasato, a quiet northern town in Japan.";
                break;
            case 3:
                textToSpeak = "The air feels clean, the people seem joyful.";
                break;
            case 4:
                textToSpeak = "It really feels like the beginning of something great.";
                break;
            case 5:
                textToSpeak = "Suddenly, the train shook. I think someone bumped into me.";
                break;
        }

        tmp.text = "";

        // Typewriter coroutine: slower, controlled speed
        for (int i = 0; i <= textToSpeak.Length; i++)
        {
            if (isSkipping)
            {
                // Instantly show full line if user clicked during typing
                tmp.text = textToSpeak;
                break;
            }

            tmp.text = textToSpeak.Substring(0, i);
            yield return new WaitForSeconds(charDelay);
        }

        // Small pause after full line is visible
        yield return new WaitForSeconds(0.3f);

        isPlayingLine = false;
        nextButton.SetActive(true);
    }

    public void NextButton()
    {
        // If still typing → skip to full line
        if (isPlayingLine)
        {
            isSkipping = true;
            return;
        }

        introIndex++;

        if (introIndex <= 5)
        {
            StartCoroutine(PlayIntroLine(introIndex));
        }
        else
        {
            StartCoroutine(FadeOutAndLoadNext());
        }
    }

    IEnumerator FadeOutAndLoadNext()
    {
        nextButton.SetActive(false);
        textBox.SetActive(false);
        mainTextObject.SetActive(false);

        if (fadeOut != null)
        {
            fadeOut.SetActive(true);
        }

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
}