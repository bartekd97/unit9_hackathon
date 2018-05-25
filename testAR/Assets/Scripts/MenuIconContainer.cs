using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconContainer : MonoBehaviour {
    public enum IconType
    {
        Active,
        Disabled,
        Hover
    }
    public Sprite activeIcon;
    public Sprite disabledIcon;
    public Sprite hoverIcon;
    public UnityEngine.UI.Image image;

    public Interactable interactable;

	void Start () {
        SetIconType(IconType.Active);
	}
	
    public void SetIconType( IconType type )
    {
        switch (type)
        {
            case IconType.Active:
                image.sprite = activeIcon;
                break;
            case IconType.Disabled:
                image.sprite = disabledIcon;
                break;
            case IconType.Hover:
                image.sprite = hoverIcon;
                break;
            default:
                break;
        }
        if (interactable)
        {
            if (type == IconType.Disabled)
                interactable.isInteractable = false;
            else
                interactable.isInteractable = true;
        }
    }
}
