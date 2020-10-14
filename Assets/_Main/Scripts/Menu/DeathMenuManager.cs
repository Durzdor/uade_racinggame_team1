using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject buttonsHolder;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) ShowButtons();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        if (EditorApplication.isPlaying) EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void ShowButtons()
    {
        buttonsHolder.SetActive(true);
    }
}