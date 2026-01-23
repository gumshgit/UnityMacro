using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CommandData", menuName = "Scriptable Objects/CommandData")]
public class CommandData : ScriptableObject
{
    public string m_name;
    public Sprite m_img;
    public CommandType m_commandType;
    public int m_option1 = 0;

    public List<int> m_intList = new List<int>();
}
