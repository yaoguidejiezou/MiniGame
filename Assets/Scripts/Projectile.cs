using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_Speed;
    public Transform m_TargetMonster;
    private Vector3 m_Direction;
    // Start is called before the first frame update
    void Start()
    {
        m_Direction = (m_TargetMonster.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction * m_Speed, Space.Self);
         
        if(Vector3.Magnitude(m_TargetMonster.position - transform.position) <= 0.5)
        {
            m_TargetMonster.GetComponent<MonsterController>().Hurt();
            Destroy(gameObject);
        }
    }
}
