using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform _parentMenu = null;
    [SerializeField] private TransitionData _menuTransition = null;
    [SerializeField] private Button _back = null;

    // Start is called before the first frame update
    void Start()
    {
        _back.onClick.AddListener(CloseCredits);
    }

    private void CloseCredits() {
        _parentMenu.gameObject.SetActive(true);

        StartCoroutine( _menuTransition.Transition(
            (t) => {
                GetComponent<RectTransform>().localScale = Vector2.one - Vector2.one * t ;
                _parentMenu.localScale = Vector2.one * t ;
            },
            (complete) => {
                if (!complete) return;
                _back.interactable = true;
                gameObject.SetActive(false);
            }
        ));
    }
}
