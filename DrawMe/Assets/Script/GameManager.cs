using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public Slider slider;

    public Drawing drawingManager;
    public ModelManager modelManager;

    private string savePath;
    public static string fileName;
    public static byte[] screenShotPNG;


    void Start()
    {
        cam = Camera.main;
        savePath = Application.dataPath + "/Resources/ScreenShots/";

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

    public void OnThickness()
    {
        drawingManager.SetThickness(slider.value);
    }
    public void OnClickComplete()
    {
        TakeCapture();
        modelManager.TakeModel();
        //Customer.satisfaction = 60.0f;
        //float satisfaction = serverManager.accuracy;//serverManager.TakeModel();
        /*Customer.satisfaction = satisfaction; //나중에 모델 따라 만족도 설정
        Debug.Log("satisfaction = " + satisfaction);
        Debug.Log("Customer.satisfaction = " + Customer.satisfaction);*/
        //Debug.Log("serverManager.TakeModel() / drawingManager.SetClear()");

        drawingManager.SetClear();
        cam.targetTexture = null;


        //Debug.Log("========test===========" + Customer.satisfaction);

        //        SceneManager.LoadScene("SceneGameCustomer");
        //        GameManager_order.customer.SetActive(true);
        //        GameManager_order.orderbox.SetActive(true);
    }


    private void TakeCapture()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24); //1920 1200
        cam.targetTexture = renderTexture;

        Texture2D screenShot = new Texture2D(720, 780, TextureFormat.RGB24, false); //캡쳐 크기
        cam.Render();
        RenderTexture.active = renderTexture;

        Rect area = new Rect(720, 200, 720, 780); //직접적으로 캡쳐 위치 조정하는 부분
        //Rect area = new Rect(750, 0f, 1500, 1000); //기준
        screenShot.ReadPixels(area, 0, 0);
        screenShot.Apply();

        screenShotPNG = screenShot.EncodeToPNG();
        //Debug.Log("1:" + screenShotPNG);
        try
        {
            if (Directory.Exists(savePath) == false)
            {
                Directory.CreateDirectory(savePath);
            }
            fileName = savePath + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

            File.WriteAllBytes(fileName, screenShotPNG); //저장
            //File.WriteAllBytes(fileName, screenShot.EncodeToPNG());
        }
        catch (Exception e)
        {
            Debug.Log("capture error");
            Debug.Log(e);
        }
        //Destroy(screenShot);
    }

    /*    private IEnumerator ScreenShotRoutine()
        {
            yield return new WaitForEndOfFrame();
        }*/
}