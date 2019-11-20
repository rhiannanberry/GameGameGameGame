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

        StartCoroutine(_menuTransition.StartTransitionScale(GetComponent<RectTransform>(), false, 
            complete => {
                if (complete) {
                    _back.interactable = true;
                    gameObject.SetActive(false);
                }
        }));

        StartCoroutine(_menuTransition.StartTransitionScale(_parentMenu, true, complete => {}));
    }
}
