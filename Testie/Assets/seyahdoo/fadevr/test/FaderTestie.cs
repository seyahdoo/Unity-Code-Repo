///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using System.Collections;
using UnityEngine;

namespace seyahdoo.fadevr.test
{

    /// <summary>
    /// Attach it to something to test "seyahdoo.fadevr.Fader"
    /// J-> auto test
    /// k-> fadein
    /// l-> fadeout
    /// Alpha Numeric buttons -> adjust time
    /// </summary>
    public class FaderTestie : MonoBehaviour
    {

        public float duration = 2f;
        public bool testRunning = false;

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.K))
            {
                Fader.FadeIn();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Fader.FadeOut();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if(!testRunning) StartCoroutine(AutoTest());
            }

            if (Input.anyKeyDown)
            {
                if(Input.inputString.Length > 0)
                {
                    if (char.IsDigit(Input.inputString[0]))
                    {
                        duration = int.Parse(Input.inputString[0].ToString());
                        Fader.SetDefaultFadeDuration(int.Parse(Input.inputString[0].ToString()));
                    }
                }
            }
            


        }
        
        //maybe usable
        void Start()
        {
            //Fader.CreateInstance();
            //StartCoroutine(doit());
        }

        IEnumerator AutoTest()
        {
            testRunning = true;
            Debug.Log("FaderTestie -> Auto Test Started");

            //Fader.CreateInstance();
            Fader.FadeOut(1);
            yield return new WaitForSeconds(2f);
            Fader.FadeIn(4);
            yield return new WaitForSeconds(2f);
            Fader.SetFadeColor(Color.blue);
            Fader.FadeOut(1);
            yield return new WaitForSeconds(2f);
            Fader.FadeIn(2);
            yield return new WaitForSeconds(2f);
            Fader.SetFadeColor(Color.black);
            Fader.FadeOut(1);
            yield return new WaitForSeconds(2f);
            Fader.FadeIn(2);
            yield return new WaitForSeconds(2f);
            Fader.FadeOut(1);

            Fader.FadeIn();

            Debug.Log("FaderTestie -> Auto Test finished");
            testRunning = false;
        }

    }

}
