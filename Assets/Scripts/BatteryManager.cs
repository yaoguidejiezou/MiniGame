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

    private void Awake()
    {
        m_BatterySlots = GameObject.FindGameObjectsWithTag("BatterySlot");

        //初始化默认都没有塔
        for (int i = 0; i < m_BatterySlots.Length; ++i)
        {
            m_BatterySlotHasTower.Add(m_BatterySlots[i], false);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //检测是否可以造塔
    public bool IfCanBuildBattery(int batteryIndex)
    {
        if (m_BatterySlotHasTower.ContainsKey(TargetTowerBase))
        {
            return !m_BatterySlotHasTower[TargetTowerBase];
        }
        //输出不能造塔的对话框
        return false;
    }



    //建造塔
    public void BuildBattery(int batteryIndex)
    {
        Debug.Log(batteryIndex);
        if (!IfCanBuildBattery(batteryIndex))
        {
            return;
        }
        
        
        m_BatterySlotHasTower[TargetTowerBase] = true;
        var clone = Instantiate(m_BatteryPrefabs[batteryIndex], TargetTowerBase.transform);
        clone.transform.parent = null;
    }
}
