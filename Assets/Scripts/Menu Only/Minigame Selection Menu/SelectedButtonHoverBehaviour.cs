using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class SelectedButtonHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Color _color;
    [SerializeField] private TextMeshProUGUI _text = null;
    void Start()
    {
        _button = GetComponent<Button>();
        //_text.GetComponent<CanvasRenderer>().SetAlpha(0f);
        
    }

    void OnEnable() {
        _text.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        
        _text.CrossFadeAlpha(1.0f, _button.colors.fadeDuration, true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _text.CrossFadeAlpha(0.0f, _button.colors.fadeDuration, true);
    }
}
