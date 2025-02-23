using System.Collections;
using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class VanishingPlatform : Platform
    {
        [Header("Vanishing Settings")]
        [SerializeField] private float vanishDelay = 3f;   
        [SerializeField] private float reappearDelay = 5f; 

        private Coroutine vanishCoroutine;

        protected override void Update()
        {
            base.Update();  // Ensure movement logic runs
        }

        public override void Activate()
        {
            base.Activate();

            // Start the vanishing sequence
            if (vanishCoroutine != null)
            {
                StopCoroutine(vanishCoroutine); 
            }
            vanishCoroutine = StartCoroutine(VanishingSequence());
        }

        private IEnumerator VanishingSequence()
        {
            // Wait for vanish delay
            yield return new WaitForSeconds(vanishDelay);

            // Vanish the platform
            gameObject.SetActive(false);

            // Wait for reappear delay
            yield return new WaitForSeconds(reappearDelay);

            // Reappear the platform
            gameObject.SetActive(true);
            Activate(); // Restart behavior when reappearing
        }

        protected override PlatformType GetPlatformType()
        {
            return PlatformType.VanishingPlatform;
        }
    }
}

