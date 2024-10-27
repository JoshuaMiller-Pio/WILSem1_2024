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

    private void Start()
    {
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
        yield return new WaitForSeconds(startWait);
       
            for (int i = 0; i < 3; i++)
            {
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
