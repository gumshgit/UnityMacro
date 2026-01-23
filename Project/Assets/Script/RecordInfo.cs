using UnityEngine;
using UnityEngine.UI;
using GmshLib;

public class RecordInfo : UiBase
{
    [SerializeField] Image m_img;

    public static RecordInfo Create(CommandData command)
    {
        GameObject obj = PrefabManager.CreateObj(PrefabDef.RecordInfo);
        var v = obj.GetComponent<RecordInfo>();
        v.Init(command);
        return v;
    }

    public void Init(CommandData command)
    {
        m_img.sprite = command.m_img;
    }
}
