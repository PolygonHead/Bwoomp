using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bwomp : MonoBehaviour
{
    public AudioClip[] sounds;
    public Texture2D[] images;
    public GameObject imageObject;
    RawImage imageComponent;
    public byte alpha = 0;
    [SerializeField] float alphaLerpSpeed = 1;
    public GameObject text;
    bool lerpAlpha;
    float volume;
    public Slider slider;

    void Start() {
        imageComponent = imageObject.GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        volume = slider.value;
        imageComponent.color = new Color32(255, 255, 255, alpha);
        if (lerpAlpha) {
            float alphaFloat = (float)alpha;
            alpha = (byte)Mathf.Lerp(alphaFloat, -1, Time.deltaTime * alphaLerpSpeed);
        }
        if (Input.anyKeyDown) {
            if (text != null){
                Destroy(text);
            }
            lerpAlpha = false;
            alpha = 255;
            int sIndex = Random.Range(0, sounds.Length);
            int iIndex = Random.Range(0, images.Length);

            AudioClip randomSound = sounds[sIndex];
            Texture2D randomImage = images[iIndex];
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.volume = volume;
            source.clip = randomSound;
            imageComponent.texture = randomImage;
            source.Play();
            StartCoroutine(DestroyAudio(source));
            StartCoroutine(BeginLerpAlpha());
        }
    }

    IEnumerator DestroyAudio (AudioSource source) {
        // Destroys the Audio Source
        while (source.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(source);
    }
    public IEnumerator BeginLerpAlpha(){
        yield return new WaitForSeconds(1f);
        lerpAlpha = true;
    }
}
