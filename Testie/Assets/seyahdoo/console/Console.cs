using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.console
{
    
    public class Console : seyahdoo.other.Singleton<Console>
    {

        //maybe draw console in singleton inspector view

        void Update()
        {
            //Draw Console
            //Get Input
        }

        public static void Excecute(string line)
        {
            if (line == "Debug")
            {
                Debug.Log("Debug Works");
            }

            //throw new NotImplementedException();
        }

        public static string[] Commmandlist()
        {
            throw new NotImplementedException();
        }

        public delegate void LineOutDelegate(string line);

        /// <summary>
        /// This will be fired when i write a line in console output
        /// </summary>
        //public static event LineOutDelegate LineOutEvent;

        public class Command
        {
            string name;
            string[] paramaters;

            public void Excecute()
            {
                throw new NotImplementedException();
            }
        }

        public class UsableAttribute : Attribute
        {

        }

        public class DeveloperOnlyAttribute : Attribute
        {

        }


    }



}
