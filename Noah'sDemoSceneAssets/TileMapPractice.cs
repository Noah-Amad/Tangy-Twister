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
    public Tilemap transparentMap;
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
                }
            }

            spintrue = true;
            if (v3i.Count != 4)
            {
                v3i = new List<Vector3Int>();
            }
        }
        
        if (spintrue && v3i.Count == 4)
        {
            float rota0 = map.GetTransformMatrix(v3i[0]).GetR().eulerAngles.z;
            float rota1 = map.GetTransformMatrix(v3i[1]).GetR().eulerAngles.z;
            float rota2 = map.GetTransformMatrix(v3i[2]).GetR().eulerAngles.z;
            float rota3 = map.GetTransformMatrix(v3i[3]).GetR().eulerAngles.z;
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
                tileRotate(v3i);
                v3i = new List<Vector3Int>();
                count = 0;
                spintrue = false;
            }
        }
    }
    
        
    void tileRotate(List<Vector3Int> v3int)
    {
        
        TileBase temp0 = map.GetTile(v3int[0]);
        TileBase temp1 = map.GetTile(v3int[1]);
        TileBase temp2 = map.GetTile(v3int[2]);
        TileBase temp3 = map.GetTile(v3int[3]);
        
        
        map.SetTile(v3int[0], temp1);
        map.SetTile(v3int[1], temp3);
        map.SetTile(v3int[2], temp0);
        map.SetTile(v3int[3], temp2);

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