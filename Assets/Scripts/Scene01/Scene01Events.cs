using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Events : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charKasumi;
    public GameObject charHaruka;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        charKasumi.SetActive(true);
        yield return new WaitForSeconds(2);
        // this is where our text function will go in future tutorial
        yield return new WaitForSeconds(2);
        charHaruka.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
