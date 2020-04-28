using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background = null;

    void Start()
    {
        Bounds backgroundBounds = background.bounds;
        float screenRatio = (float) Screen.width / (float) Screen.height;
        float targetRatio = backgroundBounds.size.x / backgroundBounds.size.y;

        if (screenRatio >= targetRatio) { Camera.main.orthographicSize = background.bounds.size.y / 2; }
        else {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = background.bounds.size.y / 2 * differenceInSize;
        }
    }
}
