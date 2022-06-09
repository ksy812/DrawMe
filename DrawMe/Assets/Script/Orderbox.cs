using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Orderbox : MonoBehaviour
{
    public Text text;
    
    public void setText(string o)
    {
        text.text = o;
    }

    void Destroy()
    {
                
    }
}
