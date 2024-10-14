using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ComicStrip", menuName = "ScriptableObjects/Comics/ComicStrip", order = 1)]
public class ComicStripSO : ScriptableObject
{
    public int playComicIndex;
    public string nextScene;

    //Checks and Sets the nextScene string
    public void SetScene(string scene)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (scene == SceneManager.GetSceneAt(i).name)
            {
                nextScene = scene;
                return;
            }
        }
    }
}
