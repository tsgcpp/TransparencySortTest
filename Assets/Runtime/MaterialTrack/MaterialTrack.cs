using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1.0f, 0.0f, 0.0f)]
[TrackBindingType(typeof(Material))]
[TrackClipType(typeof(MaterialClip))]
public class MaterialTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MaterialMixerBehaviour>.Create(graph, inputCount);
    }
}
