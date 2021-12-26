using System;
using UnityEngine;
using UnityEngine.Playables;

public class MaterialMixerBehaviour : PlayableBehaviour
{
    private Material _target = null;

    private Color _defaultColor = Color.white;
    private int _defaultRenderQueue = 2000;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Material trackBinding = playerData as Material;
        if (trackBinding == null)
            return;

        InitializeIfNecessary(trackBinding);

        int inputCount = playable.GetInputCount();

        Color color = Color.clear;
        float renderQueue = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight <= 0f)
            {
                continue;
            }

            ScriptPlayable<MaterialPlayableBehaviour> inputPlayable = (ScriptPlayable<MaterialPlayableBehaviour>)playable.GetInput(i);
            MaterialPlayableBehaviour input = inputPlayable.GetBehaviour();
            color += input.color * inputWeight;
            renderQueue += input.renderQueue * inputWeight;
        }

        trackBinding.color = color;
        trackBinding.renderQueue = Mathf.RoundToInt(renderQueue);
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        RestoreDefaults();
        base.OnPlayableDestroy(playable);
    }

    private void InitializeIfNecessary(Material trackBinding)
    {
        if (_target == trackBinding)
        {
            return;
        }

        if (_target != null)
        {
            RestoreDefaults();
        }

        _target = trackBinding;
        _defaultColor = _target.color;
        _defaultRenderQueue = _target.renderQueue;
    }

    private void RestoreDefaults()
    {
        if (_target == null)
        {
            return;
        }

        _target.color = _defaultColor;
        _target.renderQueue = _defaultRenderQueue;
    }
}
