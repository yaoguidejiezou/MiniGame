using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int m_Health;
    public float m_Speed;
    public GameObject m_Path;
    public Transform[] m_Children;
    private int m_NowTargetIndex;
    private Vector3 m_Direction;
    private Coroutine m_AttackedCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        m_Children = m_Path.GetComponentsInChildren<Transform>();
        m_NowTargetIndex = 1;
        m_Direction = (m_Children[1].transform.position - transform.position).normalized;
        // foreach (var child in m_Children)
        // {
        //     Debug.Log(child.name);
        // }


    }

    
    // Update is called once per frame
    void Update()
    {
         transform.Translate(m_Direction * m_Speed, Space.Self);
         
         if(m_NowTargetIndex <= m_Children.Length - 1 && Vector3.Magnitude(m_Children[m_NowTargetIndex].transform.position - transform.position) <= 0.3)
         {
             if (m_NowTargetIndex == m_Children.Length - 1)
             {
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
    public void Hurt()
    {
        AttackedCoroutine = StartCoroutine(Attacked(10));
    }
    
    IEnumerator Attacked(int lossHealth)
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        for (float timer = 0; timer < 0.5; timer += Time.deltaTime)
        {
            yield return 0;
        }
        m_Health -= lossHealth;
        GetComponent<MeshRenderer>().material.color = Color.green;
        StopCoroutine(AttackedCoroutine);
        
    }

    
    
}
