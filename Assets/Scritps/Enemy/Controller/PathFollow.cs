using UnityEngine;
using PathCreation;

public class PathFollow : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public Transform mapCenter;
    public float speed = 5;
    public float distanceTravelled;
    public float turningRate = 90f; 

    public PathFollow(){
    }

    private void Start() {
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
            speed = pathCreator.path.length/22.5f + 5;
        }   
    }

    private void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void moveOnPath(){
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            rotateOnPath();
        }
    }

    public bool endPathReached(){
        if(transform.position == pathCreator.path.GetPoint(pathCreator.path.localPoints.Length - 1)){
            return true;
        }else{
            return false;
        }
    }

    private void rotateOnPath(){
        Vector3 tarObj = (mapCenter.position - transform.position).normalized;
        float angle = Mathf.Atan2(tarObj.y, tarObj.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,Mathf.LerpAngle(transform.rotation.z, angle-90, 1));;
    }
}
