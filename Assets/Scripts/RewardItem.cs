using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public event Action onGoldSpawned;
    public event Action onGoldRemoved;


    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    private Transform _originalParent;
    private Vector3 _originalPosition;
    private CanvasGroup _canvasGroup;

    

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        onGoldSpawned?.Invoke();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = _rectTransform.parent;
        _originalPosition = _rectTransform.localPosition;

        _rectTransform.SetParent(_mainCanvas.transform, true); // Переносим золото на верхний уровень
        _canvasGroup.blocksRaycasts = false; // Отключаем возможность перехвата событий
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var uibag = FindObjectOfType<UIBag>();
        if (_rectTransform.parent != uibag.transform)
        {
            Debug.Log("Золото вернулось на клетку!");
            _rectTransform.SetParent(_originalParent, true);
            _rectTransform.localPosition = _originalPosition;
            _canvasGroup.blocksRaycasts = true;
        }
        else
        {
            Debug.Log("Золото преемещено в сумку из золота");
            onGoldRemoved?.Invoke();
            Destroy(gameObject);
        }

        
    }
}