using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [SerializeField] private GameObject tileObj;
    [SerializeField] private GameObject wallUp, wallDown, wallLeft, wallRigjt;

    public HashSet<Vector3Int> spawnedCells = new(); // 해당 cell에 접근해서 오브젝트 생성

    public void OrderFloor(IEnumerable<Vector3Int> positions)
    {
        foreach (var pos in positions)
        {
            InstSingleTile(floorTilemap, pos, tileObj);
        }
    }

    private void InstSingleTile(Tilemap tilemap, Vector3Int pos, GameObject inst)
    {
        Vector3Int cellPos = new Vector3Int(pos.x, pos.y, 0);

        if (!spawnedCells.Add(cellPos))
            return;

        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
        GameObject obj = Instantiate(inst, worldPos, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
        
    }

    public void InstSingleBasicWall(Vector3Int pos)
    {
        if (wallUp != null)
            InstSingleTile(wallTilemap, pos, wallUp);
        else
            Debug.Log("null");
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
}
