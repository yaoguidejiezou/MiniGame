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
        //Debug.Log(m_Children[m_NowTargetIndex].name);
        if (Vector3.Magnitude(m_Children[m_NowTargetIndex].transform.position - transform.position) <= 0.3)
        {
            m_NowTargetIndex++;
            
            m_Direction = (m_Children[m_NowTargetIndex].transform.position - transform.position).normalized;
            if (m_NowTargetIndex == m_Children.Length - 1)
            {
                Destroy(gameObject);
            }
        }
    }
    
    
}
