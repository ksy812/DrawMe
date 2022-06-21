using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gallery : MonoBehaviour
{
    public Image[] image;
    void Start()
    {

        GameObject[] filename = Resources.LoadAll<GameObject>("ScreenShots/");
        int i = 0;
        foreach (var img in image)
        {
            Texture2D texture = Resources.Load<Texture2D>("ScreenShots/"+30+"_20_"+i);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            img.sprite = sprite;
            i++;
        }

    }

}
