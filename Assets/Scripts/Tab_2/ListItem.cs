using TMPro;
using UnityEngine;

namespace UIDeveloperTest.Tab_2
{
    public class ListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemLabel;
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;

        private const string _labelFormat = "Item_{0}";

        public void UpdateContent(int newIndex)
        {
            _itemLabel.SetText(string.Format(_labelFormat, newIndex));
        }
    }
}