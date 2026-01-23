using UnityEngine;
using GmshLib;
using System.Collections.Generic;

public class CommandRecorder : SingletonMonoBehaviour<CommandRecorder>
{
    [SerializeField] int m_nRecord = 5;
    [SerializeField, NonEditable] List<CommandData> m_commands = new List<CommandData>();
    [SerializeField] CommandData m_nullCommand;
    List<RecordInfo> m_recordInfos = new List<RecordInfo>();

    public void Init()
    {
        m_commands.Clear();

        for(int i = 0; i < m_nRecord; i++)
        {
            m_commands.Add(m_nullCommand);

            RecordInfo info = RecordInfo.Create(m_commands[i]);
            info.transform.SetParent(this.transform, false);
            info.Position = new Vector2(120.0f * (float)i, 0.0f);
            m_recordInfos.Add(info);
        }
    }

    public void Record()
    {
        List<CommandData> commands = new List<CommandData>(GameWork.Instance.SelectCommands);
        m_commands.AddRange(commands);
        m_commands = m_commands.GetRange(m_commands.Count - m_nRecord, m_nRecord);
        UpdateRecordInfo();
    }

    public List<CommandData> GetCommandRecord(int n)
    {
        return m_commands.GetRange(m_nRecord - n, n);
    }

    void UpdateRecordInfo()
    {
        for (int i = 0; i < m_nRecord; i++)
        {
            m_recordInfos[i].Init(m_commands[i]);
        }
    }
}
