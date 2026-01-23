using UnityEngine;
using GmshLib;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    StepManager m_step;

    void Start()
    {
        m_step.Set(StageInit);
    }

    void Update()
    {
        m_step.Update();
    }

    void StageInit()
    {
        GameField.Instance.Init();
        CommandRecorder.Instance.Init();
        CommandSelector.Instance.InitCommand();
        m_step.Set(StepInputCommand);
    }

    void StepInputCommand()
    {
        if (m_step.IsInit)
        {
            GameWork.Instance.ClearSelectCommands();
            GameField.Instance.SetEnemyCommand();
        }

        if (GameWork.Instance.IsSelectCommand())
        {
            CommandRecorder.Instance.Record();
            m_step.Set(StepPlayerAction);
        }
    }

    void StepPlayerAction()
    {
        if (m_step.IsInit)
        {
            GameField.Instance.DoAction(UNIT_TYPE.PLAYER);
            m_step.Set(StepCheckGoal);
            
        }
    }

    void StepCheckGoal()
    {
        if (m_step.IsInit)
        {
            if (GameField.Instance.IsGoal())
            {
                m_step.Set(StepGoal);
            }
            else
            {
                m_step.Set(StepEnemyAction);
            }

        }
    }

    void StepEnemyAction()
    {
        if (m_step.IsInit)
        {
            GameField.Instance.DoAction(UNIT_TYPE.ENEMY);
            m_step.Set(StepPlayerDeadCheck);
        }
    }

    void StepPlayerDeadCheck()
    {
        if (m_step.IsInit)
        {
            if (GameField.Instance.IsPlayerDead())
            {
                m_step.Set(StepDead);
            }
            else
            {
                m_step.Set(StepInputCommand);
            }
        }
    }

    void StepGoal()
    {
        if (m_step.IsInit)
        {
            GmshUtil.Log("Goal!!!");
        }
    }

    void StepDead()
    {
        if (m_step.IsInit)
        {
            GmshUtil.Log("Dead...");
        }
    }
}
