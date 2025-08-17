using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersonnage : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float jumpForce = 5f;
    public float rotationSpeed = 2f; // Vitesse de rotation
    private float pitch = 0f; // Rotation verticale (haut/bas) de la caméra

    public Transform cameraTransform;

    private Rigidbody rb; // Référence au Rigidbody

    private bool isGrounded = true; // Vérifie si le joueur est au sol

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        // Empêche tout mouvement si le jeu est en pause
        if (PauseManager.Instance != null && PauseManager.Instance.isPaused)
            return;

        // Mouvement personnalisé
        Vector3 move = Vector3.zero;
        if (Input.GetKey(OptionsManager.Instance.controlsManager.GetForwardKey()))
            move += transform.forward;
        if (Input.GetKey(OptionsManager.Instance.controlsManager.GetBackwardKey()))
            move -= transform.forward;
        if (Input.GetKey(OptionsManager.Instance.controlsManager.GetRightKey()))
            move += transform.right;
        if (Input.GetKey(OptionsManager.Instance.controlsManager.GetLeftKey()))
            move -= transform.right;
        if (Input.GetKeyDown(OptionsManager.Instance.controlsManager.GetJumpKey()) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 velocity = move.normalized * moveSpeed * Time.deltaTime;
        transform.position += velocity;

        // Rotation caméra
        float yaw = Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.Rotate(0f, yaw, 0f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Ouverture du menu pause
        if (Input.GetKeyDown(OptionsManager.Instance.controlsManager.GetEchap()))
        {
            if (PauseManager.Instance.isPaused)
                PauseManager.Instance.HidePause();
            else
                PauseManager.Instance.ShowPause();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
