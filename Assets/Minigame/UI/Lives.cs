using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MinigameBehaviour
{
    private TextMeshProUGUI _livesUI;

    protected override void Start() {
        base.Start();
        _livesUI = GetComponent<TextMeshProUGUI>();
        _livesUI.text = "Lives: " + PersistentDataManager.run.Lives;
    }

    protected override void OnStateEnter() {}
    
    protected override void OnStateExit() {
        _livesUI.text = "Lives: " + PersistentDataManager.run.Lives;
    }
}
