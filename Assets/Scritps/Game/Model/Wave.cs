public class Wave
{
   private int enemyAmount;
   private int pointsAmount;
   private int bonusPoints;
   private int enemiesKilled;
   
   public Wave()
   {
      enemyAmount = 8;
      pointsAmount = 800;
      bonusPoints = 1000;
   }

   public Wave(int enemyAmount, int pointsAmount, int bonusPoints)
   {
      this.enemyAmount = enemyAmount;
      this.pointsAmount = pointsAmount;
      this.bonusPoints = bonusPoints;
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
