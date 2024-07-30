using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float time = Globals.gameTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
