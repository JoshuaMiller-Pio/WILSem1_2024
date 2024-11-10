using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTut : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
