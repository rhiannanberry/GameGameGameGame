using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColor : MonoBehaviour
{
    private List<Color> _colors;
    public Color _scoreColor;

    public void Reset()
    {
        _colors = new List<Color> {Color.red, Color.blue, Color.green,
                                   Color.yellow};
        _scoreColor = _colors[(int) (Random.value * 4)];

        for (int i = 0; i < 4; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            int pick = (int) (Random.value * _colors.Count);
            go.GetComponent<Renderer>().material.color = _colors[pick];
            _colors.RemoveAt(pick);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
}
