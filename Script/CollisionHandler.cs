using System;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisonHandler : MonoBehaviour
{
        [SerializeField] float levelLoadDelay = 2f;
        [SerializeField] AudioClip landingSFX;
        [SerializeField] AudioClip crashSFX;
        [SerializeField] ParticleSystem successParticles;
        [SerializeField] ParticleSystem crashParticles;
        
        AudioSource audioSource;

            bool isControllable = true;
        private void Start(){
            audioSource = GetComponent<AudioSource>();
        }
    private void OnCollisionEnter(Collision other)
    {
        if(!isControllable ) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are my friend");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
            StartCrashSequence();
                break;
        }
}

    private void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
         audioSource.PlayOneShot(landingSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled=false;
           Invoke("NewScene",levelLoadDelay);
        
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("RelaodScene", levelLoadDelay);
    }
    void NewScene()
    {
        
        // @GetComponent<Movement>().enabled=false;
        int scene_index= SceneManager.GetActiveScene().buildIndex;
        int next_scene=scene_index+1;
        if(next_scene==SceneManager.sceneCountInBuildSettings)
        {
            next_scene=0;
        }
        SceneManager.LoadScene(next_scene);
    }
    void RelaodScene()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene);  
    }
 }

