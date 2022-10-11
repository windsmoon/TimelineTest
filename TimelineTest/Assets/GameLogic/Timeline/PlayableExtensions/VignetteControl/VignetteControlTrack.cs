using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [TrackBindingType(typeof(Volume))]
    [TrackClipType(typeof(VignetteControlClip))]
    [TrackColor(0.32f, 0.21f,0.55f)]
    public class VignetteControlTrack : TrackAsset
    {
        #region methods
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<VignetteControlMixerBehaviour>.Create(graph, inputCount);
        }
        #endregion
    }
}