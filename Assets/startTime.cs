using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
