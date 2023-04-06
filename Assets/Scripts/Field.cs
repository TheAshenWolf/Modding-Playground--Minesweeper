using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Field : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [HideInInspector] public UnityEvent onReveal;
    [HideInInspector] public UnityEvent onFlag;
    
    public bool isMine;
    public bool isRevealed;
    public bool isFlagged;
    public int adjacentMines;
    
    public Image graphic;
    public Color defaultColor = Color.white;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        graphic.color = hoverColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.gameFinished) return;
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                onReveal?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                onFlag?.Invoke();
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        graphic.color = defaultColor;
    }
}