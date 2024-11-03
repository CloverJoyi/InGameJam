using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI视差偏移效果
/// </summary>
public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);

        // //设置ui偏移（失败
        // Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // RectTransform rectTransform = GetComponent<RectTransform>();
        // rectTransform.localPosition = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
