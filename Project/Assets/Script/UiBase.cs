using UnityEngine;
using UnityEngine.UI;

public class UiBase : MonoBehaviour
{
    RectTransform m_rect;
    RectTransform Rect
    {
        get
        {
            if (m_rect == null) m_rect = this.GetComponent<RectTransform>();
            return m_rect;
        }
    }

    public Vector2 Position
    {
        get
        {
            return Rect.anchoredPosition;
        }
        set
        {
            Rect.anchoredPosition = value;
        }
    }
}
