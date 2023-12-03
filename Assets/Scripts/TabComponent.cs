using System.Collections.Generic;
using UnityEngine;

public class TabComponent : MonoBehaviour
{
    [SerializeField] private List<Behaviour> _behavioursToDisable;

    public void ShowHide(bool value)
    {
        _behavioursToDisable.ForEach((component) => component.enabled = value);
    }
    public void Show()
    {
        ShowHide(true);
    }
    public void Hide()
    {
        ShowHide(false);
    }
}
