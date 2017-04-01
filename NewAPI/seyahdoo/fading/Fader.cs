using UnityEngine;
using seyahdoo.fading.singleton;

/// <summary>
/// Fading Simplified
/// </summary>
namespace seyahdoo.fading
{
    
    /// <summary>
    /// Simple static wrapper for FaderSingleton class
    /// </summary>
    public static class Fader
    {
        
        /// <summary>
        /// make it bright
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public static void FadeIn(float duration)
        {
            FaderSingleton.Instance.FadeIn(duration);
        }

        /// <summary>
        /// make it bright
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public static void FadeIn()
        {
            FaderSingleton.Instance.FadeIn();
        }

        /// <summary>
        /// make it dark
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public static void FadeOut(float duration)
        {
            FaderSingleton.Instance.FadeOut(duration);
        }

        /// <summary>
        /// make it dark
        /// </summary>
        /// <param name="duration">Animation duration as seconds</param>
        public static void FadeOut()
        {
            FaderSingleton.Instance.FadeOut();
        }

        /// <summary>
        /// Sets default fading duration for Fader.Instance
        /// </summary>
        /// <param name="duration"></param>
        public static void SetDefaultFadeDuration(float duration)
        {
            FaderSingleton.Instance.SetDefaultFadeDuration(duration);
        }

        /// <summary>
        /// sets which color to fade to
        /// (0,0,0) is black
        /// (1,1,1) is white
        /// </summary>
        /// <param name="color"></param>
        public static void SetFadeColor(Color color)
        {
            FaderSingleton.Instance.SetFadeColor(color);
        }

        /// <summary>
        /// if you dont use this before using fade it will lag for a brief moment.
        /// </summary>
        public static void CreateFaderInstance()
        {
            FaderSingleton.CreateInstance();
        }

        /// <summary>
        /// Sometimes you gonna wanna change your cameras, use this after you change them
        /// </summary>
        public static void UpdateActiveCameras()
        {
            FaderSingleton.Instance.UpdateActiveCameras();
        }

        /// <summary>
        /// is fading in progress?
        /// </summary>
        public static bool IsFading
        {
            get
            {
                return FaderSingleton.Instance.IsFading;
            }
        }
        

    }


}
