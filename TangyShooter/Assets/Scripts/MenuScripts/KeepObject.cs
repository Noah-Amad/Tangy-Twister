using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
