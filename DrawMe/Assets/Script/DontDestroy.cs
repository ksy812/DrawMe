using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static bool destroy;
    private void Awake()
    {
        destroy = false;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Update()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}


