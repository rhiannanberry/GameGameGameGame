using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarButtonsBehaviour : MonoBehaviour
{
    public ScrollRect _sr;
    private Scrollbar _sb;

    private float _moveValue;

    private void Start() {
        _sb = GetComponent<Scrollbar>();
    }

    private void Update() {
        if (_moveValue > 0) {
            _moveValue -= 3*_sr.decelerationRate * Time.deltaTime;
            _moveValue = Mathf.Clamp01(_moveValue);
        } else if (_moveValue < 0) {
            _moveValue += 3*_sr.decelerationRate * Time.deltaTime;
            _moveValue = Mathf.Clamp(_moveValue, -1f, 0);
        }

        _sb.value += _moveValue * Time.deltaTime;
        //_sb.value = Mathf.Clamp01(_sb.value);
    }

    public void MoveScrollbarLeft() {
        _moveValue = -1f;
    }
    public void MoveScrollbarRight() {
        _moveValue = 1f;
    }
}
