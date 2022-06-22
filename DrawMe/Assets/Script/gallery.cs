using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gallery : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject imgPrefab;
    public List<RectTransform> images=new List<RectTransform>();
    void Start()
    {
        Texture2D[] filename = Resources.LoadAll<Texture2D>("ScreenShots/");
        scrollRect=GetComponent<ScrollRect>();
        foreach (var  f in filename)
        {
            imgPrefab.GetComponent<Image>().sprite = Sprite.Create(f, new Rect(0, 0, f.width, f.height), new Vector2(0.5f, 0.5f));
            var newImg=Instantiate(imgPrefab,scrollRect.content).GetComponent<RectTransform>();
            //Sprite sprite = Sprite.Create(f, new Rect(0, 0, f.width, f.height), new Vector2(0.5f, 0.5f));
            images.Add(newImg);
        }

    }

}
