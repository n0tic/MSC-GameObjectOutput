using System;
using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

/*
 DONT FORGET TO ADD REFERENCE TO:
    MSCLoader
    PlayMaker
    UnityEngine

 I always use this too:
    Assenbly.CSharp
*/

namespace SceneObjects
{
    public class SceneObjects : Mod
    {
        public override string ID => "SceneObjects";
        public override string Name => "SceneObjects";
        public override string Author => "N0tiC";
        public override string Version => "1.0";

        //Set this to true if you will be load custom assets from Assets folder.
        //This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        //Make a keybind
        private Keybind create_Object_Dump = new Keybind("objectDump", "Create dump of all objects in scene.", KeyCode.F5); // Create a keybind..

        //Called when mod is loading
        public override void OnLoad()
        {
            //Print out  console message.
            ModConsole.Print("SceneObjects: Loaded");
        }

        // Update is called once per frame
        public override void Update()
        {
            //If pressed..
            if (create_Object_Dump.IsDown())
                DumpObjects(); // Run this
        }

        /* "Debug" */
        private void DumpObjects()
        {
            //We put it in a try block.
            try
            {
                //Path to debug.txt dump. (Same folder as mysummercar.exe)
                string file_path = "ChildObjects.txt";

                //Declare an array and define its name.
                GameObject[] allObjects;

                //Declare an StreamWriter and define its name but also initialize it with given parmiters
                System.IO.StreamWriter sr = System.IO.File.CreateText(file_path);

                //We Initialize allObjects and give it the array values of GameObjects
                allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                //We write first line of the document to give it a "header" like appearance.
                sr.WriteLine("SceneObjects=>FindObjectsOfType<GameObject>:");

                //For every GameObject in allObjects we print the name of the object in to the file "SceneObjects.txt"
                foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                {
                    var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);
                    {
                        if (fsm.gameObject.transform.root.name == "Database")
                        {
                            sr.WriteLine(fsm.gameObject.name);
                        }

                    }
                }
				//We write down the name of each object in the array.
                foreach (GameObject obj in allObjects)
                {
                    sr.WriteLine(obj.name);
                }

                //We close the StreamWriter...
                sr.Close();
                //Clean up console.. 
                ModConsole.Clear();
                //Print out a success message in console.
                ModConsole.Print("SceneObjects: Success creating " + file_path);
            }
            catch (System.Exception) { ModConsole.Print("Something went wrong..."); }


        }
    }
}