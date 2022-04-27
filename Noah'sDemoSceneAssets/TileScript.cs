using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    public Tilemap map;
    public Tilemap transparentMap;
    public Camera mainCamera;
    private Vector3Int[] selectedAxis = new Vector3Int[2];
    private int index = 0;
    private TileBase[] t = new TileBase[2];
    void Awake()
    {
        mainCamera = Camera.main;;
        int ind = 0;
        foreach (var position in map.cellBounds.allPositionsWithin) {
            string name = map.GetTile(position).name;
            if (name == "sampleTile")
            {
                map.SetColliderType(position, Tile.ColliderType.None);
            }
        }
        Debug.Log(ind);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickCellPosition = map.WorldToCell(clickWorldPosition);
                if (map.GetTile(clickCellPosition) || transparentMap.GetTile(clickCellPosition))
                {
                    t[index] = map.GetTile(clickCellPosition);
                    selectedAxis[index] = clickCellPosition;
                    index++;
                }

                Debug.Log(clickCellPosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (index == 4)
            {
                Debug.Log("SUCCESS");
                int count = 0;

                for (int i = 0; i < index; i++)
                {
                    switch (count)
                    {
                        case 0:
                            map.SetTile(selectedAxis[i], t[1]);
                            break;
                        case 1:
                            map.SetTile(selectedAxis[i], t[0]);
                            break;
                    }

                    //map.SetTile(selectedAxis[i], t[count]);
                    count++;

                }

                selectedAxis = new Vector3Int[4];
                index = 0;
                t = new TileBase[4];
                Debug.Log("Completed");
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
    }
}
