using UnityEngine;

public class RocketParticleController : MonoBehaviour
{
   [SerializeField] private float activeTime = 0.2f;

   private float counter;
   
   private void Update()
   {
      if (!transform.gameObject.activeSelf) return;
      
      if (counter >= activeTime) {
         transform.gameObject.SetActive(false);
      }

      counter += Time.deltaTime;
   }

   private void OnDisable()
   {
      counter = 0;
   }
}
