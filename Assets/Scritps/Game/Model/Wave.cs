public class Wave
{
   private int enemyAmount;
   private int enemySpawned;
   private bool isWaveEven;
   private bool miniBoss;
   
   private string enemyName;
   
   public Wave(string enemyName, bool isWaveEven, bool miniBoss)
   {
      enemyAmount = 8;
      this.enemyName = enemyName;
      this.isWaveEven = isWaveEven;
      this.miniBoss = miniBoss;
   }

   public Wave(int enemyAmount, string enemyName, bool miniBoss)
   {
      this.enemyAmount = enemyAmount;
      this.enemyName = enemyName;
      this.miniBoss = miniBoss;
   }

   public int EnemyAmount => enemyAmount;

   public string EnemyName => enemyName;

   public bool MiniBoss => miniBoss;

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

   public PathFollow PathFollow{
      get; set;
   }
}
