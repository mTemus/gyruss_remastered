using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[CreateAssetMenu(fileName = "PathsDatabase", menuName = "pam_gyruss_remastered/PathsDatabase", order = 0)]
public class PathsDatabase : ScriptableObject {
    public List<PathCreator> normalLevelPaths;
    public List<PathCreator> chanceLevelPaths;
    public List<PathCreator> backLevelPaths;

    private void Start() {
        normalLevelPaths = new List<PathCreator>();
        chanceLevelPaths = new List<PathCreator>();
        backLevelPaths = new List<PathCreator>();
    }

    public PathCreator getRandomPathIn(){
        return normalLevelPaths[Random.Range(0,normalLevelPaths.Count)];
    }

    public PathCreator getRandomPathBack(){
        return backLevelPaths[Random.Range(0,normalLevelPaths.Count)];
    }

    public PathCreator getRandomPathInChanceStage(){
        return chanceLevelPaths[Random.Range(0,normalLevelPaths.Count)];
    }

    // public PathCreator getPathClosestToPlayer(){
    //     PathCreator closestPath = null;
    //     foreach(PathCreator getPath in backLevelPaths){
    //         if(getPath.path.GetClosestDistanceAlongPath(playerPosition.position)<=0.5f){
    //             closestPath = getPath;
    //         }else{
    //             closestPath = getRandomPathBack();
    //         }
    //     }
    //     return closestPath;
    // }
}
