using UnityEngine;
using UnityEngine.UI;

namespace UIDeveloperTest.Tab_2
{
    /// <summary>
    /// Improved and refactored solution from https://github.com/boonyifei/ScrollList/tree/master
    /// </summary>
    public class OptimizedList : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _viewport;

        [SerializeField] private ListItem _itemPrefab;

        [SerializeField] private int _itemsCount = 1;
        [SerializeField] private float _spacing;
        [SerializeField] private ListDirection _direction = ListDirection.HORIZONTAL;

        [SerializeField] private ScrollRect _scrollRect;

        private enum ListDirection
        {
            HORIZONTAL = 0,
            VERTICAL = 1
        }

        private int _visibleItemsCount;

        private float _itemSize;

        private ListItem[] _items;

        private int _realItemsCount;
        private Vector3 _startPos;

        private const int _bufferItemsCount = 2;
        private const int _halfBufferElements = _bufferItemsCount / 2;
        private Vector3 _movementDirection => _direction == ListDirection.HORIZONTAL ? Vector3.right : Vector3.down;

        void Start()
        {
            InitializeList();
        }

        private void OnEnable()
        {
            _scrollRect.onValueChanged.AddListener(OnScroll);
        }

        private void OnDisable()
        {
            _scrollRect.onValueChanged.RemoveAllListeners();
        }

        private void InitializeList()
        {
            // item size calculation
            var itemRectSize = _itemPrefab.RectTransform.rect.size;
            _itemSize = (_direction == ListDirection.HORIZONTAL ? itemRectSize.x : itemRectSize.y) + _spacing;

            // container size calculation 
            _container.anchoredPosition3D = Vector3.zero;
            _container.sizeDelta = _direction == ListDirection.HORIZONTAL ? new Vector2(_itemSize * _itemsCount, itemRectSize.y) : new Vector2(itemRectSize.x, _itemSize * _itemsCount);

            // items start position
            var containerHalfSize = _direction == ListDirection.HORIZONTAL ? _container.rect.size.x * 0.5f : _container.rect.size.y * 0.5f;
            _startPos = _container.anchoredPosition3D - _movementDirection * containerHalfSize + _movementDirection * ((_direction == ListDirection.HORIZONTAL ? itemRectSize.x : itemRectSize.y) * 0.5f);

            // optimized items count calculation
            _visibleItemsCount = Mathf.FloorToInt((_direction == ListDirection.HORIZONTAL ? _viewport.rect.size.x : _viewport.rect.size.y) / _itemSize);
            _realItemsCount = Mathf.Min(_itemsCount, _visibleItemsCount + _bufferItemsCount);

            // instantiating items
            _items = new ListItem[_realItemsCount];

            for (int i = 0; i < _realItemsCount; i++)
            {
                var item = Instantiate(_itemPrefab, _container.transform);

                item.RectTransform.anchoredPosition3D = _startPos + _movementDirection * i * _itemSize;
                item.UpdateContent(i);

                _items[i] = item;
            }

            _scrollRect.horizontal = _direction == ListDirection.HORIZONTAL;
            _scrollRect.vertical = _direction == ListDirection.VERTICAL;
        }

        private void OnScroll(Vector2 scroll)
        {
            ReorderItemsByPosition(_direction == ListDirection.HORIZONTAL ? scroll.x : scroll.y);
        }

        public void ReorderItemsByPosition(float normPos)
        {
            if (_direction == ListDirection.VERTICAL)
                normPos = 1f - normPos;

            //number of elements beyond the left boundary (or top)
            int numOutOfView = Mathf.RoundToInt(normPos * (_itemsCount - _visibleItemsCount));

            //index of first element beyond the left boundary (or top)
            int firstIndex = Mathf.Max(0, numOutOfView - _halfBufferElements);
            int originalIndex = firstIndex % _realItemsCount;

            int newIndex = firstIndex;

            for (int i = originalIndex; i < _realItemsCount; i++)
            {
                // do not move items if we reach the end of list
                if (newIndex >= _itemsCount)
                    break;

                MoveItemByIndex(i, newIndex);
                newIndex++;
            }

            for (int i = 0; i < originalIndex; i++)
            {
                // do not move items if we reach the end of list
                if (newIndex >= _itemsCount)
                    break;

                MoveItemByIndex(i, newIndex);
                newIndex++;
            }
        }

        private void MoveItemByIndex(int itemIndex, int realIndex)
        {
            _items[itemIndex].RectTransform.anchoredPosition3D = _startPos + _movementDirection * realIndex * _itemSize;
            _items[itemIndex].UpdateContent(realIndex);
        }
    }
}