using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance { get; private set; }

    [Header("UI Elements")]
    public Slider brightnessSlider;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown keyboardLayoutDropdown;
    //public Slider mouseSensitivitySlider;

    [Header("Sub Managers")]
    public LanguageManager languageManager;
    public ControlsSettingsManager controlsManager;

    [Header("Other")]
    public AudioMixer audioMixer;
    public Image brightnessOverlay;

    public GameObject generalPanel;
    public GameObject controlPanel;
    public Button generalButton;
    public Button controlButton;

    private Resolution[] availableResolutions;
    public float mouseSensitivity = 1f; // Valeur de base

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Listeners
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        fullscreenDropdown.onValueChanged.AddListener(SetFullscreenFromDropdown);
        resolutionDropdown.onValueChanged.AddListener(SetResolutionFromDropdown);
        keyboardLayoutDropdown.onValueChanged.AddListener(SetKeyboardLayout);

        // Fullscreen
        fullscreenDropdown.value = Screen.fullScreen ? 0 : 1;

        // Resolutions
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        // Clavier
        keyboardLayoutDropdown.ClearOptions();
        keyboardLayoutDropdown.options.Add(new TMP_Dropdown.OptionData("QWERTY"));
        keyboardLayoutDropdown.options.Add(new TMP_Dropdown.OptionData("AZERTY"));

        int savedLayout = PlayerPrefs.GetInt("KeyboardLayout", 0);
        keyboardLayoutDropdown.value = savedLayout;
        keyboardLayoutDropdown.RefreshShownValue();

        // Appliquer layout
        controlsManager.SetLayout(savedLayout == 0);

        // UI panels
        generalPanel.SetActive(true);
        controlPanel.SetActive(false);
        generalButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        controlButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void SetKeyboardLayout(int index)
    {
        Debug.Log("Change keyboard type: " + index);
        PlayerPrefs.SetInt("KeyboardLayout", index);
        PlayerPrefs.Save();

        // Appliquer le layout choisi
        bool isQwerty = (index == 0);
        print(isQwerty);
        controlsManager.SetLayout(isQwerty);
    }

    public void SetBrightness(float value)
    {
        Color c = brightnessOverlay.color;
        c.a = 1f - value;
        brightnessOverlay.color = c;
    }

    private void SetFullscreenFromDropdown(int index)
    {
        Screen.fullScreen = (index == 0);
    }

    private void SetResolutionFromDropdown(int index)
    {
        Resolution res = availableResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    private void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();
    }

    public void ShowGeneralPanel()
    {
        generalPanel.SetActive(true);
        controlPanel.SetActive(false);
        generalButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        controlButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void HideGeneralPanel()
    {
        generalPanel.SetActive(false);
        controlPanel.SetActive(true);
        generalButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        controlButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
