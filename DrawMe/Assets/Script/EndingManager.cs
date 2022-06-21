using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public Image ending;
    public Sprite[] ending_back;

    bool is_success;
    private void Start()
    {
        if (Money.amount > 20) is_success = true;
        else is_success = false;

        if (is_success)
        {
            ending.sprite = ending_back[0];
        }else
        {
            ending.sprite = ending_back[1];
        }
    }
}
