using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition_image : MonoBehaviour
{
    [SerializeField]
    private Image transition_image;

    private float fadeDuration = 2.0f;

    void Awake()
    {
        Color new_color = transition_image.color;
        new_color.a = 0;
        transition_image.color = new_color;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void UserClicked(){
        StartCoroutine(FadeOut());
    }


    // Update is called once per frame
    void Update() { }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color to_transparent = transition_image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            to_transparent.a = alpha;
            transition_image.color = to_transparent;

            yield return null;
        }

        // Ensure the image is fully opaque at the end of the fade
        to_transparent.a = 0f;
        transition_image.color = to_transparent;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color to_transparent = transition_image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            to_transparent.a = alpha;
            transition_image.color = to_transparent;

            yield return null;
        }

        // Ensure the image is fully opaque at the end of the fade
        to_transparent.a = 1f;
        transition_image.color = to_transparent;

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(Constants.play_state_name);
    }
}
