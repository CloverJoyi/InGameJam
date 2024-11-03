using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimAudio : MonoBehaviour
{

    #region Audio

    public void FootStep()
    {
        AudioPlayer.instance.PlayFootStep();
    }

    #endregion
}
