using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [SerializeField] private GameObject floorObj;
    [SerializeField] private GameObject wallObj;


    public void OrderFloor(IEnumerable<Vector2Int> positions)
    {
        InstFloor(positions, floorTilemap, floorObj);
    }

    private void InstFloor(IEnumerable<Vector2Int> positions, Tilemap tilemap, GameObject inst)
    {
        foreach (var pos in positions)
        {
            InstSingleTile(tilemap, pos, inst);
        }
    }

    private void InstSingleTile(Tilemap tilemap, Vector2Int pos, GameObject inst)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)pos);
        GameObject obj = Instantiate(inst, tilePos,Quaternion.identity);
        obj.transform.parent = gameObject.transform;
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();

        //ClearObj();
    }

    public void ClearObj()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i));
        }
    }
}
