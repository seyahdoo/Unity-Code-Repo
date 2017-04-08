///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///30.03.2017 -> Created by seyahdoo
///31.03.2017 -> Added Layermask to raycast

///usage:
///Attach to any camera or pointer and this will trigger CrosshairAware objects

///Dependancies:
///seyahdoo.crosshair.Target (For it to be useful of course!?)


using UnityEngine;

namespace seyahdoo.crosshair
{

    /// <summary>
    /// Attach this to a camera, and it will do its stuff
    /// </summary>
    [AddComponentMenu("Seyahdoo/Crosshair/Crosshair")]
    public class Crosshair : MonoBehaviour
    {
        /// <summary>
        /// Am i setted up?
        /// </summary>
        private static bool IsSettedUp
        {
            get
            {
                if (GameObject.FindObjectOfType<Crosshair>())
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair(Camera camera)
        {
            //Maybe setup a grosshair image? <- (i just found this typo... and i wont fix it :D)
            if (!camera)
                Debug.LogError(typeof(Crosshair).Name + " -> SetupCrosshair(Camera) -> camera cant be null");
            else
                SetupCrosshair(camera.gameObject);
            
        }
        
        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair(GameObject go)
        {
            if (!go)
                Debug.LogError(typeof(Crosshair).Name + " -> SetupCrosshair(GameObject) -> gameobject cant be null");
            else
                go.AddComponent<Crosshair>();
        }

        /// <summary>
        /// Call this if you are not sure crosshair is not setted up
        /// </summary>
        public static void SetupCrosshair()
        {
            if (!FindObjectOfType<Crosshair>())
                SetupCrosshair(Camera.main);
            else
                Debug.Log("Crosshair is already setted up?");
        }

        /// <summary>
        /// target that im focused
        /// </summary>
        private Target _target;
        /// <summary>
        /// collider that im focused
        /// </summary>
        private Collider _lastCollider;

        /// <summary>
        /// What layer to look for?
        /// </summary>
        public LayerMask layerMask = -1; //Default to everything

        void Update()
        {
            //do the raycasting
            RaycastHit hit;

            //if i found something
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,float.MaxValue,layerMask))
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
            if (_target)
            {
                ///Dont bug me about its being obsolete! i maid it!
                #pragma warning disable 612, 618
                _target.setFocus(false);
                #pragma warning restore 612, 618

                _target = null;
            }

            //im not hitting anything
            _lastCollider = collider;

            //if im not looking to nothingness
            if (collider)
            {
                //im checking if this collider is a Target?
                _target = collider.gameObject.GetComponent<Target>();

                //if so, im focused to that
                if (_target)
                {
                    ///Dont bug me about its being obsolete! i maid it!
                    #pragma warning disable 612, 618
                    _target.setFocus(true);
                    #pragma warning restore 612, 618
                }
            }

        }

    }



}


