using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private Grid tileGrid;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    private Transform roofTrans;
    [SerializeField] private GameObject tileObj;

    [SerializeField] private GameObject wallTop, wallBottom, wallSideLeft, wallSideRight, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight, wallDiagonalCornerDownRight,
        wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    
    public HashSet<Vector3Int> spawnedCells = new(); // 해당 cell에 접근해서 오브젝트 생성

    public void OrderFloor(IEnumerable<Vector3Int> positions)
    {
        HashSet<Vector3Int> roofPos = new();
        roofTrans = FindChildByName(wallTop.transform, "Roof");

        Vector3 baseWorld = wallTop.transform.position;
        Vector3 roofWorld = roofTrans.position;

        Vector3Int roofVec = tileGrid.WorldToCell(roofWorld) - tileGrid.WorldToCell(baseWorld);

        foreach (var pos in positions)
        {
            roofPos.Add(pos);
            InstSingleTile(floorTilemap, pos, tileObj);
            InstSingleTile(floorTilemap, pos + roofVec, tileObj);
        }

    }

    Transform FindChildByName(Transform root, string targetName, bool includeInactive = true)
    {
        foreach (var t in root.GetComponentsInChildren<Transform>(includeInactive))
        {
            if (t.name == targetName)
                return t;
        }
        return null;
    }

    private void InstSingleTile(Tilemap tilemap, Vector3Int pos, GameObject inst)
    {
        Vector3Int cellPos = new Vector3Int(pos.x, pos.y, pos.z);

        if (spawnedCells.Contains(cellPos))
            return;

        spawnedCells.Add(cellPos);

        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

        
        GameObject obj = Instantiate(inst, worldPos, Quaternion.identity, transform);

    }

    public void InstSingleBasicWall(Vector3Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        GameObject obj = null;

        if (WallType.wallTop.Contains(typeAsInt))
        {
            obj = wallTop;
        }
        else if (WallType.wallSideRight.Contains(typeAsInt))
        {
            obj = wallSideRight;
        }
        else if (WallType.wallSideLeft.Contains(typeAsInt))
        {
            obj = wallSideLeft;
        }
        else if (WallType.wallFull.Contains(typeAsInt))
        {
            obj = wallFull;
        }
        else if (WallType.wallBottom.Contains(typeAsInt))
        {
            obj = wallBottom;
        }


        if (obj != null)
            InstSingleTile(wallTilemap, pos, obj);
    }


    public void InstSingleCornerWall(Vector3Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        GameObject obj = null;

        if (WallType.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            obj = wallInnerCornerDownLeft;
        }
        else if (WallType.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            obj = wallInnerCornerDownRight;
        }
        else if (WallType.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            obj = wallDiagonalCornerDownLeft;
        }
        else if (WallType.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            obj = wallDiagonalCornerDownRight;
        }
        else if (WallType.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            obj = wallDiagonalCornerUpLeft;
        }
        else if (WallType.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            obj = wallDiagonalCornerUpRight;
        }
        else if (WallType.wallFullEightDirections.Contains(typeAsInt))
        {
            obj = wallFull;
        }
        else if (WallType.wallBottmEightDirections.Contains(typeAsInt))
        {
            obj = wallBottom;
        }

        if (obj != null)
            InstSingleTile(wallTilemap,pos,obj);
    }



    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();

        ClearObj();
        spawnedCells.Clear();
    }

    public void ClearObj()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i).gameObject;

            if (UnityEngine.Application.isPlaying)
                Destroy(child);
            else
                DestroyImmediate(child); 
        }
    }

    public Vector3 GridSize()
    {
        return tileGrid.cellSize;
    }
}
