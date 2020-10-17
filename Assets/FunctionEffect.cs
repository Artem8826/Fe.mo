using System;
using UnityEngine;

namespace Assets
{
    public class FunctionEffect
    {
        public static bool Call(Component supposedlyEffectable, InteractionEffect interactionEffect)
        {
            if (supposedlyEffectable.TryGetComponent(out IPutable iPutable))
            {
                if (interactionEffect == InteractionEffect.Wet)
                {
                    iPutable.OnWet();
                }
                else if (interactionEffect == InteractionEffect.Dry)
                {
                    iPutable.OnDry();
                }

                return true;
            }

            return false;
        }
    }
}