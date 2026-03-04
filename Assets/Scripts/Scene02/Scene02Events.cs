using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene03Event : MonoBehaviour
{

    [SerializeField] GameObject fadeScreenIn;

    void Start()
    {
        StartCoroutine(EventStarter());
    }


    void Update()
    {

    }

    IEnumerator EventStarter()
    {
        // event 0);
        fadeScreenIn.SetActive(true);
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);

    }

}
