using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleports : MonoBehaviour
{

    [Tooltip("Prefab")]
    public GameObject TeleportToSpawn;
    public List<GameObject> AllSpawnedTeleports;

    private GameObject computer;
    private List<Vector3> vertexes;
    private Vector3 center;
    private GameObject plane;
    private Vector3 destination;

    public void StartSpawning()
    {
        computer = GameObject.FindGameObjectWithTag("computer");

        if (computer == null)
        {
            Debug.Log("Komputera nie ma");
            return;
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
            Vector3 avg = new Vector3(vertexes[i].x + vertexes[(i + 1) % vertexes.Count].x + center.x, vertexes[i].y + vertexes[(i + 1) % vertexes.Count].y + center.y, vertexes[i].z + vertexes[(i + 1) % vertexes.Count].z + center.z) / 3;
            destination = avg;


            if (Vector3.Distance(destination, computer.transform.position) < 1 && Vector3.Distance(destination, computer.transform.position) > -1)
            {
                continue;
            }

            if (AllSpawnedTeleports != null)
            {
                foreach (GameObject GO in AllSpawnedTeleports)
                {
                    if (Vector3.Distance(GO.transform.position, destination) < 1 && Vector3.Distance(GO.transform.position, destination) > -1)
                    {
                        continue;
                    }
                }
            }
            else
            {
                Debug.Log("Nie me teleportow jeszcze");
            }

            AllSpawnedTeleports.Add(Instantiate(TeleportToSpawn, destination, Quaternion.identity));
        }
    }
}
