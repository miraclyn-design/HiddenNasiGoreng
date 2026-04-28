using UnityEngine;
using UnityEngine.UI;

public class ImageUVTiling : MonoBehaviour
{
    public float scrollSpeed = 0.3f;
    private RawImage img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect uv = img.uvRect;
        uv.x += scrollSpeed * Time.deltaTime;
        img.uvRect = uv;
    }
}
