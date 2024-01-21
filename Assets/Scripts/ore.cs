using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ore : MonoBehaviour
{
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
