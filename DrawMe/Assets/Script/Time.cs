using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Time : MonoBehaviour
{
    public Text time;
    public Image icon_time;
    public static Stopwatch watch;

    public Sprite moon;

    long minutes;

    private void Awake()
    {

        watch = new Stopwatch();
        minutes = 0;
    }
    
    void Update()
    {
        //minutes = watch.ElapsedMilliseconds / 60000;
        minutes = watch.ElapsedMilliseconds / 1000; //초
        time.text = minutes + "";
        if (minutes % 2 == 0 && minutes != 0) ;  //2분 지날때 마다 바뀜
        if (minutes == 3) icon_time.sprite = moon; //3분 되면 저녁 그림
        if (minutes >= 6 && Customer.object_is_destory) //6분 되면 게임 종료
        {
            UnityEngine.Debug.Log("엔딩씬 호출");
            watch.Stop();
            watch.Reset();
            DontDestroy.destroy=true;
            //엔딩 효과
            SceneManager.LoadScene("SceneEnding");
            
        }
    }
}
