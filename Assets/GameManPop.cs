using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManPop : MonoBehaviour
{
    public GameObject Gameover,GameWin;
    public TMP_Text GameoverUI,timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.gameOver = Gameover;
        GameManager.Instance.timerText = timerText;
        GameManager.Instance.gameOverMessage_UI = GameoverUI;
        GameManager.Instance.gameWin = GameWin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
