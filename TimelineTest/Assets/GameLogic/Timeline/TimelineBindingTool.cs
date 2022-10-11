using System.Collections.Generic;
using UnityEngine.Playables;

namespace GameLogic.Timeline
{
    public class TimelineBindingTool
    {
        #region fields
        private Dictionary<string, PlayableBinding> playableBindingDict;
        private PlayableDirector playableDirector;
        #endregion

        #region constructor
        public TimelineBindingTool(PlayableDirector playableDirector)
        {
            this.playableDirector = playableDirector;
            playableBindingDict = new Dictionary<string, PlayableBinding>();
            
            foreach (PlayableBinding playableBinding in playableDirector.playableAsset.outputs)
            {
                if (playableBindingDict.ContainsKey(playableBinding.streamName))
                {
                    continue;
                }
                
                if (playableBinding.sourceObject != null)
                {
                    playableBindingDict.Add(playableBinding.streamName, playableBinding);        
                }
            }
        }
        #endregion

        #region methods
        public void BindObject(string key, UnityEngine.Object obj)
        {
            if (playableBindingDict.TryGetValue(key, out PlayableBinding playableBinding))
            {
                playableDirector.SetGenericBinding(playableBinding.sourceObject, obj);
            }
        }
        #endregion
    }
}