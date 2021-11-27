using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGlassCrushSound : MonoBehaviour
{
   public void GlassCrushSound()
    {
        CSoundManager.Instance.PlaySFX(SoundSFX.Glass_Crush);
    }
}
