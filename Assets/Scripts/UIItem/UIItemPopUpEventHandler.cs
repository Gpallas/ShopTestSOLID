using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemPopUpEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, ISelectAction, IDeselectHandler, IDeselectAction
{
    Action onSelectAction;
    Action onDeselectAction;

    //Tem que fazer o mouse e o controle funcionarem do mesmo jeito.
    //onpointerenter do mouse e onselect no controle tem que abrir o popup
    //onpointerexit do mouse e ondeselect no controle tem que fechar o popup
    //Tem que ter duas maneiras distintas de comprar itens da loja, que pensando bem talvez precise ficar em outra classe, porque se aplica só ao buy/sell
    //Uma das maneiras só compra uma unidade e a outra compra unidades enquanto tiver no hold do botão (tanto no mouse quanto no controle)
    //No mouse, se o cursor sai do item, ele para de comprar, mas não compra o do outro item. Se o cursor voltar, ele continua comprando.
    //No controle, se o cursor sai do item, ele imediatamente começa a comprar do outro item.

    public void OnDeselect(BaseEventData eventData)
    {
        //Call Pop Up Hide
        onDeselectAction?.Invoke();
        Debug.Log(gameObject.name + " Deselected");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null, eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Call Pop Up Construction
        onSelectAction?.Invoke();
        Debug.Log(gameObject.name + " Selected");
    }

    public void AddOnSelectAction(Action actionToTrigger)
    {
        onSelectAction += actionToTrigger;
    }

    public void AddOnDeselectAction(Action actionTotrigger)
    {
        onDeselectAction += actionTotrigger;
    }

    void OnDestroy()
    {
        onDeselectAction?.Invoke();
    }
}
