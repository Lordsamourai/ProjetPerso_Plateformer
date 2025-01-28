using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederController : MonoBehaviour
{
    public float speedBoost = 10f;
    public float boostDuration = 3f;

    private Material alertMaterial; // Mat�riau rouge quand activ�
    private Material originalMaterial; // Mat�riau d'origine
    private Renderer speederRenderer;

    private void Start()
    {
        speederRenderer = GetComponent<Renderer>();

        // Sauvegarde du mat�riau d'origine
        if (speederRenderer != null)
        {
            originalMaterial = speederRenderer.material;
        }

        // Cr�ation d'un mat�riau rouge
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

        // Attendre la dur�e du boost
        yield return new WaitForSeconds(boostDuration);

        // R�initialiser la vitesse
        player.moveSpeed -= speedBoost;

        // Restaurer le mat�riau d'origine
        mats[1] = originalMaterial;
        speederRenderer.materials = mats;
    }
}
