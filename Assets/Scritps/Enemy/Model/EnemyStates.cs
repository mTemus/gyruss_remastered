public enum EnemyStates
{
    entering, // enter the map and fly on a path
    fly_to_spot, // fly to spot in the center of the map
    wait, // wait in the center of the map
    attack, // leave your spot and do a path to do a collision with player
    fly_away, // (chance stage) when enters, fly some distance on a path and fly away
    fly_to_mini_boss, // (mini_boss stage) fly to mini_boss to be eaten
    fly_from_mini_boss, // fly from mini_boss and attack
}
