using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<MonsterSpawnUnit> m_MonsterList;
    private float m_LastSpawnTime;
    private int m_NowMonsterIndex;
    public Transform m_SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        m_LastSpawnTime = 0;
        m_NowMonsterIndex = 0;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Time.time - m_LastSpawnTime >= m_MonsterList[m_NowMonsterIndex].m_SpawnTimeInterval)
        {
            //生成
            var m_NewMonster = Instantiate(m_MonsterList[m_NowMonsterIndex].m_Monster, m_SpawnPoint.position, m_SpawnPoint.rotation);
            m_NewMonster.m_Path = m_MonsterList[m_NowMonsterIndex].m_MonsterPath;
            m_NowMonsterIndex++;
            m_LastSpawnTime = Time.time;
            if (m_NowMonsterIndex > m_MonsterList.Count - 1)
            {
                gameObject.GetComponent<MonsterSpawner>().enabled = false;
            }
        }
    }
}

[System.Serializable]
public class MonsterSpawnUnit : System.Object
{
    public float m_SpawnTimeInterval;
    public MonsterController m_Monster;
    public GameObject m_MonsterPath;
}
