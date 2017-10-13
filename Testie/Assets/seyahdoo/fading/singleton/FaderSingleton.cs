///Author: seyahdoo(Seyyid Ahmed Doðan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///Created by oculus engineers in OVRScreenFade class
///Edited by eVRydayVR -> https://evrydayvr.wordpress.com/2015/07/15/unity-5-x-package-fade-screen-inout/
///Edited so that you can use it as a static service in game
///Fixed "scene change camera null" bug

///usage:
///seyahdoo.fading.Fader.CreateInstance();
///seyahdoo.fading.Fader.SetDefaultFadeDuration(float duration);
///seyahdoo.fading.Fader.SetFadeColor(Color.black);
///seyahdoo.fading.Fader.FadeOut();
///seyahdoo.fading.Fader.FadeIn();

///Dependancies:
///Unity singleton script
///https://github.com/seyahdoo/Unity-Code-Repo/blob/master/Testie/Assets/seyahdoo/other/Singleton.cs


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.fading.singleton
{
    

    public class FaderSingleton : other.Singleton<FaderSingleton>
    {
        
        [SerializeField]
        private float defaultFadeDuration = 2.0f;

        [SerializeField]
        private Color fadeColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        private Material fadeMaterial = null;

        private bool fading = false;
		private float desiredAlpha;
        protected float fadeDuration = 2.0f;

        
        private List<ScreenFadeControl> fadeControls = new List<ScreenFadeControl>();

        void Awake()
        {
            fadeMaterial = new Material(Shader.Find("Seyahdoo/Unlit Transparent Color"));
            fadeMaterial.color = fadeColor;
            AddCameraControlls();
        }

        void AddCameraControlls()
        {
            // Clean up controlls
            foreach (ScreenFadeControl fadeControl in fadeControls)
            {
                Destroy(fadeControl);
            }
            fadeControls.Clear();

            // Find all cameras and add fade material to them (initially disabled)
            foreach (Camera c in Camera.allCameras)
            {
                var fadeControl = c.gameObject.AddComponent<ScreenFadeControl>();
                fadeControl.fadeMaterial = fadeMaterial;
                fadeControls.Add(fadeControl);
            }
        }

        void SetFadersEnabled(bool value)
        {
            foreach (ScreenFadeControl fadeControl in fadeControls)
            {
                if(fadeControl == null)
                {
                    AddCameraControlls();
                    SetFadersEnabled(value);
                    return;
                }
                fadeControl.enabled = value;
            }
        }
        
        private IEnumerator DoFade()
        {
            fading = true;

            SetFadersEnabled(true);

            do
            {
                yield return new WaitForEndOfFrame();

                fadeColor.a = Mathf.Clamp01(fadeColor.a + ((Mathf.Sign(desiredAlpha - fadeColor.a) * Time.deltaTime) / fadeDuration)); //1.0f - Mathf.Clamp01((Time.time-startTime)/fadeTime);

                fadeMaterial.color = fadeColor;
            }
            while (fadeColor.a != desiredAlpha);


            if (fadeColor.a == 0f)
            {
                SetFadersEnabled(false);
            }

            fading = false;
        }

        private class ScreenFadeControl : MonoBehaviour
        {
            public Material fadeMaterial = null;

            /// <summary>
            /// Renders the fade overlay when attached to a camera object
            /// </summary>
            void OnPostRender()
            {

                fadeMaterial.SetPass(0);
                GL.PushMatrix();
                GL.LoadOrtho();
                GL.Color(fadeMaterial.color);
                GL.Begin(GL.QUADS);
                GL.Vertex3(0f, 0f, -12f);
                GL.Vertex3(0f, 1f, -12f);
                GL.Vertex3(1f, 1f, -12f);
                GL.Vertex3(1f, 0f, -12f);
                GL.End();
                GL.PopMatrix();

            }


        }

        /// <summary>
        /// make it bright
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public void FadeIn(float duration)
        {
            fadeDuration = duration;
            desiredAlpha = 0f;
            if (!fading) StartCoroutine(DoFade());
        }

        /// <summary>
        /// make it bright
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public void FadeIn()
        {
            fadeDuration = defaultFadeDuration;
            desiredAlpha = 0f;
            if (!fading) StartCoroutine(DoFade());
        }

        /// <summary>
        /// make it dark
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public void FadeOut(float duration)
        {
            fadeDuration = duration;
            desiredAlpha = 1f;
            if (!fading) StartCoroutine(DoFade());
        }

        /// <summary>
        /// make it dark
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public void FadeOut()
        {
            fadeDuration = defaultFadeDuration;
            desiredAlpha = 1f;
            if (!fading) StartCoroutine(DoFade());
        }

        /// <summary>
        /// Sets default fading duration for Instance
        /// </summary>
        /// <param name="duration"></param>
        public void SetDefaultFadeDuration(float duration)
        {
            defaultFadeDuration = duration;
        }

        /// <summary>
        /// sets which color to fade to
        /// (0,0,0) is black
        /// (1,1,1) is white
        /// </summary>
        /// <param name="color"></param>
        public void SetFadeColor(Color color)
        {
            //Dont lose alpha
            color.a = fadeColor.a;

            fadeColor = color;
        }

        /// <summary>
        /// if you dont use this before using fade it will lag for a brief moment.
        /// </summary>
        public static void CreateInstance()
        {
            if (!Instance)
            {
                Debug.LogError("Something is seriously WRONG about fade class");
            }
        }

        /// <summary>
        /// Sometimes you gonna wanna change your cameras, use this after you change them
        /// </summary>
        public void UpdateActiveCameras()
        {
            AddCameraControlls();
        }

        /// <summary>
        /// is fading in progress?
        /// </summary>
        public bool IsFading
        {
            get
            {
                return fading;
            }
        }


    }



}
