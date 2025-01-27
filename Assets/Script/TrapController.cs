using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public string playerName = "Player";
    public float detectionRange = 4f; // Distance maximale pour détecter le joueur
    public float openDelay = 2f; // Temps avant que la trappe s'ouvre
    public float closeDelay = 1f; // Temps avant que la trappe se referme
    public float rotationAngle = 60f; // Angle de rotation sur l'axe X
    public float rotationDuration = 0.5f; // Durée de l'animation de rotation

    private Transform player; // Référence au joueur
    private Quaternion initialRotation; // Rotation de départ
    private Quaternion openRotation; // Rotation lorsqu'elle est ouverte
    private bool isTriggered = false; // Empêche plusieurs activations simultanées

    private void Start()
    {
        GameObject playerObject = GameObject.Find(playerName);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        // Sauvegarde la rotation initiale
        initialRotation = transform.rotation;

        // Calcul de la rotation ouverte (ajoute 60° à l'axe X)
        openRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + rotationAngle);
    }

    private void Update()
    {
        // Vérifie si le joueur est à portée et que la trappe n'est pas déjà déclenchée
        if (!isTriggered && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            isTriggered = true; // Marque comme déclenchée
            StartCoroutine(HandleTrap());
        }
    }

    private IEnumerator HandleTrap()
    {
        // Attends avant d'ouvrir
        yield return new WaitForSeconds(openDelay);
        StartCoroutine(RotateTrap(transform.rotation, openRotation, rotationDuration));

        // Attends avant de refermer
        yield return new WaitForSeconds(closeDelay + rotationDuration); // Inclut le temps de l'ouverture
        StartCoroutine(RotateTrap(transform.rotation, initialRotation, rotationDuration));

        // Réinitialise pour permettre de retrigger
        yield return new WaitForSeconds(rotationDuration); // Attend la fin de la fermeture
        isTriggered = false;
    }

    private IEnumerator RotateTrap(Quaternion from, Quaternion to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Interpolation progressive entre les deux rotations
            transform.rotation = Quaternion.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to; // Assure que la rotation finale est exactement atteinte
    }
}
