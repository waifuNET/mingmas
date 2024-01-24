using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct OreType
{
    public int oreCount;
    public oreType oreName;

    public OreType(int oreCount, oreType oreName) : this()
    {
        this.oreCount = oreCount;
        this.oreName = oreName;
    }
}
public class UIOreMining : MonoBehaviour
{
    
    
    
    private List<OreType> oreList = new List<OreType>()
    {
        new OreType(0,oreType.Rock),
        new OreType(0,oreType.Copper),
        new OreType(0,oreType.Iron),
        new OreType(0,oreType.Gold),
        new OreType(0,oreType.Titanum),
        new OreType(0,oreType.Diamond),
        new OreType(0,oreType.Ñrystall),
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
