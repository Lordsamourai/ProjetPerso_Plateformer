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
        // Mouvement personnalisé avec touches configurables
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

        // Rotation avec la souris
        float yaw = Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.Rotate(0f, yaw, 0f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //print(collision);
        // Si le joueur touche le sol, il peut sauter à nouveau
        /*if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plateforme"))
        {
            isGrounded = true;
            print(isGrounded);
        }*/
        isGrounded = true;
        //print(isGrounded);
    }
}
