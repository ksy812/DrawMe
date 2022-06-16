using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    public GameObject menu;
    public void ClickButton()
    {
        switch (this.gameObject.name)
        {
            case "BackBtn":
                Time.watch.Start();
                menu.SetActive(false);
                break;
            case "StopBtn":
                Time.watch.Stop();
                menu.SetActive(true);
                break;
        }
        
    }
}
