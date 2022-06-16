using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private static float amount = 0;
    public Text text;

    public static void AddMoney(float money)
    {
        amount += money;
    }

    void Start()
    {
        text.text = "0";
    }
    void Update()
    {
        text.text = amount+"";
    }
}
