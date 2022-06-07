using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public void Awake()
    {
        //이거 수정~~~~~~~~~~~~~~~~~~
        //DontDestroyOnLoad(transform.root.gameObject);
    }
}
