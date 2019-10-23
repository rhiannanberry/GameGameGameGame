using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameButton : System.Object
{
    private Toggle _toggle;
    private Button _selectedButton;
    private Minigame _minigame;
    private bool _selected = false;

    private string _filterAuthor, _filterTag = "All";

    public bool Selected {get {return _selected;}}
    public Minigame MiniGame {get {return _minigame;}}
    public MinigameButton(GameObject toggle, GameObject selectedButton, Minigame minigame) {
        _toggle = toggle.GetComponent<Toggle>();
        _selectedButton = selectedButton.GetComponent<Button>();

        _minigame = minigame;

        SetProperties();
    }

    private void SetProperties() {
        TextMeshProUGUI t = _toggle.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI selectedT = _selectedButton.GetComponentInChildren<TextMeshProUGUI>();

        t.text = selectedT.text = _minigame.Name;

        _toggle.interactable = _minigame.GameWon;
        
        _toggle.onValueChanged.AddListener((value) => {
            _selectedButton.gameObject.SetActive(value);
            _selected = value;
            if (value) _selectedButton.transform.SetAsLastSibling();
            });
       
        _selectedButton.onClick.AddListener(() => _toggle.isOn = false);

        _selectedButton.gameObject.SetActive(false);
    }

    public void SetInteractable() {
        _toggle.interactable = true;
    }

    public void FilterByAuthor(string author) {
        _filterAuthor = author;

        bool authorMatch = _filterAuthor == "All" || _filterAuthor == _minigame.Author;
        bool tagMatch = _filterTag == "All";
                
        _toggle.gameObject.SetActive(authorMatch && tagMatch);
    }
}
