using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaUI : MonoBehaviour
{
    private Slider _manaSlider;

    void Start()
    {
        _manaSlider = GetComponent<Slider>();

        CommonEvents.Instance.OnPlayerManaChanged += ChangeMana;
    }

    void OnDisable()
    {
        CommonEvents.Instance.OnPlayerManaChanged -= ChangeMana;
    }

    private void ChangeMana(int mana)
    {
        _manaSlider.value = mana;
    }
}
