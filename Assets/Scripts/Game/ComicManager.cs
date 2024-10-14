using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicManager : MonoBehaviour
{
    #region VARIABLES

    [Header("Required Reference")]
    public ComicStripSO comicStripSO;

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
        if (comicStripSO.playComicIndex > -1)
        {
            stripInstance = comics[comicStripSO.playComicIndex];
        }
        else
        {
            stripInstance = comics[0];
        }

        StartCoroutine(PlayComic());
    }

    private IEnumerator PlayComic()
    {
        int comicsPlayed = 0;
        yield return new WaitForSeconds(startWait);

        do
        {
            Instantiate(stripInstance.panels[comicsPlayed], comicParent);
            comicsPlayed++;
            yield return new WaitForSeconds(panelDelay);
        }
        while (comicsPlayed <= comics.Length);
    }
}

[System.Serializable]
public class ComicStrip
{
    public GameObject[] panels;
}
