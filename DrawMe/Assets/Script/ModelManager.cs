using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

using System.IO;
using System.Text;

public class packet
{
    public int errorno;
}

public class getter : packet
{
    public float acc;
}

public class res_uploadimage : packet
{
    public bool success;
}

public class ModelManager : MonoBehaviour
{
    string url = "http://localhost:3000";
    //public Sprite sampleImage;
    public Image image;
    public float accuracy = -1f;
    public string type = "cat";

    private void Start()
    {
        Debug.Log("ModelManager Start() call");
    }

    public float TakeModel()
    {
        StartCoroutine(Upload(result =>
        {
            var responseResult = JsonConvert.DeserializeObject<res_uploadimage>(result);
            Debug.Log("Upload 성공여부 : " + responseResult.success);
        }));
        StartCoroutine(GetAccuracy(result =>
        {
            var responseResult = JsonConvert.DeserializeObject<getter>(result);
            this.accuracy = responseResult.acc;
            Debug.Log("Get Accuracy : " + responseResult.acc);
        }));
        return accuracy;
    }


    public IEnumerator GetAccuracy(System.Action<float> OnCompleteDownload) //string imaggename, System.Action<Texture> OnCompleteDownload
    {
        var path = string.Format("{0}/{1}", url, "accuracy");
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(path);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            var result = webRequest.downloadHandler.data;
            OnCompleteDownload(result);

            Debug.Log("GetAccuracy() 요청 정상 실행");
            //OnCompleteDownload(((DownloadHandlerTexture)webRequest.downloadHandler).text);
        }
    }

    private IEnumerator Upload(System.Action<string> OnCompleteUpload)
    {
        string filename = GameManager.fileName;

        var tex = GameManager.screenShotPNG;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        //System.String name, System.Byte[] data, System.String fileName, System.String contentType
        formData.Add(new MultipartFormFileSection("imgfile", tex, filename, "image/png"));
        //formData.Add(new MultipartFormFileSection("type", type));

        var path = string.Format("{0}/{1}", url, "uploadimage"); //"{0}/{1}"의 의미??

        UnityWebRequest webRequest = UnityWebRequest.Post(path, formData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        var result = webRequest.downloadHandler.text;
        OnCompleteUpload(result);
    }
}