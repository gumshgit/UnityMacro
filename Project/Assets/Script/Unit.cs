using UnityEngine;
using GmshLib;
using TMPro;

public class Unit : MonoBehaviour
{
    UnitData m_unitData;
    [SerializeField] SpriteRenderer m_spren;

    [SerializeField, NonEditable] CommandData m_nextCommand;
    public CommandData NextCommand => m_nextCommand;

    [SerializeField, NonEditable] int m_pos = 0;
    public int Position => m_pos;

    [SerializeField]
    HitPoint m_hp;

    UNIT_TYPE m_unitType;
    public UNIT_TYPE UnitType => m_unitType;

    [SerializeField] TextMeshPro m_text;

    [SerializeField] SpriteRenderer m_nextCommandImg;

    int m_nextCommandIndex = 0;

    public static Unit Create(UnitData unitData, UNIT_TYPE unitType)
    {
        GameObject obj = PrefabManager.CreateObj(PrefabDef.Unit);
        var v = obj.GetComponent<Unit>();
        v.Init(unitData, unitType);
        return v;
    }

    public void Init(UnitData unitData, UNIT_TYPE unitType)
    {
        m_unitData = unitData;
        m_spren.sprite = m_unitData.m_img;
        m_hp = new HitPoint(m_unitData.m_hp, m_text);
        m_unitType = unitType;
    }

    public void MovePosition(Cell cell)
    {
        this.transform.position = cell.transform.position;
        m_pos = cell.Position;
    }

    public bool IsDead()
    {
        return m_hp.IsZero();
    }

    public void Damage(int n)
    {
        m_hp.Damege(n);
    }

    public void SetNextCommand(int playerPos, int moveRange)
    {
        int diff = Mathf.Abs(m_pos - playerPos);
        if(diff <= moveRange)
        {
            m_nextCommand = m_unitData.m_commands[m_nextCommandIndex];
            m_nextCommandIndex = (m_nextCommandIndex + 1) % m_unitData.m_commands.Count;
        }
        else
        {
            m_nextCommand = null;
        }
        m_nextCommandImg.sprite = m_nextCommand == null ? null : m_nextCommand.m_img;
    }
}

public enum UNIT_TYPE
{
    PLAYER,
    ENEMY,
    MAX_NUM,
}