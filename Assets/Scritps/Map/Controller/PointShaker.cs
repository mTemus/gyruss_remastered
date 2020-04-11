using UnityEngine;

public class PointShaker : MonoBehaviour
{
    [SerializeField] private float shakeArea = 0.3f;
    [SerializeField] private float shakeSpeed = 0.01f;

    private int index;
    private float currentShake;
    
    void Start()
    {
        string pointName = transform.name;
        index = int.Parse(pointName[pointName.Length - 1].ToString());
    }

    void Update()
    {
        var myTransform = transform;
        Vector3 currPos = myTransform.position;

        myTransform.position = index % 2 == 0 ? 
            new Vector3(currPos.x, currPos.y + shakeSpeed, currPos.z) :
            new Vector3(currPos.x + shakeSpeed, currPos.y, currPos.z);
        
        currentShake += shakeSpeed;

        if (!(Mathf.Abs(currentShake) >= shakeArea)) return;
        currentShake = 0;
        shakeSpeed = -shakeSpeed;
    }
}
