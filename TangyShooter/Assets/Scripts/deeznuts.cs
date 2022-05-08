using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class TileMapPractice : MonoBehaviour
{
    public Tilemap map;
    public TileBase tileTemp0;
    public TileBase tileTemp1;
    public TileBase tileTemp2;
    public TileBase tileTemp3;
    public GameObject redTileSingular;
    public GameObject sampleTile;
    public List<GameObject> listOfTiles = new List<GameObject>();

    public float ro0;
    public float ro1;
    public float ro2;
    public float ro3;
    private List<float> rotaList = new List<float>();

    public Camera mainCamera;
    private Vector3Int[] selectedAxis = new Vector3Int[2];
    private int index = 0;
    private TileBase[] t = new TileBase[2];
    List<Vector3Int> v3i = new List<Vector3Int>();
    public int count = 0;
    public float[] indRot;
    private Vector3 mouseStart;
    private Vector3 mouseEnd;
    public bool spintrue = false;
    
    
    
    void Awake()
    {
        mainCamera = Camera.main;;
        
        foreach (var position in map.cellBounds.allPositionsWithin) {
            string name = map.GetTile(position).name;
            if (name == "sampleTile")
            {
                map.SetColliderType(position, Tile.ColliderType.None);
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseEnd = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseStart.x = MathF.Floor(mouseStart.x);
            mouseStart.y = MathF.Floor(mouseStart.y);
            mouseEnd.x = MathF.Floor(mouseEnd.x);
            mouseEnd.y = MathF.Floor(mouseEnd.y);
            Vector3Int ms = Vector3Int.RoundToInt(mouseStart);
            Vector3Int me = Vector3Int.RoundToInt(mouseEnd);
            ms.z = 0;
            me.z = 0;
            
            foreach (var position in map.cellBounds.allPositionsWithin)
            {
                if (ms.x <= position.x && ms.y >= position.y && me.x >= position.x && me.y <= position.y)
                {
                    v3i.Add(position);
                    rotaList.Add(map.GetTransformMatrix(position).GetR().eulerAngles.z);
                }
            }
            
            
            spintrue = true;
            if (v3i.Count != 4)
            {
                v3i = new List<Vector3Int>();
            }
            else
            {
                tileTemp0 = map.GetTile(v3i[0]);
                tileTemp1 = map.GetTile(v3i[1]);
                tileTemp2 = map.GetTile(v3i[2]);
                tileTemp3 = map.GetTile(v3i[3]);
                for (int i = 0; i < 4; i++)
                {
                    if (map.GetTile(v3i[i]).name == "redTileSingular")
                    {
                        listOfTiles.Add(redTileSingular);
                    }
                    else if (map.GetTile(v3i[i]).name == "sampleTile")
                    {
                        listOfTiles.Add(sampleTile);
                    }
                } 
                
                GameObject x = Instantiate(listOfTiles[0], v3i[0], Quaternion.identity);
                GameObject y = Instantiate(listOfTiles[1], v3i[1], Quaternion.identity);
                GameObject z = Instantiate(listOfTiles[2], v3i[2], Quaternion.identity);
                GameObject v = Instantiate(listOfTiles[3], v3i[3], Quaternion.identity);
                
                Debug.Log(v3i[0] + " ||| " + v3i[1] + " ||| " + v3i[2] + " ||| " + v3i[3]);
                Debug.Log(x.transform.position + " ||| " + y.transform.position + " ||| " + z.transform.position +
                          " ||| " + v.transform.position);
                
                map.SetTile(v3i[0], null);
                map.SetTile(v3i[1], null);
                map.SetTile(v3i[2], null);
                map.SetTile(v3i[3], null);
            }
        }
        
        if (spintrue && v3i.Count == 4)
        {
            float rota0 = rotaList[0];
            float rota1 = rotaList[1];
            float rota2 = rotaList[2];
            float rota3 = rotaList[3];
            Matrix4x4 matrix0 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota0 - 1f), Vector3.one);
            Matrix4x4 matrix1 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota1 - 1f), Vector3.one);
            Matrix4x4 matrix2 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota2 - 1f), Vector3.one);
            Matrix4x4 matrix3 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota3 - 1f), Vector3.one);
            map.SetTransformMatrix(v3i[0], matrix0);
            map.SetTransformMatrix(v3i[1], matrix1);
            map.SetTransformMatrix(v3i[2], matrix2);
            map.SetTransformMatrix(v3i[3], matrix3);

            count += 1;
            if (count == 90)
            {
                tileRotate(v3i, rotaList);
                v3i = new List<Vector3Int>();
                count = 0;
                spintrue = false;
                rotaList = new List<float>();
            }
        }
    }
    
        
    void tileRotate(List<Vector3Int> v3int, List<float> rlist)
    {
        
        TileBase temp0 = map.GetTile(v3int[0]);
        TileBase temp1 = map.GetTile(v3int[1]);
        TileBase temp2 = map.GetTile(v3int[2]);
        TileBase temp3 = map.GetTile(v3int[3]);
        
        map.SetTile(v3int[0], tileTemp1);
        map.SetTile(v3int[1], tileTemp3);
        map.SetTile(v3int[2], tileTemp0);
        map.SetTile(v3int[3], tileTemp2);

        float rota0 = rlist[0] % 360;
        float rota1 = rlist[1] % 360;
        float rota2 = rlist[2] % 360;
        float rota3 = rlist[3] % 360;
        
        Matrix4x4 matrix0 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota0 - 90f), Vector3.one);
        Matrix4x4 matrix1 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota1 - 90f), Vector3.one);
        Matrix4x4 matrix2 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota2 - 90f), Vector3.one);
        Matrix4x4 matrix3 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota3 - 90f), Vector3.one);
            
        Destroy(listOfTiles[0]);
        Destroy(listOfTiles[1]);
        Destroy(listOfTiles[2]);
        Destroy(listOfTiles[3]);
        
        map.SetTransformMatrix(v3i[0], matrix0);
        map.SetTransformMatrix(v3i[1], matrix1);
        map.SetTransformMatrix(v3i[2], matrix2);
        map.SetTransformMatrix(v3i[3], matrix3);

        foreach (var position in map.cellBounds.allPositionsWithin)
        {
            string name = map.GetTile(position).name;
            if (name == "sampleTile")
            {
                map.SetColliderType(position, Tile.ColliderType.None);
            }
        }
    }
}
