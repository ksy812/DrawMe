using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "GameStartBtn":
                SceneManager.LoadScene("SceneGameCustomer");
                break;

            case "HowToPlayBtn":
                SceneManager.LoadScene("SceneHowToPlay");
                break;
            case "GoTitleBtn":
                SceneManager.LoadScene("SceneTitleMenu");
                break;
        }
    }
    
}
