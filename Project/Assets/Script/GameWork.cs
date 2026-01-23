using UnityEngine;
using GmshLib;
using System.Collections.Generic;

public class GameWork : SingletonMonoBehaviour<GameWork>
{
    List<CommandData> m_selectCommands = new List<CommandData>();
    public List<CommandData> SelectCommands => m_selectCommands;
    public void SetCommands(List<CommandData> commands)
    {
        m_selectCommands = new List<CommandData>(commands);
    }
    public void SetCommand(CommandData command)
    {
        m_selectCommands.Add(command);
    }
    public void ClearSelectCommands()
    {
        m_selectCommands.Clear();
    }
    public bool IsSelectCommand() { return m_selectCommands.Count > 0; }
}
