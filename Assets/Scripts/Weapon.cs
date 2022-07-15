using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 m_Velocity;
    public LayerMask m_Layer;
    public Vector3 m_Gravity = Physics.gravity;
    public bool m_Active;
    public GameObject m_Effect;
 
    private Vector3 current_pos;//当前位置
    private Vector3 m_Start;
    private float i = 1f;
 
    //额外视觉效果 使模型可以旋转
    public float rotateSpeed = 600f; 
    public Transform model;
    Vector3 defaultRotate;
    void Start()
    {
        m_Start = transform.position;
        current_pos = m_Start;
        if (model) defaultRotate = model.eulerAngles;
    } 
 
 
    private void Update()
    {
        if (m_Active)
        { 
            //model.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.Self);//在空中旋转
        }
    }
    private void FixedUpdate()
    {
        CalculateThrownMove(Time.fixedDeltaTime);
    }

    private Coroutine m_CheckAttackedCoroutine;
    /// <summary>
    /// 核心计算 
    /// </summary>
    void CalculateThrownMove(float tick)
    {
        if (m_Active)
        {
            float time = tick * i;
            var gravity = m_Gravity * time * time / 2;//计算重力
            var nex = m_Velocity * time;//下一个位置
            Vector3 next_pos = m_Start + nex + gravity; //下一个位置
            Debug.DrawLine(current_pos, next_pos, Color.yellow);
            transform.position = current_pos; 
            if (Physics.Linecast(current_pos, next_pos, out RaycastHit hit, m_Layer))
            {
                //model.eulerAngles = defaultRotate + new Vector3(Random.Range(1, 46), 0, 0); //更改旋转角度
                transform.parent = null;
                m_Active = false; 
                //Debug.Log(hit.transform.name);//击中了 做一些判断
                
                Destroy(gameObject.GetComponent<MeshRenderer>());
                var c_CloneEffect = Instantiate(m_Effect, transform.position, new Quaternion(0f, 0f, 0f, 0f));
                m_CheckAttackedCoroutine = StartCoroutine(CheckAttacked());
                Destroy(c_CloneEffect, 3.0f);
                Destroy(gameObject, 3.0f);
            }
            current_pos = next_pos;
            i++; //很重要  
        }
    }
    
    public GameObject InAttackRangeMonster;
    public int seconds = 0;
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Monster"))
        {
            InAttackRangeMonster = collider.gameObject;
        }
    }
    
    IEnumerator CheckAttacked()
    {
        
        while (true)
        {
            for (float timer = 0; timer < 1; timer += Time.deltaTime)
            {
                yield return 0;
            }
            
            seconds++;
            if (InAttackRangeMonster != null)
            {
                Debug.Log(InAttackRangeMonster.name + "在" + seconds + "秒" + "受到攻击");
                InAttackRangeMonster.GetComponent<MonsterController>().Hurt();
            }
            
            if (seconds >= 3)
            {
                StopCoroutine(m_CheckAttackedCoroutine);
            }
            
        }
    }

}