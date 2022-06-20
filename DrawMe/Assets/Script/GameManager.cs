using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Camera cam;

    public Drawing drawingManager;
    public TakeModel serverManager;

    private string savePath;
    private string fileName;

    public static byte[] s;


    void Start()
    {
        cam = Camera.main;
        savePath = Application.dataPath + "/ScreenShots/";

        //화면 해상도 설정
        int setWidth = 1920;
        int setHeight = 1080;
        Screen.SetResolution(setWidth, setHeight, true);  //true:풀스크린, false:창
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
    }
    

    public void OnColor(int color)
    {
        drawingManager.SetColor(color);
    }

    public void OnClickComplete()
    {
        TakeCapture();
        
        float satisfaction = serverManager.Btn();//serverManager.GetAccuracy();
        Customer.satisfaction = satisfaction; //나중에 모델 따라 만족도 설정
        Debug.Log("Customer.satisfaction = " + Customer.satisfaction);
        drawingManager.SetClear();
    }


    private void TakeCapture()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24); //1920 1200
        cam.targetTexture = renderTexture;

        Texture2D screenShot = new Texture2D(750, 800, TextureFormat.RGB24, false); //캡쳐 크기
        cam.Render();
        RenderTexture.active = renderTexture;

        Rect area = new Rect(710, 190, 1460, 1190); //실질적으로 캡쳐 위치 조정하는 부분
        //Rect area = new Rect(750, 0f, 1500, 1000); //기준
        screenShot.ReadPixels(area, 0, 0);
        screenShot.Apply();

        s = screenShot.EncodeToPNG();
        Debug.Log("1:" + s);
        try
        {
            if (Directory.Exists(savePath) == false)
            {
                Directory.CreateDirectory(savePath);
            }
            fileName = savePath + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

            File.WriteAllBytes(fileName, s); //저장
            //File.WriteAllBytes(fileName, screenShot.EncodeToPNG());
        }
        catch (Exception e)
        {
            Debug.Log("capture error");
            Debug.Log(e);
        }

        //Destroy(screenShot);
    }

    private IEnumerator ScreenShotRoutine()
    {
        yield return new WaitForEndOfFrame();
    }
}