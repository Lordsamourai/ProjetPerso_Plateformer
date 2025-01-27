using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // N�cessaire pour charger des sc�nes

public class Teleporter : MonoBehaviour
{
    public Etat etat;  // �tat du t�l�porteur
    public GameObject[] cubes;  // Liste des cubes � changer de couleur
    public int currentLevel = 1;  // Le niveau actuel du joueur
    // Start is called before the first frame update
    void Start()
    {
        // Change la couleur des cubes en fonction de l'�tat
        ChangeCubeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fonction qui change la couleur des cubes
    void ChangeCubeColor()
    {
        // Parcours chaque cube et change sa couleur en fonction de l'�tat
        foreach (GameObject cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();

            if (cubeRenderer != null)
            {
                if (etat == Etat.Active)
                {
                    cubeRenderer.material.color = Color.blue;  // Cube bleu si actif
                }
                else if (etat == Etat.Desactive)
                {
                    cubeRenderer.material.color = Color.red;  // Cube rouge si d�sactiv�
                }
            }
        }

        if (etat == Etat.Desactive)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Charger la sc�ne suivante automatiquement
            currentLevel++;  // Augmente le num�ro de niveau
            SceneManager.LoadScene("Niveau" + currentLevel);  // Charger la sc�ne suivante selon le num�ro de niveau
        }
    }
}

public enum Etat
{
    Active,
    Desactive,
}