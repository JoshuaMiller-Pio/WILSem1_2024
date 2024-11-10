using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTimer : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.startTimer();
    }
}
