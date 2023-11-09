using UnityEngine;
using UnityEngine.UI;

public class CloudUI : MonoBehaviour
{
    [SerializeField] private RectTransform cloudSpawner;
    [SerializeField] private RectTransform cloudDestroy;
    private float cloudSpeed = 400.0f;
    private RectTransform imageRectTransform;

    void Start() 
    {
        Time.timeScale = 1.0f;
        imageRectTransform = GetComponent<RectTransform>();
        //imageRectTransform.anchoredPosition = cloudSpawner.anchoredPosition;
    }

    void Update()
    {
        Vector2 newPosition = imageRectTransform.anchoredPosition + new Vector2(1, 0) * cloudSpeed * Time.deltaTime;
        imageRectTransform.anchoredPosition = newPosition;

        if (imageRectTransform.anchoredPosition.x >= cloudDestroy.anchoredPosition.x)
        {
            imageRectTransform.anchoredPosition = cloudSpawner.anchoredPosition;
        }
    }
}
