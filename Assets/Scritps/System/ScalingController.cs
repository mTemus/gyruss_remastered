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

    private Vector2 normalEnemyColliderSize;
    private Vector2 normalEnemyColliderOffset;
    private static readonly int Scaling = Animator.StringToHash("scaling");

    void Start()
    {
        Vector3 playerPos = GyrussGameManager.Instance.GetPlayerShipPosition();
        Vector3 centerPos = Vector3.zero;

        distanceToCenter = Vector3.Distance(playerPos, centerPos);

        if (transform.CompareTag("EnemyShip")) {
            currentCollider = transform.GetComponent<BoxCollider2D>();
            normalEnemyColliderSize = currentCollider.size;
            normalEnemyColliderOffset = currentCollider.offset;
            
            
            // TODO: back to this mechanism
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


        switch (transform.tag) {
            case "EnemyShip":
                if (scalingFactor <= 0.25) {
                    if (!enemyIsInCenterPosition) {
                        currentCollider.size = new Vector2(0.1f, 0.1f);
                        currentCollider.offset = new Vector2(-0.016f, 0.015f);
                        enemyIsInCenterPosition = !enemyIsInCenterPosition;
                        myRenderer.sprite = enemyCenterSprite;
                    }
                }
                else {
                    if (enemyIsInCenterPosition) {
                        currentCollider.size = normalEnemyColliderSize;
                        currentCollider.offset = normalEnemyColliderOffset;
                        enemyIsInCenterPosition = !enemyIsInCenterPosition;
                    }
                    else {
                        if (!myRenderer.sprite.Equals(enemyNormalSprite)) {
                            myRenderer.sprite = enemyNormalSprite;
                        }
                    }
                }
                // transform.GetComponent<Animator>().SetFloat(Scaling, scalingFactor);
                break;
            
            case "Player":
                transform.GetChild(0).GetComponent<Animator>().SetFloat(Scaling, scalingFactor);
                break;
            
            case "Rocket":
                float doubleScale = scalingFactor * 2;

                if (doubleScale < 1) {
                    transform.localScale = new Vector3(doubleScale, doubleScale, 0);
                }
                return;
                break;
        }

        if (enemyIsInCenterPosition) { scalingFactor = 1.5f; }
        transform.localScale = new Vector3(scalingFactor, scalingFactor, 0);
    }
}
