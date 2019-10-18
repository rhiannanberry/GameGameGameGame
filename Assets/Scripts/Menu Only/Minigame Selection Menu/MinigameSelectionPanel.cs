using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MinigameSelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab = null;
    [SerializeField] private TMP_Dropdown _authorDropdown = null;

    private void Start() {
        DestroyExampleButtons();
        GenerateMinigameButtons();
        _authorDropdown.AddOptions(PersistentDataManager.minigameMasterList.Authors().Keys.ToList());
    }

    private void DestroyExampleButtons() {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void GenerateMinigameButtons() {
        bool first = true;
        foreach(Minigame m in PersistentDataManager.minigameMasterList.minigames) {
            GameObject go = Instantiate(_buttonPrefab, transform);
            Button b = go.GetComponent<Button>();
            TextMeshProUGUI t = go.GetComponentInChildren<TextMeshProUGUI>();

            if (first || m.GamePlayed) {
                t.text = m.Name;
            } else {
                t.text = "???";
            }

            b.onClick.AddListener(() => ExitingMainMenu.StartNewRun(m));

            if (!m.GameWon && !first) {
                b.interactable = false;
            }
            
            first = false;
        }
    }
}
