using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel : MonoBehaviour
{
    public Text text;

    string[] storys = { 
                "나뭇가지 둥지에서 살고 있는지 어언 5년째...",
                "이 둥지는 너무 차갑고 딱딱하다..",
                "이젠 이보다 좋은 집에서 윤택하게 살고 싶다!",
                "내가 가지고 있는건 그림그리는 재능뿐...",
                "그림을 팔아서 집 살 돈을 모아야겠어!"
                        };
    int i;

    private void Start()
    {
        i = 0;
        text.text = storys[i++];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (i >= storys.Length)
                SceneManager.LoadScene("SceneGameCustomer");
            else
                text.text = storys[i++];
        }
    }
}
