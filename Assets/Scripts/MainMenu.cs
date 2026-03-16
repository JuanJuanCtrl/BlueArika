using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPic1;
    [SerializeField] GameObject menuPic2;
    [SerializeField] GameObject menuPic3;
    [SerializeField] int picNum = 1;
    [SerializeField] bool isAnimating = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (isAnimating == false)
        {
            isAnimating = true;
            StartCoroutine(RandomPic());
        }
    }

    IEnumerator RandomPic()
    {
        yield return new WaitForSeconds(1);
        picNum = Random.Range(1, 4);
        if(picNum == 1)
        {
            menuPic1.SetActive(true);
            menuPic2.SetActive(false);
            menuPic3.SetActive(false);
        }
        if (picNum == 2)
        {
            menuPic1.SetActive(false);
            menuPic2.SetActive(true);
            menuPic3.SetActive(false);
        }
        if (picNum == 3)
        {
            menuPic1.SetActive(false);
            menuPic2.SetActive(false);
            menuPic3.SetActive(true);
        }
        yield return new WaitForSeconds(7);
        menuPic1.SetActive(false);
        menuPic2.SetActive(false);
        menuPic3.SetActive(false);
        isAnimating = false;
    }
}
