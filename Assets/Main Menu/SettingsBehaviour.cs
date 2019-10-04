using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Slider _masterSlider = null;
    [SerializeField] private Slider _musicSlider = null;
    [SerializeField] private Slider _sfxSlider = null;

    [Header("Navigation")]
    [SerializeField] private TransitionData _menuTransition = null;
    [SerializeField] private RectTransform _parentMenu = null;
    [SerializeField] private RectTransform _saveSettingsPanel = null;

    [Header("Buttons")]
    [SerializeField] private Button _back = null;
    [SerializeField] private Button _save = null, _dontSave = null, _cancel = null;

    private bool _initialized = false;
    private PlayerSettings _oldSettings;

    public void Start() {
        _masterSlider.onValueChanged.AddListener(MasterVolumeChange);
        _musicSlider.onValueChanged.AddListener(MusicVolumeChange);
        _sfxSlider.onValueChanged.AddListener(SFXVolumeChange);

        _back.onClick.AddListener(TryCloseSettings);
        _save.onClick.AddListener(Save);
        _dontSave.onClick.AddListener(DontSave);
        _cancel.onClick.AddListener(Cancel);
    }

    public void OnEnable() {
        OpenSettings();
    }

    public void OpenSettings() {
        LoadValues();
        _oldSettings = PersistentDataManager.playerSettings.Copy();
    }

    private void TryCloseSettings() {
        _back.interactable = false;
        bool savable = true;

        #if !UNITY_STANDALONE && !UNITY_EDITOR
        savable = false;
        #endif

        bool settingsChanged = !_oldSettings.Equals(PersistentDataManager.playerSettings);
        if (settingsChanged && savable) {
            AskSave();
        } else {
            CloseSettings();
        }
    }

    private void AskSave() {
        _saveSettingsPanel.gameObject.SetActive(true);
    }

    private void Save() {
        PersistentDataManager.INSTANCE.SavePlayerSettings();
        CloseSettings();
    }

    private void DontSave() {
        LoadValues();
        CloseSettings();
    }

    private void CloseSettings() {
        _saveSettingsPanel.gameObject.SetActive(false);

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

    private void Cancel() {
        _back.interactable = true;
        _saveSettingsPanel.gameObject.SetActive(false);
    }

    private void LoadValues() {
        PersistentDataManager.INSTANCE.LoadPlayerSettings();
        _masterSlider.value = PersistentDataManager.playerSettings.MasterVolume;
        _musicSlider.value = PersistentDataManager.playerSettings.MusicVolume;
        _sfxSlider.value = PersistentDataManager.playerSettings.SFXVolume;
    }

    private void MasterVolumeChange(float sliderValue) {
        PersistentDataManager.playerSettings.MasterVolume = sliderValue;
    }

    private void MusicVolumeChange(float sliderValue) {
        PersistentDataManager.playerSettings.MusicVolume = sliderValue;
    }

    private void SFXVolumeChange(float sliderValue) {
        PersistentDataManager.playerSettings.SFXVolume = sliderValue;
    }
}
