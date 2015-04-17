using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class soundSlider : MonoBehaviour {

    void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        slider.value = Singleton<SoundPlayer>.instance.VOLUME;
    }

    public void set(float f)
    {
        SoundPlayer sp = Singleton<SoundPlayer>.instance;

        sp.VOLUME = f;
    }
}
