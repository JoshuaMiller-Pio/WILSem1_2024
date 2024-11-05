using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ComicManager : MonoBehaviour
{
    #region VARIABLES

    [Header("Required Reference")]
    public TransitionDataSO comicStripSO;

    public Button butt;
    [Header("Play Settings")]
    public float startWait;
    public float panelDelay;

    [Header("Comic Strip References")]
    public Transform comicParent;
    public ComicStrip[] comics;

    private ComicStrip stripInstance;

    #endregion

    private void OnEnable()
    {
        Time.timeScale = 1;
        Debug.Log("test");
        butt.interactable = false; 
        if (GameManager.Instance.conc)
        {
            stripInstance = comics[4];

        }
        else
        {
            stripInstance = comics[GameManager.Instance.levelNumber];

        }
        StartCoroutine(PlayComic());
    }
   
    private IEnumerator PlayComic()
    {
        Debug.Log(GameManager.Instance.levelNumber);

        yield return new WaitForSeconds(startWait);

            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Playing Comic " + (i + 1));
                Instantiate(stripInstance.panels[i], comicParent);
                yield return new WaitForSeconds(panelDelay);
            }
        
        
        
        butt.interactable = true;
        yield return null;
    }
}

[System.Serializable]
public class ComicStrip
{
    public GameObject[] panels;
}
