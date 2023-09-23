using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip EndAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Play()
    {
        StartCoroutine(ChangeScene());
    }

    public void Quit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    IEnumerator ChangeScene()
    {
        animator.SetTrigger("startAnim");

        yield return new WaitForSeconds(EndAnim.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}