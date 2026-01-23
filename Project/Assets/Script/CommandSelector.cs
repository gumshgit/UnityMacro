using UnityEngine;
using GmshLib;
using System.Collections.Generic;

public class CommandSelector : SingletonMonoBehaviour<CommandSelector>
{
    [SerializeField] List<CommandData> m_commandDatas = new List<CommandData>();
    [SerializeField] List<CommandButton> m_commandButtons = new List<CommandButton>();
    [SerializeField] List<MacroCommand> m_macroCommands = new List<MacroCommand>();

    public void InitCommand()
    {
        for(int i = 0; i < m_commandButtons.Count; i++)
        {
            m_commandButtons[i].Init(m_commandDatas[i]);
        }

        foreach(var v in m_macroCommands)
        {
            v.Init();
        }
    }
}
