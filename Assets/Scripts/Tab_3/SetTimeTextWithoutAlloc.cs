using System;
using TMPro;
using UnityEngine;

namespace UIDeveloperTest.Tab_3
{
    public class SetTimeTextWithoutAlloc : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private readonly char[] _charArray = new char[CharArrayUtils.RequiredArrayLength];

        private void Start()
        {
            SetTimeText();
        }
        private void Update()
        {
            SetTimeText();
        }

        private void SetTimeText()
        {
            CharArrayUtils.SetCharArrayFromDateTime(_charArray, DateTime.Now);

            // Note: this line will allocate in Editor only because of updating m_text field (for inspector update reason) 
            _text.SetCharArray(_charArray);
        }
    }
}
