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

    private void Start()
    {
        Debug.Log("Take Model Start() call");
    }

    public void Btn()
    {
        var reqUploadImage = new req_uploadimage();
        //sampleImage.name = "imgfile";
        reqUploadImage.name = "imgfile";
        var json = JsonConvert.SerializeObject(reqUploadImage);

        StartCoroutine(Upload(json, (result) =>
        {
            var responseResult = JsonConvert.DeserializeObject<res_uploadimage>(result);
            Debug.Log("성공여부 : " + responseResult.success);
        }));
    }
    private IEnumerator Upload(string data, System.Action<string> OnCompleteUpload)
    {
        Texture2D texture = null;
        string path1 = "cat.png";
        texture = Resources.Load(path1, typeof(Texture2D)) as Texture2D;
        var tex = ImageConversion.EncodeToPNG(texture);

        var byteArr = Encoding.UTF8.GetBytes(data);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        //System.String name, System.Byte[] data, System.String fileName, System.String contentType
        formData.Add(new MultipartFormFileSection("imgfile", tex, "cat.png", "image/png")); //tex

        var path = string.Format("{0}/{1}", url, "uploadimage");
        UnityWebRequest webRequest = UnityWebRequest.Post(path, formData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        var result = webRequest.downloadHandler.text;
        OnCompleteUpload(result);
    }
}