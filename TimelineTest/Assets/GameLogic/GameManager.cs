using GameLogic.Timeline;
using GameLogic.Timeline.PlayableExtensions.VignetteControl;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        #region constants
        public const string CharacterAnimation = "CharacterAnimation";
        public const string SignalTrack = "Signal Track";
        #endregion
        
        #region fields
        [SerializeField]
        private PlayableDirector playableDirector;
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private SignalReceiver signalReceiver;
        private TimelineBindingTool timelineBindingTool;
        #endregion

        #region unity methods
        private void Awake()    
        {
            foreach (var output in playableDirector.playableAsset.outputs)
            {
                Debug.Log(output.streamName);
            }
            
            timelineBindingTool = new TimelineBindingTool(playableDirector);
            timelineBindingTool.BindObject(CharacterAnimation, character.GetComponent<Animator>());
            timelineBindingTool.BindObject(SignalTrack, signalReceiver);
            playableDirector.Play();
            
#if UNITY_EDITOR
            PlayableGraph playableGraph = PlayableGraph.Create();
            ScriptPlayableOutput playableOutput = ScriptPlayableOutput.Create(playableGraph, "Vignette");
            ScriptPlayable<VignetteControlMixerBehaviour> vignetteControlMixerPlayable = ScriptPlayable<VignetteControlMixerBehaviour>.Create(playableGraph, 2);
            playableOutput.SetSourcePlayable(vignetteControlMixerPlayable);
            ScriptPlayable<VignetteControlBehaviour> vignetteControlPlayable1 = ScriptPlayable<VignetteControlBehaviour>.Create(playableGraph);
            ScriptPlayable<VignetteControlBehaviour> vignetteControlPlayable2 = ScriptPlayable<VignetteControlBehaviour>.Create(playableGraph);
            playableGraph.Connect(vignetteControlPlayable1, 0, vignetteControlMixerPlayable, 0);
            playableGraph.Connect(vignetteControlPlayable2, 0, vignetteControlMixerPlayable, 1);
#endif
        }
        #endregion
    }
}