using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public Sprite[] images;
    public float slideInterval = 4.0f;

    private Image imageComponent;
    private int currentImageIndex = 0;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
        if (images.Length > 0)
        {
            imageComponent.sprite = images[currentImageIndex];
        }
        timer = slideInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = slideInterval;
            NextImage();
        }
    }

    void NextImage()
    {
        currentImageIndex = (currentImageIndex + 1) % images.Length;
        imageComponent.sprite = images[currentImageIndex];
    }
}
