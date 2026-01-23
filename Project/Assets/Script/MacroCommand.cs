using UnityEngine;
using System.Collections.Generic;
using GmshLib;

public class MacroCommand : ButtonBase
{
    [SerializeField] int m_nRecord = 3;
    [SerializeField, NonEditable] List<CommandData> m_commands = new List<CommandData>();
    List<RecordInfo> m_recordInfos = new List<RecordInfo>();
    [SerializeField] CommandData m_nullCommand;

    public void Init()
    {
        m_commands.Clear();

        for (int i = 0; i < m_nRecord; i++)
        {
            m_commands.Add(m_nullCommand);

            RecordInfo info = RecordInfo.Create(m_commands[i]);
            info.transform.SetParent(this.transform, false);
            info.Position = new Vector2(120.0f * (float)(i + 1), 0.0f);
            m_recordInfos.Add(info);
        }
    }

    public void SetMacro()
    {
        m_commands = CommandRecorder.Instance.GetCommandRecord(m_nRecord);

        for (int i = 0; i < m_nRecord; i++)
        {
            m_recordInfos[i].Init(m_commands[i]);
        }
    }

    protected override void OnClick()
    {
        base.OnClick();
        GameWork.Instance.SetCommands(m_commands);
    }

    protected override void OnScroll(int value)
    {
        base.OnScroll(value);
        SetMacro();
    }
}
