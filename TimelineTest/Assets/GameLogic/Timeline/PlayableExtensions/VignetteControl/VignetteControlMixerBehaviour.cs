using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    public class VignetteControlMixerBehaviour : PlayableBehaviour
    {
        #region mehtods
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Volume volume = playerData as Volume;

            if (volume.profile.TryGet<Vignette>(out Vignette vignette) == false)
            {
                base.ProcessFrame(playable, info, playerData);
                return;
            }
            
            int inputCount = playable.GetInputCount();
            float totalWeight = 0f;
            float blendedIntensity = 0f;
            
            for (int i = 0; i < inputCount; i++)
            {
                float weight = playable.GetInputWeight(i);
                ScriptPlayable<VignetteControlBehaviour> inputPlayable = (ScriptPlayable<VignetteControlBehaviour>)playable.GetInput(i);
                VignetteControlBehaviour volumeControlBehaviour = inputPlayable.GetBehaviour();
                blendedIntensity += volumeControlBehaviour.Intensity * weight;
                totalWeight += weight;
            }
            
            vignette.intensity.SetValue(new ClampedFloatParameter(blendedIntensity, 0.0f, 1f, true));
            base.ProcessFrame(playable, info, playerData);
        }
        #endregion
    }
}