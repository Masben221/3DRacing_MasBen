using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private UnityEngine.GameObject selectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public override void SetFocuse()
    {
        base.SetFocuse();

        selectImage.SetActive(true);
        OnSelect?.Invoke();
    }
    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        selectImage.SetActive(false);
        OnUnSelect?.Invoke();
    }
}
