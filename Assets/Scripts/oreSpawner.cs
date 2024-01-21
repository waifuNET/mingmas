using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class oreSpawner : MonoBehaviour
{

    //list
    public List <GameObject> ores = new List <GameObject> ();

    public List <GameObject> spawningOre = new List <GameObject> ();
    
    //diapason
    public GameObject startOreSpawnPos;
    public GameObject finishOreSpawnPos;

    //interval
    public float spawnInterval = 10;
    private float _spawnInterval = 10;


    //maxOreCount
    public int maxOreCount;

    public GameObject oreParent;


    // Start is called before the first frame update
    void Start()
    {
        _spawnInterval = spawnInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawningOre.Count <= maxOreCount)
        {
            spawnInterval -= Time.fixedDeltaTime;
            if (spawnInterval < 0)
            {
                spawnOre();
                spawnInterval = _spawnInterval;
            }
        }
    }

    void spawnOre()
    {
        float randomX = Random.Range(
            startOreSpawnPos.transform.position.x,
            startOreSpawnPos.transform.position.x +
            Vector3.Distance(
            new Vector3(startOreSpawnPos.transform.position.x,0,0),
            new Vector3(finishOreSpawnPos.transform.position.x,0,0)));
        float randomZ = Random.Range(
            startOreSpawnPos.transform.position.z,
            startOreSpawnPos.transform.position.z +
            Vector3.Distance(
            new Vector3(0, 0, startOreSpawnPos.transform.position.z),
            new Vector3(0, 0, finishOreSpawnPos.transform.position.z)));

        GameObject ore = Instantiate(ores[Random.Range(0,ores.Count-1)],new Vector3(randomX,0,randomZ),Quaternion.identity);
        ore.transform.SetParent(oreParent.transform);
        spawningOre.Add(ore);
    }

    public void oreRemove(GameObject ore)
    {
        spawningOre.Remove(ore);
    }
}
