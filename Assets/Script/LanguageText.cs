using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LanguageText : MonoBehaviour
{
    public string key; // Identifiant de traduction
    private TMP_Text textComponent;

    private static List<LanguageText> allTexts = new List<LanguageText>();

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        allTexts.Add(this);
        UpdateText(); // Mise à jour dès le début
    }

    void OnDestroy()
    {
        allTexts.Remove(this);
    }

    public void UpdateText()
    {
        if (LanguageManager.Instance != null)
        {
            string translated = LanguageManager.Instance.GetTranslation(key);
            //Debug.Log($"UpdateText clé='{key}' traduction='{translated}'");
            textComponent.text = translated;
        }
    }

    public static void UpdateAllTexts()
    {
        foreach (var t in allTexts)
        {
            t.UpdateText();
        }
    }
}
