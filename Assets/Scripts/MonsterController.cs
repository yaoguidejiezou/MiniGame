using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public int m_Health;
    public float m_Speed;
    public int m_MonsterPower;
    private Coroutine m_AttackedCoroutine;
    public HouseManager m_HouseManager;


    private NavMeshAgent m_PathFinder;
    private Transform m_FinalPoint;
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
        //m_HouseManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HouseManager>();
        m_State = MonsterState.Alive;
        m_PathFinder = GetComponent<NavMeshAgent>();
        m_FinalPoint = GameObject.FindGameObjectWithTag("FinalPoint").transform;
        m_PathFinder.SetDestination(m_FinalPoint.position);
        // foreach (var child in m_Children)
        // {
        //     Debug.Log(child.name);
        // }


    }

    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Magnitude(m_FinalPoint.position - transform.position) <= 0.2)
        {
            GameObject.FindWithTag("GameManager").GetComponent<HouseManager>().HurtHouse(m_MonsterPower);
            Destroy(gameObject);
        }
    }

    private Coroutine AttackedCoroutine;
    private Coroutine DiedCoroutine;
    public void Hurt(int power)
    {
        m_State = MonsterState.Hurt;
        AttackedCoroutine = StartCoroutine(Attacked(power));

    }
    
    IEnumerator Attacked(int lossHealth)
    {
        //Debug.Log(m_Health);
        if (m_State == MonsterState.Hurt)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            m_Health -= lossHealth;
            if (m_Health <= 0)
            {
                m_State = MonsterState.Die;
                DiedCoroutine = StartCoroutine(Died());
                yield break;
            }
            for (float timer = 0; timer < 0.5; timer += Time.deltaTime)
            {
                yield return 0;
            }
            GetComponent<MeshRenderer>().material.color = Color.green;
            if (m_State == MonsterState.Hurt)
            {
                StopCoroutine(AttackedCoroutine);
            }
        }
    }
    
    IEnumerator Died()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        GetComponent<Rigidbody>().isKinematic = false;
        for (float timer = 0; timer < 3; timer += Time.deltaTime)
        {
            yield return 0;
        }
        Destroy(gameObject);
        StopCoroutine(DiedCoroutine);
        
    }
    
    
}
