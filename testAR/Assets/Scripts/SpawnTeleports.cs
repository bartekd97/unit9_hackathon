using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleports : MonoBehaviour
{

    [Tooltip("Prefab")]
    public GameObject TeleportToSpawn;
    public List<GameObject> AllSpawnedTeleports;

    public bool checkMiner;

    private GameObject computer;
    private List<Vector3> vertexes;
    private Vector3 center;
    private GameObject plane;
    private Vector3 destination;



    public void StartSpawning()
    {
        bool spawn;
        computer = GameObject.FindGameObjectWithTag("BitcoinMiner") ?? null ;

        if (checkMiner)
        {
            if (computer == null)
            {
                Debug.Log("Komputera nie ma");
                return;
            }
        }

        plane = gameObject.transform.GetChild(0).gameObject;

        if (plane == null)
        {
            Debug.Log("Nie ma dzieci!");
            return;
        }

        vertexes = plane.GetComponent<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>().m_MeshVertices;
        center = plane.GetComponent<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>().m_PlaneCenter;

        for (int i = 0; i < vertexes.Count; i++)
        {
            spawn = true;
            Vector3 avg = new Vector3(vertexes[i].x + vertexes[(i + 1) % vertexes.Count].x + center.x, vertexes[i].y + vertexes[(i + 1) % vertexes.Count].y + center.y, vertexes[i].z + vertexes[(i + 1) % vertexes.Count].z + center.z) / 3;
            destination = avg;

            if (computer != null && checkMiner)
            {
                if (Vector3.Distance(destination, computer.transform.position) < 1 && Vector3.Distance(destination, computer.transform.position) > -1)
                {
                    continue;
                }
            }

            if (AllSpawnedTeleports != null)
            {
                foreach (GameObject GO in AllSpawnedTeleports)
                {
                    if (Vector3.Distance(GO.transform.position, destination) < 0.5f && Vector3.Distance(GO.transform.position, destination) > -0.5f)
                    {
                        spawn = false;
                        continue;
                    }
                }
            }
            else
            {
                Debug.Log("Nie me teleportow jeszcze");
            }

            if(spawn)
                AllSpawnedTeleports.Add(Instantiate(TeleportToSpawn, destination + new Vector3(0.0f, 0.01f, 0.0f), Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f)));
        }
    }
}
