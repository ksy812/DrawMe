using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Globalization;

public class packet
{
    public int errorno;
}
public class uploader : packet
{
    public bool success { get; set; }
    public string acc { get; set; }
}

public class ModelManager : MonoBehaviour
{
    //readonly string url = "http://localhost:3000";
    readonly string url = "http://drawme.emirim.kr:3010";
    public float accuracy = -1f;
    public static string type = null;

    public Text text;
    private void Start()
    {
        //Debug.Log("ModelManager Start() call");
    }

    public void TakeModel()
    {
        type = Customer.curr_customer;//"dog"; //crab
        //Money.amount =-1;
        StartCoroutine(Upload(result =>
        {
            //Money.amount = -2;
            //text.text = "responseResult";
            //Debug.Log("===BEFORE!!! var responseResult===");
            //Debug.Log(result);
            var responseResult = JsonConvert.DeserializeObject<uploader>(result); //
            Debug.Log("Upload 성공여부 : " + responseResult.success);
            Debug.Log("acc 반환값 : " + responseResult.acc);
            //text.text = "responseResult";
            //Money.amount = -2.5f;
            Customer.satisfaction = float.Parse(responseResult.acc, CultureInfo.InvariantCulture.NumberFormat) * 100;
            Debug.Log("Customer.satisfaction : " + Customer.satisfaction);
            //Money.amount = -3;
            if (responseResult.success)
            {
                //Money.amount = -4;
                
                SceneManager.LoadScene("SceneGameCustomer");
                
                GameManager_order.customer.SetActive(true);
                GameManager_order.orderbox.SetActive(true);
            
            }
            else
            {
                Money.amount = -5;
            }
        }));
        Debug.Log("TakeModel()");
    }

    private IEnumerator Upload(System.Action<string> OnCompleteUpload)
    {
        string filename = GameManager.fileName;

        var tex = GameManager.screenShotPNG;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        //System.String name, System.Byte[] data, System.String fileName, System.String contentType
        formData.Add(new MultipartFormFileSection("imgfile", tex, filename, "image/png"));
        formData.Add(new MultipartFormDataSection("customer", type));

        var path = string.Format("{0}/{1}", url, "uploadimage");

        UnityWebRequest webRequest = UnityWebRequest.Post(path, formData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        var result = webRequest.downloadHandler.text;
        Debug.Log("===result===");
        Debug.Log("****" + result.ToString());
        OnCompleteUpload(result); //
        //Debug.Log("===AFTER!!! OnCompleteUpload() call===");
    }
}