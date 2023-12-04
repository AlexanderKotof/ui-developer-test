using System.Collections.Generic;
using UnityEngine;

namespace UIDeveloperTest.Common
{
    public class TabComponent : MonoBehaviour
    {
        [SerializeField] private List<Behaviour> _behavioursToDisable;

        public void ShowHide(bool value)
        {
            _behavioursToDisable.ForEach((component) => component.enabled = value);
        }
    }
}