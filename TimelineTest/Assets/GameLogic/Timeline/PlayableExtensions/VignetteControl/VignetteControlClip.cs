using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [Serializable]
    public class VignetteControlClip : PlayableAsset, ITimelineClipAsset
    {
        #region fields
        [SerializeField, Range(0f, 1f)]
        private float intensity;
        #endregion
        
        #region properties
        public ClipCaps clipCaps
        {
            get => ClipCaps.All;
        }
        #endregion

        #region methods
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<VignetteControlBehaviour>.Create(graph);
            VignetteControlBehaviour volumeControlBehaviour = playable.GetBehaviour();
            volumeControlBehaviour.Intensity = intensity; 
            return playable;
        }
        #endregion
    }
}