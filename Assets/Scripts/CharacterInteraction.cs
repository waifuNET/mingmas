using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterInteraction : MonoBehaviour
{
    //List<> = new List<>;
    public oreSpawner spawner;

    public NavMeshAgent miner;

    public int currentPocket;
    public int maxPocket = 10;
    public float defaultMiningTime;

    public ore ore;

    GameObject oreTarget;

    float realMinigTime;

    void Start()
    {
        miner = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        miner.isStopped = false;
        if (oreTarget == null)
        {
            
            if (spawner.spawningOre.Count > 0)
            {
                oreTarget = spawner.spawningOre[Random.Range(0, spawner.spawningOre.Count - 1)];
                miner.SetDestination(oreTarget.transform.position);
                //miner.stoppingDistance = oreTarget.GetComponent<ore>().oreSize * 1.3f;
            }
        }
        if (oreTarget != null)
        {
            if (miner.remainingDistance<= oreTarget.GetComponent<ore>().oreSize * 1.3f)
            {
                miner.isStopped = true;
                ore.oreWasting(10); // не работает
            }
            //if (miner.isStopped && currentPocket < maxPocket && oreTarget != null)
            //{
            //    Debug.Log("Mining");
            //}
        }
        
    }

    

    
}
