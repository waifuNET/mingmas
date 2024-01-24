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

    public ore _ore;

    GameObject oreTarget;

    [SerializeField]
    private bool readyToMiningFlag;

    float realMinigTime;

    void Start()
    {
        miner = GetComponent<NavMeshAgent>();
        oreTarget = null;
        readyToMiningFlag = false;
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
                _ore = oreTarget.GetComponent<ore>();
                readyToMiningFlag = true;
                //miner.stoppingDistance = oreTarget.GetComponent<ore>().oreSize * 1.3f;
            }
        }
        if (oreTarget != null && readyToMiningFlag == true)
        {
            Debug.Log(oreTarget);
            if (miner.remainingDistance <= oreTarget.GetComponent<ore>().oreSize * 1.3f && miner.isStopped == false)
            {
                miner.isStopped = true;
                _ore.oreWasting(10);
            }
            
        }
        
    }

    

    
}
