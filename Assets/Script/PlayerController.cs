using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public ParticleSystem dustParticle;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private Vector3 gameOverForce = new Vector3(-25f, 25f, 0);
    private float yBoundary = -10f;
    private Vector3 gravityAdjust = new Vector3(0, -9.8f * 2.3f, 0);
    private float upForce = 13.0f;
    private bool isOnGround;
    private AudioSource audioSource;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity = gravityAdjust;
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        isOnGround = true;
        Debug.Log(Physics.gravity.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigidbody.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            playerAnimator.enabled = false;
            isOnGround = false;
            audioSource.PlayOneShot(jumpSound);
            dustParticle.Stop();
        }

        if(transform.position.y < yBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameManager.IsGameOver())
        {
            playerAnimator.enabled = true;
            isOnGround = true;
            dustParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(crashSound);
            dustParticle.Stop();
            gameManager.GameOver();
            playerAnimator.enabled = false;
            playerRigidbody.constraints = RigidbodyConstraints.None;
            playerRigidbody.AddForce(gameOverForce, ForceMode.Impulse);
            explosionParticle.Play();
        }
    }
}
