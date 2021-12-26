using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
[DisplayName("Material Clip")]
public class MaterialClip : PlayableAsset, ITimelineClipAsset, IPropertyPreview
{
    public Color color = Color.white;

    public int renderQueue = 2000;

    // Implementation of ITimelineClipAsset. This specifies the capabilities of this timeline clip inside the editor.
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    // Creates the playable that represents the instance of this clip.
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new MaterialBehaviour
        ScriptPlayable<MaterialPlayableBehaviour> playable = ScriptPlayable<MaterialPlayableBehaviour>.Create(graph);
        MaterialPlayableBehaviour behaviour = playable.GetBehaviour();

        // set the behaviour's data
        behaviour.color = this.color;
        behaviour.renderQueue = this.renderQueue;

        return playable;
    }

    // Defines which properties are changed by this playable. Those properties will be reverted in editmode
    // when Timeline's preview is turned off.
    public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
    }
}
