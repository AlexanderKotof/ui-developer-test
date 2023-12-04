using UnityEngine;
using UnityEngine.UI;

namespace UIDeveloperTest.Common
{
    public class TabsController : MonoBehaviour
    {
        [SerializeField] private Toggle[] toggles;
        [SerializeField] private TabComponent[] tabs;

        private void OnValidate()
        {
            Debug.Assert(toggles.Length == tabs.Length);
        }

        private void OnEnable()
        {
            InitTogglesCallbacks();
            ActivateTab(true, 0);
        }
        private void OnDisable()
        {
            ClearTogglesCallbacks();
        }

        private void InitTogglesCallbacks()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                var toggle = toggles[i];
                int index = i;
                toggle.onValueChanged.AddListener((isOn) =>
                {
                    ActivateTab(isOn, index);
                });
            }
        }
        private void ClearTogglesCallbacks()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                var toggle = toggles[i];
                toggle.onValueChanged.RemoveAllListeners();
            }
        }

        private void ActivateTab(bool isOn, int index)
        {
            if (!isOn)
                return;

            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i].ShowHide(index == i);
            }
        }
    }
}
