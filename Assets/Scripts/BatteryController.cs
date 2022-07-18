using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public Transform m_TargetMonster;
    public Projectile m_UsingBullet;
    public Transform m_ProjectPoint;
    public Transform m_FinalPoint;
    
    
    private void OnTriggerStay(Collider other)
    {
        
    }

    public void Attack()
    {
        var m_Bullet = Instantiate(m_UsingBullet, m_ProjectPoint);
        m_Bullet.m_TargetMonster = m_TargetMonster;
    }
    
    IEnumerator Attack(int lossHealth)
    {
        while (true)
        {
            for (float timer = 0; timer < 1; timer += Time.deltaTime)
            {
                yield return 0;
            }
            
            // seconds++;
            //
            // if (InAttackRangeMonster != null)
            // {
            //     Debug.Log(InAttackRangeMonster.name + "在" + seconds + "秒" + "受到攻击");
            //     InAttackRangeMonster.GetComponent<MonsterController>().Hurt();
            // }
            //
            // if (seconds >= 3)
            // {
            //     StopCoroutine(m_CheckAttackedCoroutine);
            // }
            
        }
    }
}
