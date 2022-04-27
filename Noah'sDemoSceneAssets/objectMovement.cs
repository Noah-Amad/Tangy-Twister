using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int movSpeed = 7;

    public int jumpS = 7;
    // Use this for initialization
    void Start () {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * jumpS * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * jumpS * Time.deltaTime);
        }
    }
}
