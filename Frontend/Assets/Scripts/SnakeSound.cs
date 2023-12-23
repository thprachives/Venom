using UnityEngine;

public class SnakeSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
    }

    // Example code to trigger the sound effect when a player lands on a snake
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "bad" tag
        if (other.gameObject.CompareTag("bad"))
        {
            // Play the snake sound effect
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Other snake-related logic (move player back, etc.)
        }
    }
}