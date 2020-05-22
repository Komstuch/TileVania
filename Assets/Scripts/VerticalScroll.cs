using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollSpeed = 1f;
    void Update()
    {
        float yMove = scrollSpeed * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
