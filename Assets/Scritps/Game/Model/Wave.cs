public class Wave
{
   private int enemyAmount;
   private int pointsAmount;
   private int bonusPoints;
   private int enemiesKilled;

   private string enemyName;
   
   public Wave(string enemyName)
   {
      enemyAmount = 8;
      pointsAmount = 800;
      bonusPoints = 1000;
      this.enemyName = enemyName;
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

   public int EnemiesKilled
   {
      get => enemiesKilled;
      set => enemiesKilled = value;
   }
}
