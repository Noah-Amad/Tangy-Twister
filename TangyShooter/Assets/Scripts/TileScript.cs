using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.WSA;

public class TileScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;
    public int number2;
    
    void Awake()
    {
        targetPosition = transform.position;
        correctPosition = targetPosition;
        _sprite = GetComponent<SpriteRenderer>();
        number2 = number;
    }

    
    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        if (targetPosition == correctPosition || number == number2)
        {
            _sprite.color = Color.green;
        }
        else
        {
            _sprite.color = Color.red;
        }
    }
}
