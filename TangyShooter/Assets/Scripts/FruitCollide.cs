using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if(other.gameObject.tag == "Fruit")
        {
            Destroy(other.gameObject);
            // Add win animation
            // Add win screen
        }
        if(other.gameObject.tag == "Bad")
        {
            Destroy(other.gameObject);
            // Add lose animation
            // Add defeat screen
        }
    }
}
