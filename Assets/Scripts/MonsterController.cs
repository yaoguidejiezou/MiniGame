using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int m_Health;
    public float m_Speed;
    public int m_MonsterPower;
    public GameObject m_Path;
    public Transform[] m_Children;
    private int m_NowTargetIndex;
    private Vector3 m_Direction;
    private Coroutine m_AttackedCoroutine;
    public HouseManager m_HouseManager;

    public enum MonsterState
    {
        Alive,
        Hurt, 
        Die
    }

    public MonsterState m_State;

    // Start is called before the first frame update
    void Start()
    {
        m_HouseManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HouseManager>();
        m_Children = m_Path.GetComponentsInChildren<Transform>();
        m_NowTargetIndex = 1;
        m_Direction = (m_Children[1].transform.position - transform.position).normalized;
        m_State = MonsterState.Alive;
        // foreach (var child in m_Children)
        // {
        //     Debug.Log(child.name);
        // }


    }

    
    // Update is called once per frame
    void Update()
    {
         transform.Translate(m_Direction * m_Speed, Space.Self);
         if(m_NowTargetIndex <= m_Children.Length - 1 &&
            Vector3.Magnitude(m_Children[m_NowTargetIndex].transform.position - transform.position) <= 0.3 &&
            m_State != MonsterState.Die)
         {
             if (m_NowTargetIndex == m_Children.Length - 1)
             {
                 m_HouseManager.HurtHouse(m_MonsterPower);
                 Destroy(gameObject);
             }
             else
             {
                 m_NowTargetIndex++;
             }

             if (m_NowTargetIndex <= m_Children.Length - 1)
             {
                 m_Direction = (m_Children[m_NowTargetIndex].transform.position - transform.position).normalized;
             }
         }
    }

    private Coroutine AttackedCoroutine;
    private Coroutine DiedCoroutine;
    public void Hurt(int power)
    {
        m_State = MonsterState.Hurt;
        AttackedCoroutine = StartCoroutine(Attacked(power));
        if (m_Health <= 0)
        {
            m_State = MonsterState.Die;
            DiedCoroutine = StartCoroutine(Died());
        }
    }
    
    IEnumerator Attacked(int lossHealth)
    {
        //Debug.Log(m_Health);
        GetComponent<MeshRenderer>().material.color = Color.red;
        m_Health -= lossHealth;
        for (float timer = 0; timer < 0.5; timer += Time.deltaTime)
        {
            yield return 0;
        }
        GetComponent<MeshRenderer>().material.color = Color.green;
        StopCoroutine(AttackedCoroutine);
        
    }
    
    IEnumerator Died()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshRenderer>().material.color = Color.green;
        m_Direction = new Vector3(0, 0, 0);
        for (float timer = 0; timer < 3; timer += Time.deltaTime)
        {
            yield return 0;
        }
        Destroy(gameObject);
        StopCoroutine(DiedCoroutine);
        
    }
    
    
}
