using UnityEngine;

namespace UIDeveloperTest.Common
{
    public class TabComponent : MonoBehaviour
    {
        [SerializeField] private Behaviour[] _behavioursToDisable;

        public void ShowHide(bool value)
        {
            for (int i = 0; i < _behavioursToDisable.Length; i++)
            {
                _behavioursToDisable[i].enabled = value;
            }            
        }
    }
}