using UnityEngine;

public class StarsCreator : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab = null;
    [SerializeField] private GameObject starsBackground = null;
    [SerializeField] private int starsAmount = 100;
    
    private float sizeX;
    private float sizeY;
    
    void Start()
    {
        Vector3 backgroundScale = starsBackground.transform.localScale;
        sizeX = backgroundScale.x;
        sizeY = backgroundScale.y;

        for (int i = 0; i < starsAmount; i++) {
            float posX = Random.Range(-(sizeX / 2), sizeX / 2);
            float posY = Random.Range(-(sizeY / 2), sizeY / 2);

            GameObject star = Instantiate(starPrefab, starsBackground.transform, true);
            star.transform.position = new Vector3(posX, posY, 2);
        }
    }

    
}
