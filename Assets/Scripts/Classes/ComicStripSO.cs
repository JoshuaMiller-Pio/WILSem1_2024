using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "TransitionData", menuName = "ScriptableObjects/Data/TransitionData", order = 1)]
public class TransitionDataSO : ScriptableObject
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
