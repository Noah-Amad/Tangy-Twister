using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manualSwitch : MonoBehaviour
{
    public GameObject go0;
    public GameObject go1;
    public GameObject go2;
    public GameObject go3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 g0 = go0.transform.position;
            Vector3 g1 = go1.transform.position;
            Vector3 g2 = go2.transform.position;
            Vector3 g3 = go3.transform.position;

            go0.transform.position = g1;
            go1.transform.position = g2;
            go2.transform.position = g3;
            go3.transform.position = g0;
        }
    }
}
