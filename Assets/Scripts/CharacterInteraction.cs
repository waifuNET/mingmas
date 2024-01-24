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
    public int maxPocket = 1000;
    public float defaultMiningTime;

    public ore _ore;

    GameObject oreTarget;
    public GameObject Dumper;

    Vector3 storageTarget;

    [SerializeField]
    private bool readyToMiningFlag;

    float rockMinigTime;
    float _rockMiningTime;

    int characterOreMiningCount = 10;


    public Dictionary<oreType,int> characterInventory = new Dictionary<oreType,int>();


    void Start()
    {
        miner = GetComponent<NavMeshAgent>();
        oreTarget = null;
        readyToMiningFlag = false;
        _rockMiningTime = rockMinigTime;
    }

    

    // Update is called once per frame
    void Update()
    {
        Mining();
        //GoToStorage();
    }

    void Mining()
    {
        if (oreTarget == null)
        {
            readyToMiningFlag = false;
        }
        if (!readyToMiningFlag)
        {
            if (spawner.spawningOre.Count > 0)
            {
                oreTarget = spawner.spawningOre[Random.Range(0, spawner.spawningOre.Count - 1)];
                miner.destination = oreTarget.transform.position;
                _ore = oreTarget.GetComponent<ore>();
                rockMinigTime = defaultMiningTime * _ore.timeMiningIndex;
                readyToMiningFlag = true;
                miner.stoppingDistance = _ore.oreSize * 1.3f;
            }
        }
        if (readyToMiningFlag)
        {
            if (currentPocket <= maxPocket)
            {
                if (miner.remainingDistance <= miner.stoppingDistance && !miner.isStopped)
                {
                    rockMinigTime -= Time.deltaTime;
                    if (rockMinigTime <= 0)
                    {
                        AddToCharacterInventory();
                        rockMinigTime = _rockMiningTime;
                    }
                }
            }
        }
    }
    private void AddToCharacterInventory()
    {
        if (characterInventory.ContainsKey(_ore.oreSpecie))
        {
            characterInventory[_ore.oreSpecie] += characterOreMiningCount;

        }
        else
        {
            characterInventory.Add(_ore.oreSpecie, characterOreMiningCount);
        }
        _ore.oreWasting(characterOreMiningCount);
        currentPocket += characterOreMiningCount;
    }


    /*void GoToStorage()
    {
        if(currentPocket>=maxPocket)
        {
            oreTarget = null;
            storageTarget = Dumper.transform.position;
            miner.destination = storageTarget;
            miner.stoppingDistance = 4;
            if(miner.remainingDistance <= miner.stoppingDistance)
            {
                currentPocket = 0;
            }
        }
    }
    */
}

