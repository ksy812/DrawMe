using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class packet
{
    public int errorno;
}
public class uploader : packet
{
    public bool success;
    public string acc;
}

public class ModelManager : MonoBehaviour
{
    string url = "http://localhost:3000";
    public float accuracy = -1f;
    public static string type = null;

    private void Start()
    {
        //Debug.Log("ModelManager Start() call");
    }

    public void TakeModel()
    {
        type = "dog"; //crab
        StartCoroutine(Upload(result =>
        {
            var responseResult = JsonConvert.DeserializeObject<uploader>(result);
            Debug.Log("Upload 성공여부 : " + responseResult.success);
            Debug.Log("acc 반환값 : " + responseResult.acc);
            Customer.satisfaction = float.Parse(responseResult.acc);
            Debug.Log("Customer.satisfaction : " + Customer.satisfaction);
            if (responseResult.success)
            {
                SceneManager.LoadScene("SceneGameCustomer");
                GameManager_order.customer.SetActive(true);
                GameManager_order.orderbox.SetActive(true);
            }
        }));
        Debug.Log("TakeModel()");
        //return accuracy;
    }

    private IEnumerator Upload(System.Action<string> OnCompleteUpload)
    {
        string filename = GameManager.fileName;

        var tex = GameManager.screenShotPNG;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        //System.String name, System.Byte[] data, System.String fileName, System.String contentType
        formData.Add(new MultipartFormFileSection("imgfile", tex, filename, "image/png"));
        formData.Add(new MultipartFormDataSection("customer", type));

        var path = string.Format("{0}/{1}", url, "uploadimage"); //"{0}/{1}"의 의미??

        UnityWebRequest webRequest = UnityWebRequest.Post(path, formData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        var result = webRequest.downloadHandler.text;
        OnCompleteUpload(result);
    }
}