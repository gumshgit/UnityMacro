using UnityEngine;
using GmshLib;

public class Cell : MonoBehaviour
{

    [SerializeField, NonEditable] int m_pos = 0;
    public int Position => m_pos;

    public static Cell Create(int pos)
    {
        GameObject obj = PrefabManager.CreateObj(PrefabDef.Cell);
        var v = obj.GetComponent<Cell>();
        v.Init(pos);
        return v;
    }

    public void Init(int pos)
    {
        m_pos = pos;
    }
}
