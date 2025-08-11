using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Entry
{
    public string key;
    public string value;
}

[System.Serializable]
public class LanguageEntry
{
    public string code;
    public List<Entry> entries;
}

[System.Serializable]
public class LanguageFile
{
    public List<LanguageEntry> languages;
}

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    private Dictionary<string, Dictionary<string, string>> allLanguages = new();
    private Dictionary<string, string> currentLanguage;

    public TMP_Dropdown dropdown;

    public delegate void LanguageChanged(string newLangCode);
    public static event LanguageChanged OnLanguageChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguages();

            string savedLang = PlayerPrefs.GetString("language", "en");
            ApplyLanguage(savedLang);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (dropdown != null)
        {
            string lang = PlayerPrefs.GetString("language", "en");
            dropdown.value = (lang == "fr") ? 1 : 0;
            dropdown.onValueChanged.AddListener(OnDropdownChanged);
        }
        else
        {
            Debug.LogWarning("TMP_Dropdown non assigné !");
        }
    }

    void LoadLanguages()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("languages");
        if (jsonFile == null)
        {
            Debug.LogError("Fichier languages.json introuvable dans Resources !");
            return;
        }

        LanguageFile file = JsonUtility.FromJson<LanguageFile>(jsonFile.text);
        if (file?.languages == null)
        {
            Debug.LogError("Erreur de désérialisation ou languages vide !");
            return;
        }

        foreach (var lang in file.languages)
        {
            var dict = new Dictionary<string, string>();
            foreach (var entry in lang.entries)
                dict[entry.key.ToLower()] = entry.value;
            allLanguages[lang.code] = dict;
        }
    }

    public void ApplyLanguage(string langCode)
    {
        if (!allLanguages.ContainsKey(langCode))
        {
            Debug.LogWarning($"Langue non trouvée : {langCode}");
            return;
        }

        currentLanguage = allLanguages[langCode];
        PlayerPrefs.SetString("language", langCode);
        PlayerPrefs.Save();

        LanguageText.UpdateAllTexts();
        OnLanguageChanged?.Invoke(langCode);
    }

    public string GetTranslation(string key)
    {
        key = key.ToLower();
        return currentLanguage != null && currentLanguage.TryGetValue(key, out var val) ? val : key;
    }

    public void OnDropdownChanged(int index)
    {
        string langCode = index == 0 ? "en" : "fr";
        ApplyLanguage(langCode);
    }
}
