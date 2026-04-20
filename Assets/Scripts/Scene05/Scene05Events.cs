using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Scene05Event : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource studentChatter;

    [Header("SFX")]
    [SerializeField] private AudioSource phonering;

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

    [Header("Choice Buttons")]
    [SerializeField] Button choiceButton1;
    [SerializeField] Button choiceButton2;
    [SerializeField] Button choiceButton3;

    [Header("Choice Lines")]
    private string[] choiceLines;
    private int currentLine = 0;

    // --- Dialogue segment arrays and indices ---
    private string[] transitionLines =
    {
        "Anyways, the train's gonna get to school pretty soon...",
        "[He softly groans]",
        "There was a weird sound coming from outside my house, it may have just been the wind",
        "I looked outside the window, and everything was... oddly blue.",
        "I swear I haven't gone crazy, bro. I'll even snap some pictures to show you later!",
        "Oh, we're already here. You gonna let me copy off your homework, right?",
        "[Koda's situation was similar to yours, you couldn't help but feel a bit uneasy about the whole situation. You decided to keep it to yourself for now, not wanting to worry about it.]"
    };
    private int transitionLineIndex = 0;

    private string[] phoneCallLines =
    {
        "H- hel-llo?",
        "An-anyo--ne lis-t-tening!?",
        "P-le-ase, som-eo-ne!",
        "H-e--lp!"
    };
    private int phoneCallLineIndex = 0;

    private string[] phoneDisconnectLines =
    {
        "[The call was suddenly disconnected. You shoved your phone back in your pocket]"
    };
    private int phoneDisconnectLineIndex = 0;

    private string[] kodaConcernLines =
    {
        "Uhh, everything good bro?",
        "We can always hang out later if you want…"
    };
    private int kodaConcernLineIndex = 0;

    private string[] finalNarrationLines =
    {
        "[You felt Koda’s concern. Was it just a prank call, or something deeper? Whatever it is, you decided to hang out with Koda without telling him what happened. You walked home with him afterwards]"
    };
    private int finalNarrationLineIndex = 0;

    int introIndex = 0;
    bool isTyping = false; // Prevents spamming Next

    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        // --- Audio ----
        if (bgm != null)
        {
            bgm.volume = 0.7f;
            bgm.loop = true;
            if (!bgm.isPlaying) bgm.Play();
        }
        if (studentChatter != null)
        {
            studentChatter.volume = 0.1f;
            studentChatter.loop = true;
            if (!studentChatter.isPlaying) studentChatter.Play();
        }
        // --------------

        StartCoroutine(EventStarter());
        choiceButton1.onClick.AddListener(OnChoice1);
        choiceButton2.onClick.AddListener(OnChoice2);
        choiceButton3.onClick.AddListener(OnChoice3);
        nextButton.SetActive(false); // Ensure it's hidden at start
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
            case "neutral":     charNeutral.SetActive(true); break;
            case "surprised":   charSurprised.SetActive(true); break;
            case "smirk":       charSmirk.SetActive(true); break;
            case "smile":       charSmile.SetActive(true); break;
            case "embarrassed": charEmbarrassed.SetActive(true); break;
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
        charName.GetComponent<TMP_Text>().text = "Koda";

        introIndex = 0;
        yield return StartCoroutine(PlayIntroLine(introIndex));
        eventPos = 0;
    }

    IEnumerator PlayIntroLine(int index)
    {
        nextButton.SetActive(false);
        isTyping = true;
        switch (index)
        {
            case 0:
                ShowExpression("smile");
                textToSpeak = "Yo! Good morning bro, you good?";
                break;
            case 1:
                ShowExpression("smile");
                textToSpeak = "You were like, practically dragging me yesterday!";
                break;
            case 2:
                ShowExpression("embarrassed");
                textToSpeak = "I had to go running home, I hate walking at night...";
                break;
            case 3:
                ShowExpression("smirk");
                textToSpeak = "[Your mind went back to the events that occurred the previous night. What was that all about?]";
                break;
            case 4:
                ShowExpression("smirk");
                textToSpeak = "[You felt the urge to tell Koda about what happened.]";
                break;
            case 5:
                ShowExpression("smirk");
                textToSpeak = "How should you approach this?";
                break;
        }
        charName.GetComponent<TMP_Text>().text = "Koda";
        textBox.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        isTyping = false;
    }

    void ShowChoiceButtons()
    {
        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);
        choiceButton3.gameObject.SetActive(true);
        nextButton.SetActive(false);
        textBox.SetActive(false);
        mainTextObject.SetActive(false);
    }

    void HideChoiceButtons()
    {
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        choiceButton3.gameObject.SetActive(false);
    }

    public void OnChoice1()
    {
        HideChoiceButtons();
        StartChoiceDialogue(0);
    }

    public void OnChoice2()
    {
        HideChoiceButtons();
        StartChoiceDialogue(1);
    }

    public void OnChoice3()
    {
        HideChoiceButtons();
        StartChoiceDialogue(2);
    }

    void StartChoiceDialogue(int choice)
    {
        textBox.SetActive(true);
        mainTextObject.SetActive(true);
        nextButton.SetActive(false);

        switch (choice)
        {
            case 0:
                ShowExpression("embarrassed");
                choiceLines = new string[]
                {
                    "[You reminded him what happened yesterday, and what happened to you at night.]",
                    "A talking cat inside of your phone? Dude, is the phone possessed or something?",
                    "I mean, it’s not like I haven’t heard of that kind of stuff before, but still…",
                    "Maybe you were just seeing things, I mean everyone sees stuff once in a while, right? It’s probably just your imagination running wild.",
                    "That must be it [laughs nerviously]"
                };
                break;
            case 1:
                ShowExpression("smirk");
                choiceLines = new string[]
                {
                    "[You told him what happened to you last night.]",
                    "A talking cat, inside of your phone? That sounds like a dream, sure you weren't dreaming bro?",
                    "Sounds like you were just seeing things, I mean everyone sees stuff once in a while, right? It’s probably just your imagination running wild.",
                };
                break;
            case 2:
                ShowExpression("embarrassed");
                choiceLines = new string[]
                {
                    "Cat go meow meow, phone go ring ring, and you go scream scream?",
                    "[laughs nervously]",
                    "Did you get enough sleep yesterday? Sounds like a bunch of nonsense to me, bro. You probably just had a nightmare or something.",
                    "Yeah, just a nightmare..."
                };
                break;
        }
        eventPos = 1; // Set event state to choice dialogue
        currentLine = 0;
        StartCoroutine(DisplayChoiceLine(currentLine));
    }

    IEnumerator DisplayChoiceLine(int lineIndex)
    {
        nextButton.SetActive(false);
        isTyping = true;
        if (lineIndex >= 0 && lineIndex < choiceLines.Length)
        {
            textToSpeak = choiceLines[lineIndex];
            charName.GetComponent<TMP_Text>().text = "Koda";
            textBox.GetComponent<TMP_Text>().text = textToSpeak;
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.5f);
            nextButton.SetActive(true);
        }
        isTyping = false;
    }

    IEnumerator DisplayTransitionLine(int lineIndex)
    {
        nextButton.SetActive(false);
        isTyping = true;

        ShowExpression("neutral");
        if (lineIndex >= 0 && lineIndex < transitionLines.Length)
        {
            textToSpeak = transitionLines[lineIndex];
            // --- Always show "Koda" as per your new requirement ---
            charName.GetComponent<TMP_Text>().text = "Koda";

            textBox.SetActive(true);
            mainTextObject.SetActive(true);
            textBox.GetComponent<TMP_Text>().text = textToSpeak;
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            // Special action: phone beeped and fade BGM
            if (textToSpeak == "[While you were complaining about the upcoming test, your phone suddenly beeped. You decided to answer]")
            {
                StartCoroutine(FadeBGMVolume(bgm, 0.3f, 2f)); // fade bgm to 0.3 over 2s
                StartCoroutine(PlayPhoneRingTwice());
            }

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.5f);
            nextButton.SetActive(true);
        }
        isTyping = false;
    }

    IEnumerator DisplayPhoneCallLine(int lineIndex)
    {
        nextButton.SetActive(false);
        isTyping = true;
        ShowExpression("embarrassed"); // Optional: show a distressed/embarrassed face for "Unknown"
        if (lineIndex >= 0 && lineIndex < phoneCallLines.Length)
        {
            textToSpeak = phoneCallLines[lineIndex];
            charName.GetComponent<TMP_Text>().text = "Unknown";
            textBox.SetActive(true);
            mainTextObject.SetActive(true);
            textBox.GetComponent<TMP_Text>().text = textToSpeak;
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.5f);
            nextButton.SetActive(true);
        }
        isTyping = false;
    }

    IEnumerator DisplayPhoneDisconnectLine()
    {
        nextButton.SetActive(false);
        isTyping = true;
        ShowExpression("neutral");
        textToSpeak = phoneDisconnectLines[0];
        charName.GetComponent<TMP_Text>().text = "";
        textBox.SetActive(true);
        mainTextObject.SetActive(true);
        textBox.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);

        isTyping = false;
    }

    IEnumerator DisplayKodaConcernLine(int lineIndex)
    {
        nextButton.SetActive(false);
        isTyping = true;
        ShowExpression("smile");
        if (lineIndex >= 0 && lineIndex < kodaConcernLines.Length)
        {
            textToSpeak = kodaConcernLines[lineIndex];
            charName.GetComponent<TMP_Text>().text = "Koda";
            textBox.SetActive(true);
            mainTextObject.SetActive(true);
            textBox.GetComponent<TMP_Text>().text = textToSpeak;
            currentTextLength = textToSpeak.Length;
            TextCreator.runTextPrint = true;

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.5f);
            nextButton.SetActive(true);
        }
        isTyping = false;
    }

    IEnumerator DisplayFinalNarrationLine()
    {
        nextButton.SetActive(false);
        isTyping = true;
        ShowExpression("neutral");
        textToSpeak = finalNarrationLines[0];
        charName.GetComponent<TMP_Text>().text = "";
        textBox.SetActive(true);
        mainTextObject.SetActive(true);
        textBox.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);

        isTyping = false;
    }

    // NextButton to be called by Unity UI OnClick
    public void NextButton()
    {
        if (isTyping) return; // Don’t skip the typing animation

        if (eventPos == 0)
        {
            introIndex++;
            if (introIndex <= 5)
            {
                StartCoroutine(PlayIntroLine(introIndex));
            }
            else
            {
                eventPos = 1;
                ShowChoiceButtons();
            }
            return;
        }

        if (eventPos == 1)
        {
            if (currentLine < choiceLines.Length - 1)
            {
                currentLine++;
                StartCoroutine(DisplayChoiceLine(currentLine));
            }
            else
            {
                eventPos = 2;
                nextButton.SetActive(false);
                transitionLineIndex = 0;
                StartCoroutine(DisplayTransitionLine(transitionLineIndex));
            }
            return;
        }

        // Handle transition message lines ("Koda monologue + phone rings")
        if (eventPos == 2)
        {
            transitionLineIndex++;
            if (transitionLineIndex < transitionLines.Length)
            {
                StartCoroutine(DisplayTransitionLine(transitionLineIndex));
            }
            else
            {
                eventPos = 3;
                nextButton.SetActive(false);
                phoneCallLineIndex = 0;
                StartCoroutine(DisplayPhoneCallLine(phoneCallLineIndex));
            }
            return;
        }

        // Handle "Unknown" phone call lines
        if (eventPos == 3)
        {
            phoneCallLineIndex++;
            if (phoneCallLineIndex < phoneCallLines.Length)
            {
                StartCoroutine(DisplayPhoneCallLine(phoneCallLineIndex));
            }
            else
            {
                eventPos = 4;
                nextButton.SetActive(false);
                StartCoroutine(DisplayPhoneDisconnectLine());
            }
            return;
        }

        // Handle disconnected narration
        if (eventPos == 4)
        {
            eventPos = 5;
            nextButton.SetActive(false);
            kodaConcernLineIndex = 0;
            StartCoroutine(DisplayKodaConcernLine(kodaConcernLineIndex));
            return;
        }

        // Koda's post-call concern lines
        if (eventPos == 5)
        {
            kodaConcernLineIndex++;
            if (kodaConcernLineIndex < kodaConcernLines.Length)
            {
                StartCoroutine(DisplayKodaConcernLine(kodaConcernLineIndex));
            }
            else
            {
                eventPos = 6;
                nextButton.SetActive(false);
                StartCoroutine(DisplayFinalNarrationLine());
            }
            return;
        }

        // Final narration, then fade out and change scene
        if (eventPos == 6)
        {
            eventPos = 7;
            nextButton.SetActive(false);
            StartCoroutine(EventFour());
            return;
        }
    }

    IEnumerator EventFour()
    {
        nextButton.SetActive(false);
        ShowExpression("smile");
        textBox.SetActive(true);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(6);
    }

    // ========== NEW HELPERS ==========

    IEnumerator FadeBGMVolume(AudioSource audioSource, float targetVolume, float fadeDuration)
    {
        if (audioSource == null) yield break;
        float startVolume = audioSource.volume;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    IEnumerator PlayPhoneRingTwice()
    {
        if (phonering == null) yield break;
        int times = 2;
        for (int i = 0; i < times; i++)
        {
            phonering.Play();
            yield return new WaitForSeconds(phonering.clip.length);
        }
    }
}