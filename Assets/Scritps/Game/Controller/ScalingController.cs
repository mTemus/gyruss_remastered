using UnityEngine;

public class ScalingController : MonoBehaviour
{
    private float scalingFactor;
    private float distanceToCenter;
    private float currentDistanceToCenter;

    private bool enemyIsInCenterPosition = false;
    
    
    void Start()
    {
        Vector3 playerPos = GyrussGameManager.Instance.PlayerInputManager.GetPlayerPosition();
        Vector3 centerPos = Vector3.zero;

        distanceToCenter = Vector3.Distance(playerPos, centerPos);
    }

    void Update()
    {
        Vector3 currPosition = transform.position;
        currentDistanceToCenter = Vector3.Distance(currPosition, Vector3.zero);

        scalingFactor = currentDistanceToCenter / distanceToCenter;
        if (scalingFactor > 1) scalingFactor = 1;

        if (transform.CompareTag("EnemyShip")) {
            if (scalingFactor <= 0.20) {
                if (!enemyIsInCenterPosition) {
                    transform.GetComponent<SpriteRenderer>().sprite = LoadNewSprite("normal", "center");
                    enemyIsInCenterPosition = !enemyIsInCenterPosition;
                }
            }
            else {
                if (enemyIsInCenterPosition) {
                    transform.GetComponent<SpriteRenderer>().sprite = LoadNewSprite("center", "normal");
                    enemyIsInCenterPosition = !enemyIsInCenterPosition;
                }
            }
        }

        if (enemyIsInCenterPosition) { scalingFactor = 1; }
        transform.localScale = new Vector3(scalingFactor, scalingFactor, 0);
    }

    private Sprite LoadNewSprite(string oldSpriteType, string newSpriteType)
    {
        int currentLevel = GyrussGameManager.Instance.LevelManager.CurrentLevel;
        string enemyName = transform.GetComponent<SpriteRenderer>().sprite.name;
        enemyName = enemyName.Replace(oldSpriteType, newSpriteType);
        Sprite[] enemySprites = Resources.LoadAll<Sprite>("Sprites/Enemies/enemies-level-" + currentLevel);
        Sprite newSprite = null;

        foreach (Sprite enemySprite in enemySprites) {
            if (enemySprite.name.Equals(enemyName)) { newSprite = enemySprite; }
        }

        return newSprite;
    }
    
    
}
