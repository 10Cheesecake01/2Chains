using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;
    private bool isFlipping = false;

    public void OnMouseDown()
    {
        if (Card_Back.activeSelf && controller.canReveal && !isFlipping)
        {
            StartCoroutine(FlipCard());
            controller.CardRevealed(this);
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image; //This gets the sprite renderer component and changes the property of it's sprite!
    }

    public void Unreveal()
    {
        StartCoroutine(FlipCardBack());
    }

    private IEnumerator FlipCard()
    {
        isFlipping = true;
        float duration = 0.2f;
        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        Card_Back.SetActive(false);
        isFlipping = false;
    }

    private IEnumerator FlipCardBack()
    {
        isFlipping = true;
        float duration = 0.2f;
        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 0f, 0f);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        Card_Back.SetActive(true);
        isFlipping = false;
    }
}