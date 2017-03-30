///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///30.03.2017 -> Created by seyahdoo

///usage:
///Attach to any camera or pointer and this will trigger CrosshairAware objects

///Dependancies:
///None


using UnityEngine;

namespace seyahdoo.crosshair
{
    
    /// <summary>
    /// Attach this to a camera, and it will do its stuff
    /// </summary>
    public class Crosshair : MonoBehaviour
    {


        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair(Camera camera)
        {

            //Maybe setup a grosshair image? (i just found this typo... and i wont fix it :D)
            SetupCrosshair(camera.gameObject);
            
        }
        
        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair(GameObject go)
        {
            go.AddComponent<Crosshair>();
        }

        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair()
        {
            foreach (Camera cam in Camera.allCameras)
            {
                SetupCrosshair(cam);
            }

        }

        private CrosshairAware _crosshairAware;
        private Collider _lastCollider;

        void Update()
        {
            //do the raycasting
            RaycastHit hit;

            //if i found something
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                //did i found a new thing?
                if(_lastCollider != hit.collider)
                {
                    //i should focus to that
                    focusToCollider(hit.collider);
                }
            }
            else
            {
                //im not hitting anything, i should focus to nothing
                focusToCollider(null);
            }
        }

        /// <summary>
        /// Change current focus
        /// </summary>
        /// <param name="collider">collider?</param>
        void focusToCollider(Collider collider)
        {
            //if i was focused to anything im not focused anymore
            if (_crosshairAware)
            {
                ///Dont bug me about its being obsolete! i maid it!
                #pragma warning disable 612, 618
                _crosshairAware.setFocus(false);
                #pragma warning restore 612, 618

                _crosshairAware = null;
            }

            //im not hitting anything
            _lastCollider = collider;

            //if im not looking to nothingness
            if (collider)
            {
                //im checking if this collider is Crosshair Aware?
                _crosshairAware = collider.gameObject.GetComponent<CrosshairAware>();

                //if so, im focused to that
                if (_crosshairAware)
                {
                    ///Dont bug me about its being obsolete! i maid it!
                    #pragma warning disable 612, 618
                    _crosshairAware.setFocus(true);
                    #pragma warning restore 612, 618
                }
            }

        }

    }



}
