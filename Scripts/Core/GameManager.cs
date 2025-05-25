using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject droneRedPrefab, droneBluePrefab;
    public Transform redSpawn, blueSpawn;
    public BaseStation2D baseRed, baseBlue;
    [HideInInspector] public int perFaction = 1;
    [HideInInspector] public float droneSpeed = 10f;
    [HideInInspector] public bool drawPaths;
    List<DroneAI2D> drones = new List<DroneAI2D>();

    public void SpawnDrones()
    {
        int need = perFaction * 2;
        while (drones.Count > need)
        {
            var d = drones[drones.Count - 1];
            drones.RemoveAt(drones.Count - 1);
            Destroy(d.gameObject);
        }
        int rc = 0, bc = 0;
        foreach (var d in drones)
        {
            if (d.home == baseRed) rc++;
            else if (d.home == baseBlue) bc++;
        }
        while (rc < perFaction)
        {
            var r = Instantiate(droneRedPrefab, redSpawn.position, Quaternion.identity);
            r.tag = "Drone";
            var ai = r.GetComponent<DroneAI2D>(); ai.Init(baseRed, droneSpeed, drawPaths);
            drones.Add(ai);
            rc++;
        }
        while (bc < perFaction)
        {
            var b = Instantiate(droneBluePrefab, blueSpawn.position, Quaternion.identity);
            b.tag = "Drone";
            var ai = b.GetComponent<DroneAI2D>(); ai.Init(baseBlue, droneSpeed, drawPaths);
            drones.Add(ai);
            bc++;
        }
        foreach (var d in drones)
        {
            d.speed = droneSpeed;
            d.drawPath = drawPaths;
        }
    }
}