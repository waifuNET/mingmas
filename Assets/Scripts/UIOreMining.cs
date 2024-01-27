using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct OreTypeStruct
{
    public int oreCount;
    public oreType oreName;

    public OreTypeStruct(int oreCount, oreType oreName) : this()
    {
        this.oreCount = oreCount;
        this.oreName = oreName;
    }
    
    
}
public class UIOreMining : MonoBehaviour
{
    
    
    
    public List<OreTypeStruct> oreList = new List<OreTypeStruct>()
    {
        new OreTypeStruct(0,oreType.Rock),
        new OreTypeStruct(0,oreType.Copper),
        new OreTypeStruct(0,oreType.Iron),
        new OreTypeStruct(0,oreType.Gold),
        new OreTypeStruct(0,oreType.Titanum),
        new OreTypeStruct(0,oreType.Diamond),
        new OreTypeStruct(0,oreType.Ñrystall),
    };

    public TextMeshProUGUI textOreCounterUGUI;

    // Update is called once per frame
    void Update()
    {
        textOreCounterUGUI.text = "";
        for(int i =0; i < oreList.Count;i++)
        {
            textOreCounterUGUI.text += oreList[i].oreName.ToString() + ": " + oreList[i].oreCount.ToString()+ "\n";
        }    
    }
}
