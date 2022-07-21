using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BatteryController : MonoBehaviour
{
    public GameObject m_TargetMonster;
    public Projectile m_UsingBullet;
    public Transform m_ProjectPoint;
    public Transform m_FinalPoint;
    public float m_AttackTimeInterval;
    public float m_TurnSpeed;
    public int m_Cost;
    
    public List<GameObject> m_Monsters = new List <GameObject>();

    private float m_LastAttackTime;
    public void Start()
    {
        m_LastAttackTime = 0;
        m_FinalPoint = GameObject.FindWithTag("FinalPoint").transform;
    }

    public void Update()
    {

        foreach (var monster in m_Monsters)
        {
            if (monster.GetComponent<MonsterController>().m_State == MonsterController.MonsterState.Die)
            {
                m_Monsters.Remove (monster);
                break;
            }
        }

        if (m_Monsters.Count > 0 && m_TargetMonster != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                    Quaternion.LookRotation(new Vector3( m_TargetMonster.transform.position.x - transform.position.x,
                        0, 
                        m_TargetMonster.transform.position.z - transform.position.z)),
                    5.0f * Time.deltaTime);

            if (Time.time - m_LastAttackTime >= m_AttackTimeInterval)
            {
                var m_Bullet = Instantiate(m_UsingBullet, m_ProjectPoint);
                m_Bullet.transform.parent = null;
                m_Bullet.m_TargetMonster = m_TargetMonster.transform;
                m_LastAttackTime = Time.time;
            }

        }
    }
    
    private GameObject CheckCloserMonster()
    {
        int index = -1;
        float minDistance = 999999;
        for(int i = 0; i < m_Monsters.Count && m_Monsters[i].GetComponent<MonsterController>().m_State != MonsterController.MonsterState.Die; i++)
        {
            float newDistance = Vector3.Magnitude(m_Monsters[i].transform.position - m_FinalPoint.position);
            if (newDistance <= minDistance)
            {
                minDistance = newDistance;
                index = i;
            }
        }

        if (index == -1)
        {
            return null;
        }
        return m_Monsters[index];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))//检查标签
        {
            m_Monsters.Add(other.gameObject);
            m_TargetMonster = CheckCloserMonster();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_Monsters.Remove (other.gameObject);
        if(m_Monsters.Count > 0) { m_TargetMonster = CheckCloserMonster(); }
    }

}
