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

    public void Start()
    {
        text.text = "0";
    }
    public void Update()
    {
        text.text = amount+"";
        //더 좋은 방법 찾아보기
    }
}
