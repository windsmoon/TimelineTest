using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [Serializable]
    public class VignetteControlBehaviour : PlayableBehaviour
    {
        #region fields
        [SerializeField]
        private float intensity;
        #endregion

        #region properties
        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }
        #endregion

        #region methods
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Volume volume = playerData as Volume;

            if (volume.profile.TryGet<Vignette>(out Vignette vignette))
            {
                vignette.intensity.SetValue(new ClampedFloatParameter(intensity, 0.0f, 1f, true));
            }
            
            base.ProcessFrame(playable, info, playerData);
        }
        #endregion
    }
}