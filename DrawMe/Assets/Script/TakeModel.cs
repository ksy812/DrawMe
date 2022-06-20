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

public class req_uploadimage
{
    public string name;
}

public class res_uploadimage : packet
{
    public bool success;
}

public class TakeModel : MonoBehaviour
{
    string url = "http://localhost:3000";
    //public Sprite sampleImage;
    public Image image;
    public float accuracy = -1f;
    public string type = "cat";

    private void Start()
    {
        Debug.Log("Take Model Start() call");
    }

    public float Btn()
    {
        var reqUploadImage = new req_uploadimage();
        //sampleImage.name = "imgfile";
        reqUploadImage.name = "cat.png";
        var json = JsonConvert.SerializeObject(reqUploadImage);

        Debug.Log("json:"+json);

        StartCoroutine(Upload(json, (result) =>
        {
            var responseResult = JsonConvert.DeserializeObject<res_uploadimage>(result);
            Debug.Log("성공여부 : " + responseResult.success);
        }));
        StartCoroutine(GetAccuracy());
        return accuracy;
    }

    public IEnumerator GetAccuracy() //strin imaggename, System.Action<Texture> OnCompleteDownload
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
            this.accuracy = 100f;
            Debug.Log("GetAccuracy() 요청 정상 실행");
            //OnCompleteDownload(((DownloadHandlerTexture)webRequest.downloadHandler).texture);
        }
    }

    private IEnumerator Upload(string data, System.Action<string> OnCompleteUpload)
    {
        //string filename = "cat.png";

        var tex = GameManager.s;//ImageConversion.EncodeToPNG(texture);

        Debug.Log("2:" + GameManager.s);
        Debug.Log("3:" + tex);


        var byteArr = Encoding.UTF8.GetBytes(data);
        
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        //System.String name, System.Byte[] data, System.String fileName, System.String contentType
        formData.Add(new MultipartFormFileSection("imgfile", tex, "cat.png", "image/png")); //tex //byteArr
        //formData.Add(new MultipartFormFileSection("type", type));



        var path = string.Format("{0}/{1}", url, "uploadimage"); //"{0}/{1}"의 의미??

        UnityWebRequest webRequest = UnityWebRequest.Post(path, formData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        var result = webRequest.downloadHandler.text;
        OnCompleteUpload(result);
    }
}