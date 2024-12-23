using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterStatus
{
    Idle = 0,
    Walk = 1,
    Mining = 2,
	Unloading = 3,
}

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

    float rockMinigTime;
    float _rockMiningTime;

    int characterOreMiningCount = 10;

    public CharacterStatus characterStatus = CharacterStatus.Idle;

    public UIOreMining _UIOreMining;
    public oreType _oreType;
    

    public Dictionary<oreType, int> characterInventory = new Dictionary<oreType, int>();

    public Animator animator;

    public void AddToStorage()
    {
        for(int i= 0;i<_UIOreMining.oreList.Count;i++)
        {
            oreType tempOreType = _UIOreMining.oreList[i].oreName;
            if(characterInventory.TryGetValue(tempOreType, out int value))
            {
                _UIOreMining.oreList[i] = new OreTypeStruct(_UIOreMining.oreList[i].oreCount+value,tempOreType);
            }
        }    
    }

    void Start()
    {
        miner = GetComponent<NavMeshAgent>();
        oreTarget = null;
        _rockMiningTime = rockMinigTime;
		characterStatus = CharacterStatus.Idle;
        animator = GetComponent<Animator>();
	}

    public bool TargetExist()
    {
        if (oreTarget == null) return false;
        else return true;
    }

    public bool InvetoryFull()
    {
        if (currentPocket <= maxPocket) return false;
        else return true;
    }

    public bool Arrived()
    {
        if (miner.remainingDistance <= miner.stoppingDistance) return true;
        else return false;
    }

    public bool GetOre()
    {
        oreTarget = null;
        _ore = null;

		if (spawner.spawningOre.Count == 0) return false;

        oreTarget = spawner.spawningOre[Random.Range(0, spawner.spawningOre.Count - 1)];
        if(oreTarget == null) return false;

        _ore = oreTarget.GetComponent<ore>();
		if (_ore == null) return false;

		rockMinigTime = defaultMiningTime * _ore.timeMiningIndex;
        return false;
    }

    public void GoToOre()
    {
        if (oreTarget == null) return;

        CharacterSetDestination(oreTarget.transform.position, _ore.oreSize * 1.3f);
    }

    public void GoToUnloading()
    {
        CharacterSetDestination(Dumper.transform.position, 4);
    }

    public void CharacterSetDestination(Vector3 point, float stopDistance)
    {
        miner.destination = point;
        miner.stoppingDistance = stopDistance;
    }

    public void UnloadingPoket() // �����������, ��� �� �������� � �����
    {
        AddToStorage();
        currentPocket = 0;
        characterInventory.Clear();
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        switch (characterStatus)
        {
            case CharacterStatus.Idle:
                ResetAllTriggers("Idle");
                if (TargetExist())
                {
                     GoToOre();
                     characterStatus = CharacterStatus.Walk;
                }
                else if (!TargetExist())
                {
                    GetOre();
                }
                else if (!InvetoryFull())
                {
                    if (TargetExist())
                    {
                        GoToOre();
                        characterStatus = CharacterStatus.Walk;
                    }
                }
                else if (InvetoryFull())
                {
                    characterStatus = CharacterStatus.Walk;
                }
                break;
            case CharacterStatus.Walk:
                ResetAllTriggers("Walking");
                if (Arrived())
                {
                    characterStatus = CharacterStatus.Mining;
                }
                break;
            case CharacterStatus.Mining:
                ResetAllTriggers("Mining");
                animator.SetTrigger("Mining");
                Mining();
                break;
            case CharacterStatus.Unloading:
				UnloadingPoket();
				characterStatus = CharacterStatus.Idle;
				break;
        }
    }

    void Mining()
    {
        if (InvetoryFull()) { characterStatus = CharacterStatus.Walk; GoToUnloading(); return; }
        if(oreTarget == null) { characterStatus = CharacterStatus.Idle; return; }

		rockMinigTime -= Time.deltaTime;
        if (rockMinigTime <= 0)
        {
            AddToCharacterInventory();
            rockMinigTime = _rockMiningTime;
        }
    }

    void AddToCharacterInventory()
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
        if(_ore.oreDepositCount <= 0)
        {
            characterStatus = CharacterStatus.Idle;
        }
        currentPocket += characterOreMiningCount;
    }

    private List <string> AnimationList = new List <string>()
    {
        "Mining","Idle","Walking"
    };
    void ResetAllTriggers(string nameAnimations)
    {
        foreach(var item in AnimationList)
        {
            animator.ResetTrigger(item);
        }
        animator.SetTrigger(nameAnimations);
    }
}

