using UnityEngine;
using UnityEngine.UI;
using GmshLib;

public class CommandButton : ButtonBase
{
    //[SerializeField] Text m_text;
    [SerializeField] Image m_img;

    [SerializeField, NonEditable]
    CommandData m_commandData;

    public void Init(CommandData commandData)
    {
        m_commandData = commandData;
        //m_text.text = m_commandData.m_name;
        m_img.sprite = commandData.m_img;
    }

    protected override void OnClick()
    {
        base.OnClick();
        GameWork.Instance.SetCommand(m_commandData);
    }
}
