using UnityEngine;
using UnityEngine.UI;

public class BonusTextDeactivator : MonoBehaviour
{
    private float timer;
    private float period = 2f;

    private bool count;
    
    void Update()
    {
        if (!count) return;
        
        if (timer >= period) Destroy(transform.gameObject); 
        else timer += Time.deltaTime; 
    }

    private void OnEnable()
    {
        count = true;
    }

    private void OnDestroy()
    {
        GyrussGameManager.Instance.AddPointsToScore(int.Parse(transform.GetComponent<Text>().text));
    }
}
