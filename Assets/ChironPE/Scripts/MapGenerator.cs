using UnityEngine;

namespace ChironPE
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject groundPrefab = null;
        [SerializeField]
        private GameObject indestructibleExteriorWallPrefab = null;
        [SerializeField]
        private Transform levelParent = null;

        [SerializeField]
        private Vector3Int mapSize = new Vector3Int(7, 4, 7);
        [SerializeField]
        private float groundYPosition = -1.5f;
        [SerializeField]
        private float wallStartingYPosition = -1.5f;

        private void Awake()
        {
            if (groundPrefab == null)
            {
                Debug.LogErrorFormat("{0} is missing; the map cannot be generated!", nameof(groundPrefab));
                return;
            }

            if (indestructibleExteriorWallPrefab == null)
            {
                Debug.LogErrorFormat("{0} is missing; the map cannot be generated!", nameof(indestructibleExteriorWallPrefab));
                return;
            }

            GenerateMap();
        }

        private void OnValidate()
        {
            mapSize.x = Mathf.Clamp(mapSize.x, 1, mapSize.x);
            mapSize.y = Mathf.Clamp(mapSize.y, 1, mapSize.y);
            mapSize.z = Mathf.Clamp(mapSize.z, 1, mapSize.z);
        }

        private void GenerateMap()
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int z = 0; z < mapSize.z; z++)
                {
                    GameObject spawnedObject = Instantiate(groundPrefab, new Vector3(x, groundYPosition, z), Quaternion.identity, levelParent);
                    spawnedObject.name = $"Map Ground ({x}, {z})";
                }
            }

            for (int y = 0; y < mapSize.y; y++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    GameObject spawnedLowerObject = Instantiate(indestructibleExteriorWallPrefab, new Vector3(x, wallStartingYPosition + y, -1), Quaternion.identity, levelParent);
                    spawnedLowerObject.name = $"Map Exterior Wall ({x}, {y}, {-1})";

                    GameObject spawnedUpperObject = Instantiate(indestructibleExteriorWallPrefab, new Vector3(x, wallStartingYPosition + y, mapSize.z), Quaternion.identity, levelParent);
                    spawnedUpperObject.name = $"Map Exterior Wall ({x}, {y}, {mapSize.y})";
                }

                for (int z = 0; z < mapSize.z; z++)
                {
                    GameObject spawnedLeftObject = Instantiate(indestructibleExteriorWallPrefab, new Vector3(-1, wallStartingYPosition + y, z), Quaternion.identity, levelParent);
                    spawnedLeftObject.name = $"Map Exterior Wall ({-1}, {y}, {z})";

                    GameObject spawnedRightObject = Instantiate(indestructibleExteriorWallPrefab, new Vector3(mapSize.x, wallStartingYPosition + y, z), Quaternion.identity, levelParent);
                    spawnedRightObject.name = $"Map Exterior Wall ({mapSize.x}, {y}, {z})";
                }                
            }
        }
    }
}
