using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UI_FlappyBird uiFlappyBird;

    public virtual void Init(UI_FlappyBird uiFlappyBird)
    {
        this.uiFlappyBird = uiFlappyBird;
    }

    protected abstract FlappyState GetUIState();

    public void SetActive(FlappyState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
