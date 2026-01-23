using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class HitPoint
{
    int m_hp;
    int m_maxHp;

    [SerializeField] TextMeshPro m_text;

    public HitPoint(int maxHp, TextMeshPro text)
    {
        m_text = text;
        Hp = maxHp;
        m_maxHp = maxHp;
    }

    public bool IsZero() { return m_hp == 0; }
    public int Hp
    {
        get
        {
            return m_hp;
        }
        set
        {
            m_hp = value;
            if(m_text != null) m_text.text = m_hp.ToString();
        }
    }
    public int MaxHp => m_maxHp;

    public void Damege(int n)
    {
        Hp = Mathf.Clamp(Hp - n, 0, m_maxHp);
    }

    public void Heal(int n)
    {
        Hp = Mathf.Clamp(Hp - n, 0, m_maxHp);
    }


    public void MaxHeal()
    {
        Hp = m_maxHp;
    }
}
