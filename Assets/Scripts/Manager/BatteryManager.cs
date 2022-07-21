using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    //塔个体对象prefab
    public List<GameObject> m_BatteryPrefabs;
    //游戏场景里的塔基
    private GameObject[] m_BatterySlots;
    //塔基对应是否有塔
    private Dictionary<GameObject, bool> m_BatterySlotHasTower = new Dictionary<GameObject, bool>();
    [HideInInspector]
    private GameObject targetTowerBase = null;
    public GameObject TargetTowerBase { get => targetTowerBase; set => targetTowerBase = value; }
    private MoneySystem m_MoneySystem;

    private void Awake()
    {
        m_MoneySystem = GameObject.FindWithTag("GameManager").GetComponent<MoneySystem>();
        m_BatterySlots = GameObject.FindGameObjectsWithTag("BatterySlot");
        
        //初始化默认都没有塔
        for (int i = 0; i < m_BatterySlots.Length; ++i)
        {
            m_BatterySlotHasTower.Add(m_BatterySlots[i], false);
        }

    }

    //检测是否可以造塔
    public bool IfCanBuildBattery(int batteryIndex)
    {
        if (m_BatterySlotHasTower.ContainsKey(TargetTowerBase))
        {
            if (m_BatterySlotHasTower[TargetTowerBase] == false)
            {
                return true;
            }
            GameObject.FindWithTag("GameManager").GetComponent<DialogSystem>().ShowDialog("已有防御塔");
            return false;
        }

        return false;
    }



    //建造塔
    public void BuildBattery(int batteryIndex)
    {
        //Debug.Log(batteryIndex);
        if (!IfCanBuildBattery(batteryIndex))
        {
            return;
        }

        bool canBuy = false;
        switch (batteryIndex)
        {
            case 0:
                canBuy = m_MoneySystem.Buy(50);
                break;
            case 1:
                canBuy = m_MoneySystem.Buy(50);
                break;
        }
        if (!canBuy)
        {
            return;
        }
        
        m_BatterySlotHasTower[TargetTowerBase] = true;
        var clone = Instantiate(m_BatteryPrefabs[batteryIndex], TargetTowerBase.transform);
        clone.transform.parent = null;
    }
}
