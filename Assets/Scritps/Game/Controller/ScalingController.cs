using System.Linq;
using UnityEngine;

public class ScalingController : MonoBehaviour
{
    private float scalingFactor;
    private float distanceToCenter;
    private float currentDistanceToCenter;

    private bool enemyIsInCenterPosition = false;

    private BoxCollider2D currentCollider;
    private SpriteRenderer myRenderer;
    private Sprite enemyNormalSprite;
    private Sprite enemyCenterSprite;
    
    void Start()
    {
        Vector3 playerPos = GyrussGameManager.Instance.PlayerInputManager.GetPlayerPosition();
        Vector3 centerPos = Vector3.zero;

        distanceToCenter = Vector3.Distance(playerPos, centerPos);

        if (transform.CompareTag("EnemyShip")) {
            currentCollider = transform.GetComponent<BoxCollider2D>();
            myRenderer = transform.GetComponent<SpriteRenderer>();
            enemyNormalSprite = transform.GetComponent<SpriteRenderer>().sprite;
            
            int currentLevel = GyrussGameManager.Instance.LevelManager.CurrentLevel;
            string enemyName = myRenderer.sprite.name;
            enemyName = enemyName.Replace("normal", "center");

            enemyCenterSprite = Resources.LoadAll<Sprite>("Sprites/Enemies/enemies-level-" + currentLevel)
                .Single(sprite => sprite.name.Equals(enemyName));
        }
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
                    myRenderer.sprite = enemyCenterSprite;
                    currentCollider.size = new Vector2(0.1f, 0.1f);
                    currentCollider.offset = new Vector2(-0.016f, 0.015f);
                    enemyIsInCenterPosition = !enemyIsInCenterPosition;
                }
            }
            else {
                if (enemyIsInCenterPosition) {
                    myRenderer.sprite = enemyNormalSprite;
                    enemyIsInCenterPosition = !enemyIsInCenterPosition;
                }
            }
        }

        if (enemyIsInCenterPosition) { scalingFactor = 1; }
        transform.localScale = new Vector3(scalingFactor, scalingFactor, 0);
    }
}
