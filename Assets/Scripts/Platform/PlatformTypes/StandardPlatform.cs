using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class StandardPlatform : Platform
    {
        protected override PlatformType GetPlatformType()
        {
            return PlatformType.StandardPlatform;
        }

        protected override void Update()
        {
            base.Update();  
        }
    }
}
