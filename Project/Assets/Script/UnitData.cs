using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public Sprite m_img;
    public int m_hp = 3;
    public List<CommandData> m_commands;
}
