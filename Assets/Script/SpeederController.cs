using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederController : MonoBehaviour
{
    public float speedBoost = 10f;
    public float boostDuration = 3f;

    private Material alertMaterial; // Matériau rouge quand activé
    private Material originalMaterial; // Matériau d'origine
    private Renderer speederRenderer;

    private void Start()
    {
        speederRenderer = GetComponent<Renderer>();

        // Sauvegarde du matériau d'origine
        if (speederRenderer != null)
        {
            originalMaterial = speederRenderer.material;
        }

        // Création d'un matériau rouge
        alertMaterial = new Material(Shader.Find("Standard"));
        alertMaterial.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeplacementPersonnage player = other.GetComponent<DeplacementPersonnage>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
        }
    }

    private IEnumerator ApplySpeedBoost(DeplacementPersonnage player)
    {
        Material[] mats = speederRenderer.materials;
        originalMaterial = mats[1];
        mats[1] = alertMaterial;
        // Changer la couleur du Speeder en rouge
        speederRenderer.materials = mats;

        // Augmenter la vitesse du joueur
        player.moveSpeed += speedBoost;

        // Attendre la durée du boost
        yield return new WaitForSeconds(boostDuration);

        // Réinitialiser la vitesse
        player.moveSpeed -= speedBoost;

        // Restaurer le matériau d'origine
        mats[1] = originalMaterial;
        speederRenderer.materials = mats;
    }
}
