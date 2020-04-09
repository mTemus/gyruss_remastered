public enum StageState
{
/* 0 */    get_ready,
/* 1 */    start,            // initiate player ship
/* 2 */    end,              // move player ship to leave position and initiate warping
/* 3 */    wait,             // wait until player kill all enemies or until new wave data will arrive
/* 4 */    loading_wave,     // load new wave data
/* 5 */    spawn_enemies,    // spawn all enemies in wave data
/* 6 */    initialize_GUI,                                
/* 7 */    spawn_player, 
    
}
