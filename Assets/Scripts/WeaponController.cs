using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform m_HoldPoint;
    public Transform m_Hand;
    public Weapon m_StartWeapon; 
    public Weapon m_UsingWeapon;
    public HandState m_HandState;
    
    public Weapon item;
    public Transform point;
    public Vector3 velocity;//46
    //float time;
    public bool debug;
    public enum HandState
    {
        UpHand,
        DownHand,
        Neither
    };
    /// <summary>
    /// 武器初始化
    /// </summary>
    void Start()
    {
        if (m_UsingWeapon == null)
        {
            EquipWeapon(m_StartWeapon);
        }

        m_HandState = HandState.Neither;
    }

    /// <summary>
    /// 主要针对武器投掷
    /// </summary>
    private float m_Strength = 0f;
    private float m_HandUpSpeed = 0.5f;
    private float m_HandDownSpeed = 0.8f;
    private float m_Rotation = 0.0f;

    private void Update()
    {
        // 投掷武器
        if (m_HandState == HandState.UpHand && m_Rotation >= -90.0f)
        {
            m_Rotation -= m_HandUpSpeed;
            m_Hand.localEulerAngles = new Vector3(m_Rotation, 0, 0);
        }
        if (m_HandState == HandState.DownHand)
        {
            m_Rotation += m_HandDownSpeed;
            m_Hand.localEulerAngles = new Vector3(m_Rotation, 0, 0);
            //m_Hand.localEulerAngles = new Vector3(-90.0f + (Time.time - m_PressTime) / 0.3f, 0, 0);
            if (m_Rotation >= 0.0f)
            {
                m_Hand.localEulerAngles = new Vector3(0, 0, 0);
                m_HandState = HandState.Neither;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_Strength = Time.time;
            m_HandState = HandState.UpHand;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_HandState = HandState.DownHand;
            m_Strength = Time.time - m_Strength;
            if (m_Strength >= 2.0f)
            {
                m_Strength = 2.0f;
            }
            // m_UsingWeapon.Attack(m_Strength, gameObject.transform.forward);
            // m_UsingWeapon = null;
            velocity = transform.forward * m_Strength * 25;
            m_UsingWeapon.m_Velocity = velocity;
            m_UsingWeapon.m_Active = true;
            velocity = Vector3.zero;
            //time = 0;
            if (debug)
            {
                Debug.Break();
                debug = false;
            }
            m_UsingWeapon = null;
        }
        
        // 换弹
        if (m_UsingWeapon == null)
        {
            EquipWeapon(m_StartWeapon);
        }
    }

    /// <summary>
    /// 更换武器或装备武器则调用此函数
    /// </summary>
    /// <param name="weapon"></param>
    void EquipWeapon(Weapon weapon)
    {
        if (m_UsingWeapon != null)
        {
            Destroy(m_UsingWeapon.gameObject);   
        }

        m_UsingWeapon = Instantiate(weapon, m_HoldPoint.position, m_HoldPoint.rotation) as Weapon;
        m_UsingWeapon.transform.parent = m_HoldPoint;
    }

}
