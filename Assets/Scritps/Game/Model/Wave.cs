public class Wave
{
   private int enemyAmount;
   private int pointsAmount;
   private int bonusPoints;
   private int enemiesKilled;
   private int enemySpawned;

   private bool isWaveEven;
   
   private string enemyName;
   
   public Wave(string enemyName, bool isWaveEven)
   {
      enemyAmount = 8;
      pointsAmount = 800;
      bonusPoints = 1000;
      this.enemyName = enemyName;
      this.isWaveEven = isWaveEven;
   }

   public Wave(int enemyAmount, int pointsAmount, int bonusPoints, string enemyName)
   {
      this.enemyAmount = enemyAmount;
      this.pointsAmount = pointsAmount;
      this.bonusPoints = bonusPoints;
      this.enemyName = enemyName;
   }
   
   public int EnemyAmount => enemyAmount;

   public int PointsAmount => pointsAmount;

   public int BonusPoints => bonusPoints;

   public string EnemyName => enemyName;

   public bool IsWaveEven
   {
      get => isWaveEven;
      set => isWaveEven = value;
   }

   public int EnemySpawned
   {
      get => enemySpawned;
      set => enemySpawned = value;
   }

   public int EnemiesKilled
   {
      get => enemiesKilled;
      set => enemiesKilled = value;
   }
}
