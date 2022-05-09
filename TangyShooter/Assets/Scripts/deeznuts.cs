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

public class deeznuts : MonoBehaviour
{
    public Tilemap map;
    public GameObject DummyTile0;
    public GameObject DummyTile1;
    public GameObject DummyTile2;
    public GameObject DummyTile3;
    public GameObject Fruit;

    public TileBase tileTemp0;
    public TileBase tileTemp1;
    public TileBase tileTemp2;
    public TileBase tileTemp3;
    public GameObject SolidTile;
    public GameObject TransparentTile;
    public GameObject SlopedTile;
    public List<GameObject> listOfTiles = new List<GameObject>();
    public int FruitTransform;
    public bool FruitRotateBool;
    private List<float> rotaList = new List<float>();

    public Camera mainCamera;
    List<Vector3Int> v3i = new List<Vector3Int>();
    public float count = 0;
    private Vector3 mouseStart;
    private Vector3 mouseEnd;
    public bool spintrue = false;
    public bool inputTrue = true;



    void Awake()
    {
        mainCamera = Camera.main;
        
        foreach (var position in map.cellBounds.allPositionsWithin) {
            if (map.GetTile(position) == null)
            {
                continue;
            }
            string name = map.GetTile(position).name;
            if (name == "TransparentTile")
            {
                map.SetColliderType(position, Tile.ColliderType.None);
                
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inputTrue)
        {
            mouseStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && inputTrue)
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
                if (map.GetTile(position) == null)
                {
                    continue;
                }
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
                    if (map.GetTile(v3i[i]).name == "SolidTile")
                    {
                        listOfTiles.Add(SolidTile);
                    }
                    else if (map.GetTile(v3i[i]).name == "TransparentTile")
                    {
                        listOfTiles.Add(TransparentTile);
                    }
                    else if (map.GetTile(v3i[i]).name == "SlopedTile")
                    {
                        listOfTiles.Add(SlopedTile);
                    }
                } 
                
                DummyTile0.SetActive(true);
                DummyTile1.SetActive(true);
                DummyTile2.SetActive(true);
                DummyTile3.SetActive(true);
                
                DummyTile0.GetComponent<SpriteRenderer>().sprite = listOfTiles[0].GetComponent<SpriteRenderer>().sprite;
                DummyTile1.GetComponent<SpriteRenderer>().sprite = listOfTiles[1].GetComponent<SpriteRenderer>().sprite;
                DummyTile2.GetComponent<SpriteRenderer>().sprite = listOfTiles[2].GetComponent<SpriteRenderer>().sprite;
                DummyTile3.GetComponent<SpriteRenderer>().sprite = listOfTiles[3].GetComponent<SpriteRenderer>().sprite;
                
                Vector3 pos0 = map.CellToWorld(v3i[0]);
                Vector3 pos1 = map.CellToWorld(v3i[1]);
                Vector3 pos2 = map.CellToWorld(v3i[2]);
                Vector3 pos3 = map.CellToWorld(v3i[3]);
                pos0.x += 0.5f;
                pos0.y += 0.5f;
                pos1.x += 0.5f;
                pos1.y += 0.5f;
                pos2.x += 0.5f;
                pos2.y += 0.5f;
                pos3.x += 0.5f;
                pos3.y += 0.5f;
                
                DummyTile0.transform.SetPositionAndRotation(pos0, Quaternion.identity);
                DummyTile1.transform.SetPositionAndRotation(pos1, Quaternion.identity);
                DummyTile2.transform.SetPositionAndRotation(pos2, Quaternion.identity);
                DummyTile3.transform.SetPositionAndRotation(pos3, Quaternion.identity);
                
                map.SetTile(v3i[0], null);
                map.SetTile(v3i[1], null);
                map.SetTile(v3i[2], null);
                map.SetTile(v3i[3], null);
                
                if (map.WorldToCell(Fruit.transform.position) == v3i[0])
                {
                    FruitTransform = 0;
                    FruitRotateBool = true;
                }
                else if (map.WorldToCell(Fruit.transform.position) == v3i[1])
                {
                    FruitTransform = 1;
                    FruitRotateBool = true;
                }
                else if (map.WorldToCell(Fruit.transform.position) == v3i[2])
                {
                    FruitTransform = 2;
                    FruitRotateBool = true;
                }
                else if (map.WorldToCell(Fruit.transform.position) == v3i[3])
                {
                    FruitTransform = 3;
                    FruitRotateBool = true;
                }
            }
        }
        
        if (spintrue && v3i.Count == 4)
        {
            inputTrue = false;
            rotaList[0] -= 0.5f;
            rotaList[1] -= 0.5f;
            rotaList[2] -= 0.5f;
            rotaList[3] -= 0.5f;
            float offset = 0.0055f;
            
            Vector3 dummy0 = DummyTile0.transform.position;
            dummy0.Set(dummy0.x, dummy0.y + offset, dummy0.z);
            
            Vector3 dummy1 = DummyTile1.transform.position;
            dummy1.Set(dummy1.x - offset, dummy1.y, dummy1.z);
            
            Vector3 dummy2 = DummyTile2.transform.position;
            dummy2.Set(dummy2.x + offset, dummy2.y, dummy2.z);
            
            Vector3 dummy3 = DummyTile3.transform.position;
            dummy3.Set(dummy3.x, dummy3.y - offset, dummy3.z);
            
            DummyTile0.transform.SetPositionAndRotation(dummy0, Quaternion.Euler(0f, 0f, rotaList[0]));
            DummyTile1.transform.SetPositionAndRotation(dummy1, Quaternion.Euler(0f, 0f, rotaList[1]));
            DummyTile2.transform.SetPositionAndRotation(dummy2, Quaternion.Euler(0f, 0f, rotaList[2]));
            DummyTile3.transform.SetPositionAndRotation(dummy3, Quaternion.Euler(0f, 0f, rotaList[3]));
            if (FruitRotateBool)
            {
                switch (FruitTransform)
                {
                    case 0: Fruit.transform.SetPositionAndRotation(dummy0, Quaternion.Euler(0f, 0f, 0f)); break;
                    case 1: Fruit.transform.SetPositionAndRotation(dummy1, Quaternion.Euler(0f, 0f, 0f)); break;
                    case 2: Fruit.transform.SetPositionAndRotation(dummy2, Quaternion.Euler(0f, 0f, 0f)); break;
                    case 3: Fruit.transform.SetPositionAndRotation(dummy3, Quaternion.Euler(0f, 0f, 0f)); break;
                }
            }

            count += 0.5f;
            if (count >= 90)
            {
                tileRotate(v3i, rotaList);
                v3i = new List<Vector3Int>();
                count = 0;
                spintrue = false;
                rotaList = new List<float>();
                listOfTiles = new List<GameObject>();
                FruitRotateBool = false;
                inputTrue = true;
            }
        }
    }
    
        
    void tileRotate(List<Vector3Int> v3int, List<float> rlist)
    {
        
        map.SetTile(v3int[0], tileTemp1);
        map.SetTile(v3int[1], tileTemp3);
        map.SetTile(v3int[2], tileTemp0);
        map.SetTile(v3int[3], tileTemp2);

        float rota0 = rlist[1] % 360;
        float rota1 = rlist[3] % 360;
        float rota2 = rlist[0] % 360;
        float rota3 = rlist[2] % 360;
        
        Matrix4x4 matrix0 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota0), Vector3.one);
        Matrix4x4 matrix1 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota1), Vector3.one);
        Matrix4x4 matrix2 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota2), Vector3.one);
        Matrix4x4 matrix3 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rota3), Vector3.one);

        DummyTile0.SetActive(false);
        DummyTile1.SetActive(false);
        DummyTile2.SetActive(false);
        DummyTile3.SetActive(false);
        
        map.SetTransformMatrix(v3i[0], matrix0);
        map.SetTransformMatrix(v3i[1], matrix1);
        map.SetTransformMatrix(v3i[2], matrix2);
        map.SetTransformMatrix(v3i[3], matrix3);

        foreach (var position in map.cellBounds.allPositionsWithin)
        {
            if (map.GetTile(position) == null)
            {
                continue;
            }
            string name = map.GetTile(position).name;
            if (name == "TransparentTile")
            {
                map.SetColliderType(position, Tile.ColliderType.None);
            }
        }
    }
}
