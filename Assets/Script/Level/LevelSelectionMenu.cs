using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    public LevelsList levelsList;       // La liste des niveaux
    public GameObject buttonPrefab;     // Bouton � instancier
    public Transform contentParent;     // Parent o� placer les boutons

    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        foreach (LevelData level in levelsList.levels)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentParent);
            Button btn = buttonObj.GetComponent<Button>();
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = level.levelName;

            btn.onClick.AddListener(() => LoadLevel(level.sceneName));
        }
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
