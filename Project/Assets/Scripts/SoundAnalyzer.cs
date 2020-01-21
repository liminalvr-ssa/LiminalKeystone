using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnalyzer : MonoBehaviour {

    public AudioSource sound;
    public int matReferencer;
    Material affectedTexture;
    public float outputOffest;
    public float reactionSpeedMultiplayer = 1;
    public float valueMultiplayer = 1;
    public int sample = 5;
    public bool isReading = false;

    int samplesNum;
    public float[] soundData;
    float[] Band = new float[8];
    int seeker;
    float precomp;
    string modifier;
    Vector3 output;

    void Start() {
        //sound = GetComponent<AudioSource>();
        samplesNum = 512;//sound.clip.samples;
        soundData = new float[samplesNum];//new float[samplesNum];
        seeker = 0;
        modifier = "_skinModifier";
        output = Vector3.zero;

        sound.clip.GetData(soundData, 0);

        affectedTexture = GameGraphics.MATS[matReferencer];
    }

    void Update() {
        if (isReading) {
            sound.GetSpectrumData(soundData, 0, FFTWindow.Blackman);
            getBands();
            precomp = (float)System.Convert.ToDouble(Band[sample].ToString().Substring(0, 4)) * valueMultiplayer + outputOffest;
            affectedTexture.SetFloat(modifier, Mathf.Lerp(affectedTexture.GetFloat(modifier), precomp, Time.time * reactionSpeedMultiplayer * Time.deltaTime));
        }
    }

    void getBands () {
        // 86hz per sample

        int count = 0;

        for (int i = 0; i < Band.Length; i++) {
            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2,i) * 2;
            for (int j = 0; j < sampleCount; j++) {
                avg += soundData[count] * (count + 1);
                count++;
            }
            avg /= count;

            Band[i] = avg;
        }
    }
}
