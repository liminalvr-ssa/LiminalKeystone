using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenFader.Instance.FadeToClearFromBlack(2);
        //StartCoroutine(FadeRout());
    }

    // Update is called once per frame
    void Update()
    {
        var device = VRDevice.Device.PrimaryInputDevice;
    }

    private IEnumerator FadeRout()
    {
        ScreenFader.Instance.FadeToBlack(2);
        yield return new WaitForSeconds(2f);
        End();
    }

    private void End()
    {
        ExperienceApp.End();
    }

    private void OnTriggerEnter(Collider other)
    {
        End();
    }
}
