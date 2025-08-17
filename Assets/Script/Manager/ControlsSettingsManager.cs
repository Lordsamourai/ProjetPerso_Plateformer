using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlsSettingsManager : MonoBehaviour
{
    public Button rebindJumpButton;
    public Button rebindForwardButton;
    public Button rebindBackwardButton;
    public Button rebindLeftButton;
    public Button rebindRightButton;

    private KeyCode jumpKey;
    private KeyCode forwardKey;
    private KeyCode backwardKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode echapKey;

    private bool isQwertyLayout;

    void Start()
    {
        // On choisit le layout ici, QWERTY par défaut (false = AZERTY)
        isQwertyLayout = PlayerPrefs.GetInt("KeyboardLayout", 0) == 0;
        SetLayout(isQwertyLayout);

        UpdateButtonTexts();

        // On désactive les boutons de rebinding (optionnel)
        rebindJumpButton.interactable = false;
        rebindForwardButton.interactable = false;
        rebindBackwardButton.interactable = false;
        rebindLeftButton.interactable = false;
        rebindRightButton.interactable = false;
    }

    public void SetLayout(bool useQwerty)
    {
        isQwertyLayout = useQwerty;

        Debug.Log(isQwertyLayout);
        if (isQwertyLayout == false)
        {
            forwardKey = KeyCode.W;
            leftKey = KeyCode.A;
        }
        else
        {
            forwardKey = KeyCode.Z;
            leftKey = KeyCode.Q;
        }

        backwardKey = KeyCode.S;
        rightKey = KeyCode.D;
        jumpKey = KeyCode.Space;
        echapKey = KeyCode.Escape;

        UpdateButtonTexts();
    }

    private void UpdateButtonTexts()
    {
        KeyCode forwardKeyLoc, leftKeyLoc;
        if (isQwertyLayout == true)
        {
            forwardKeyLoc = KeyCode.W;
            leftKeyLoc = KeyCode.A;
        }
        else
        {
            forwardKeyLoc = KeyCode.Z;
            leftKeyLoc = KeyCode.Q;
        }
        rebindJumpButton.GetComponentInChildren<TextMeshProUGUI>().text = jumpKey.ToString();
        rebindForwardButton.GetComponentInChildren<TextMeshProUGUI>().text = forwardKeyLoc.ToString();
        rebindBackwardButton.GetComponentInChildren<TextMeshProUGUI>().text = backwardKey.ToString();
        rebindLeftButton.GetComponentInChildren<TextMeshProUGUI>().text = leftKeyLoc.ToString();
        rebindRightButton.GetComponentInChildren<TextMeshProUGUI>().text = rightKey.ToString();
    }

    // Getters pour récupérer les touches
    public KeyCode GetJumpKey() => jumpKey;
    public KeyCode GetForwardKey() => forwardKey;
    public KeyCode GetBackwardKey() => backwardKey;
    public KeyCode GetLeftKey() => leftKey;
    public KeyCode GetRightKey() => rightKey;
    public KeyCode GetEchap() => echapKey;
}
