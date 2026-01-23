using UnityEngine;
using GmshLib;
using System.Collections.Generic;

public class GameField : SingletonMonoBehaviour<GameField>
{
    [SerializeField] int m_fieldCount = 10;

    List<Cell> m_cells = new List<Cell>();

    [SerializeField] float m_cellSpace = 1.25f;

    [SerializeField, NonEditable]
    Unit m_playerUnit;
    [SerializeField, NonEditable]
    List<Unit> m_enemyUnits = new List<Unit>();


    [SerializeField] UnitData m_playerUnitData;

    [SerializeField] List<UnitData> m_enemyUnitDatas = new List<UnitData>();
    [SerializeField] int m_nEnemy = 3;

    [SerializeField, NonEditable] List<Unit> m_fieldUnits = new List<Unit>();

    [SerializeField] int m_enemyMoveRange = 4;

    public void Init()
    {
        for(int i = 0; i < m_fieldCount; i++)
        {
            Cell cell = Cell.Create(i);
            cell.transform.SetParent(this.transform);
            cell.transform.position = new Vector3((float)i * m_cellSpace, 0.0f, 0.0f);
            m_cells.Add(cell);
        }

        for(int i = 0; i < m_fieldCount; i++)
        {
            m_fieldUnits.Add(null);
        }

        //プレイヤー生成
        AddUnit(m_playerUnitData, 0, UNIT_TYPE.PLAYER);
        MainCamera.Instance.transform.SetParent(m_playerUnit.transform);

        //敵生成
        List<int> enemyPoss = MathUtil.GetNotCoverRandList(3, m_fieldCount, m_nEnemy);
        foreach(var v in enemyPoss)
        {
            int rand = MathUtil.GetRand(0, m_enemyUnitDatas.Count);
            AddUnit(m_enemyUnitDatas[rand], v, UNIT_TYPE.ENEMY);
        }
    }

    void AddUnit(UnitData data, int pos, UNIT_TYPE type)
    {
        Unit u = Unit.Create(data, type);
        u.transform.SetParent(this.transform);
        u.MovePosition(m_cells[pos]);
        m_fieldUnits[pos] = u;

        if (type == UNIT_TYPE.PLAYER)
        {
            m_playerUnit = u;
        }
        else
        {
            m_enemyUnits.Add(u);
        }
    }

    public void DoAction(UNIT_TYPE type)
    {
        if(type == UNIT_TYPE.PLAYER)
        {
            List<CommandData> commands = GameWork.Instance.SelectCommands;
            foreach(var v in commands)
            {
                DoActionUnit(m_playerUnit, v, type);
            }
        }
        else
        {
            for(int i = 0; i < m_enemyUnits.Count; i++)
            {
                DoActionUnit(m_enemyUnits[i], m_enemyUnits[i].NextCommand, type);
            }
        }
    }

    void DoActionUnit(Unit unit, CommandData command, UNIT_TYPE type)
    {
        if (command == null) return;

        int pos = unit.Position;
        UNIT_TYPE opponent = (UNIT_TYPE)(((int)type + 1) % 2);
        switch (command.m_commandType.Value)
        {
            case CommandType.Move:
                {
                    int nextPos = Mathf.Clamp(pos + command.m_option1, 0, m_fieldCount - 1);
                    if (CanMove(nextPos))
                    {
                        SwapFieldUnitPosition(pos, nextPos);
                        pos = nextPos;
                        unit.MovePosition(m_cells[pos]);
                    }
                    break;
                }
            case CommandType.Attack:
                {
                    foreach(var v in command.m_intList)
                    {
                        int nextPos = Mathf.Clamp(pos + v, 0, m_fieldCount - 1);
                        Atack(nextPos, command, opponent);
                    }
                    break;
                }
        }
    }

    public bool IsGoal()
    {
        return m_playerUnit.Position == m_fieldCount - 1;
    }

    bool CanMove(int pos)
    {
        if (pos < 0 || pos >= m_fieldCount) return false;

        return m_fieldUnits[pos] == null;
    }

    void SwapFieldUnitPosition(int a, int b)
    {
        Unit tmp = m_fieldUnits[a];
        m_fieldUnits[a] = m_fieldUnits[b];
        m_fieldUnits[b] = tmp;
    }

    void Atack(int pos, CommandData command, UNIT_TYPE targetType)
    {
        if (pos < 0 || pos >= m_fieldCount) return;
        Unit unit = m_fieldUnits[pos];
        if (unit == null) return;
        if (unit.UnitType != targetType) return; //同チーム

        unit.Damage(command.m_option1);
        if (unit.IsDead())
        {
            m_fieldUnits[pos] = null;

            if(targetType == UNIT_TYPE.ENEMY)
            {
                m_enemyUnits.Remove(unit);
            }
            Destroy(unit.gameObject);
        }
    }

    public bool IsPlayerDead()
    {
        return m_playerUnit == null;
    }

    public void SetEnemyCommand()
    {
        foreach(var v in m_enemyUnits)
        {
            v.SetNextCommand(m_playerUnit.Position, m_enemyMoveRange);
        }
    }
}
