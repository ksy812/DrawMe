using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public Text time;
    public Image icon_time;
    public static Stopwatch watch;

    public Sprite moon;

    long minutes;

    void Start()
    {
        watch=new Stopwatch();
        //watch.Start();
        minutes = 0;
    }
    void Update()
    {
        //minutes = watch.ElapsedMilliseconds / 60000;
        minutes = watch.ElapsedMilliseconds / 1000; //초
        time.text = minutes + "";
        if (minutes % 2 == 0 && minutes != 0) ;  //2분 지날때 마다 바뀜
        if (minutes == 3) icon_time.sprite = moon; //3분되면 저녁 그림
    }
}
