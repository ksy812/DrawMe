using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    
    public void ChangeSceneBtn()
    {        
        switch (this.gameObject.name)
        {
            case "GameStartBtn":
                SceneManager.LoadScene("SceneStory");
                break;
            case "HowToPlayBtn":
                SceneManager.LoadScene("SceneHowToPlay");
                break;
            case "GoTitleBtn":
                SceneManager.LoadScene("SceneTitleMenu");
                break;
            case "SkipBtn":
                SceneManager.LoadScene("SceneGameCustomer");
                break;
            case "btn_accept(Clone)":
                SceneManager.LoadScene("SceneDraw");
                GameManager_order.customer.SetActive(false);
                GameManager_order.orderbox.SetActive(false);
                break;
            case "BtnComplete":
                SceneManager.LoadScene("SceneGameCustomer");
                GameManager_order.customer.SetActive(true);
                GameManager_order.orderbox.SetActive(true);
                break;
            case "EndBtn":
                SceneManager.LoadScene("SceneTitleMenu");
                break;
            case "StopAndTitleBtn":
                DontDestroy.destroy = true;
                SceneManager.LoadScene("SceneTitleMenu");
                break;
        }

        
    }

    
}
