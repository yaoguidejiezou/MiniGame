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
    private float m_Strength;
    private float m_PressTime;
    private float m_HandUpSpeed;
    private float m_HandDownSpeed;

    private void Update()
    {
        if (m_HandState == HandState.UpHand && m_Hand.rotation.x >= -90.0f)
        {
            m_Hand.localEulerAngles = new Vector3(-(Time.time - m_PressTime) / 0.5f * 90, 0, 0);
        }
        if (m_HandState == HandState.DownHand)
        {
            m_Hand.localEulerAngles = new Vector3(-90.0f + (Time.time - m_PressTime) / 0.3f, 0, 0);
            if (m_Hand.rotation.x >= 0.0f)
            {
                m_Hand.localEulerAngles = new Vector3(0, 0, 0);
                m_HandState = HandState.Neither;

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            m_HandState = HandState.UpHand;
            m_PressTime = Time.time;
            //摆臂物理逻辑（0.5s完成手臂上扬）
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_HandState = HandState.DownHand;
            m_Strength = Time.time - m_Strength;
            m_UsingWeapon.Attack(m_Strength);
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
