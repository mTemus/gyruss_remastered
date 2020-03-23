public enum StageState
{
    start,            // initiate player ship
    end,              // move player ship to leave position and initiate warping
    wait,             // wait until player kill all enemies or until new wave data will arrive
    loading_wave,     // load new wave data
    spawn_enemies,    // spawn all enemies in wave data
    
}
