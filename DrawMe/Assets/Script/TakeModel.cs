using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TakeModel : MonoBehaviour
{
    string url = "http://openapi.11st.co.kr/openapi/OpenApiService.tmall?key=d31cb5254083f025e9231e22960e7e14&apiCode=ProductInfo&productCode=3575";

    void Start()
    {
        Debug.Log("modelManager 생성");

        //StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.Send();
            if(request.isNetworkError || request.isHttpError)
            {
                Debug.Log("request.Send() 에러");
            }
        }
    }

    public void Test()
    {
        Debug.Log("modelManager.Test() 호출");
    }

}
