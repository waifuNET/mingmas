using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ore : MonoBehaviour
{
    //Ore mining index
    public float rockMiningIndex = 1;
    public float copperMiningIndex = 2;
    public float ironMiningIndex = 3;
    public float goldMiningIndex = 4;
    public float titanumMiningIndex = 5;
    public float diamondMiningIndex = 6;
    public float crystallsMiningIndex = 10;

    public float oreSize;
    public float oreMiningTime;
    public int oreDepositCount;

    public oreSpawner oreSpawner;

    public oreType oreSpecie;

    public void oreWasting(int oreMiningCount)
    {
        oreDepositCount -= oreMiningCount;
        if (oreDepositCount <= 0)
        {
            oreSpawner.oreRemove(gameObject);
        }
    }

    public void Init()
    {
        oreMiningTime = Random.Range(oreMiningTime, oreMiningTime + 1f);
        oreDepositCount = Random.Range(oreDepositCount, oreDepositCount + 15);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        oreSpawner = FindAnyObjectByType<oreSpawner>();
    }
}
