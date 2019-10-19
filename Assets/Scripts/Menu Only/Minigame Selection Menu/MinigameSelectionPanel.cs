using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MinigameSelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab = null;
    [SerializeField] private GameObject _selectedButtonPrefab = null;

    [SerializeField] private TMP_Dropdown _authorDropdown = null;

    [SerializeField] private Transform _selectedContainer = null;
    [SerializeField] private Button _goButton = null;

    private List<MinigameButton> _buttons;
    private Dictionary<string, bool> _authors;

    private void Start() {
        List<string> authors = PersistentDataManager.minigameMasterList.Authors().Keys.ToList();
        _authors = authors.ToDictionary(k => k, v => false);

        DestroyExampleButtons();
        GenerateMinigameButtons();

        authors.Insert(0, "All");

        _authorDropdown.AddOptions(authors);
        _authorDropdown.onValueChanged.AddListener((value) => _buttons.ForEach(b => b.FilterByAuthor(authors[value])));
    }

    private void Update() {
        _goButton.interactable = _selectedContainer.GetComponentsInChildren<Transform>().GetLength(0) > 1;
    }

    private void DestroyExampleButtons() {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void GenerateMinigameButtons() {        
        _buttons = new List<MinigameButton>();

        foreach(Minigame m in PersistentDataManager.minigameMasterList.minigames) {
            MinigameButton mb = new MinigameButton(Instantiate(_buttonPrefab, transform), Instantiate(_selectedButtonPrefab, _selectedContainer), m);
            if (_authors[m.Author] == false) {
                mb.SetInteractable();
                _authors[m.Author] = true;
            }            
            _buttons.Add(mb);
        }
    }

    public void StartRun() {
        List<Minigame> selected = _buttons.Where(x => x.Selected).Select(x => x.MiniGame).ToList();
        PersistentDataManager.INSTANCE.CreateNewRun((new MinigameList(selected)).RandomReorder());
        TransitionController.BeginExitToScene(PersistentDataManager.run.CurrentGame.SceneName);
    }
}
