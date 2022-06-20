using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength = 30;
    public int numberOfTiles = 5;
    public float zSpawn = 0;
    public Transform playerTransform;
    private int previousIndex;
    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z -35 >= zSpawn - (numberOfTiles * tileLength))
        {
            int index = Random.Range(0, tilePrefabs.Length);
            while (index == previousIndex)
                index = Random.Range(0, tilePrefabs.Length);

            DeleteTile();
            SpawnTile(index);
        }

    }

    public void SpawnTile(int index = 0)
    {
        GameObject tile = Instantiate(tilePrefabs[index], transform.forward * zSpawn, playerTransform.rotation);

        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
