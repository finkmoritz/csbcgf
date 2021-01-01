// ---------------------------------------------------------------------------------------------
// <copyright>PhotonNetwork Framework for Unity - Copyright (C) 2020 Exit Games GmbH</copyright>
// <author>developer@exitgames.com</author>
// ---------------------------------------------------------------------------------------------

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Photon.Pun.Simple.Assists
{
    public static class TutorialAssists
    {
        [MenuItem(AssistHelpers.TUTORIAL_FOLDER + "Add Basic NPC", false, AssistHelpers.TUTORIAL_PRIORITY)]
        public static void AddBasicNPC()
        {
            // Create root dummy
            var par = new GameObject("Example NPC");
            par.transform.position = new Vector3(0, 0, 3);
            par.transform.eulerAngles = new Vector3(0, 90, 0);
            var rb = par.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            var mover = par.AddComponent<SyncAdditiveMover>();
            mover.autoSync = true;
            mover.rotDef.addVector = new Vector3(0, 30, 0);
            mover.posDef.addVector = new Vector3(0, 0, 1.75f);

            // Create visible object
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.parent = par.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1f, 2f, 1f);
            go.name = "Visible Object";



            Selection.activeGameObject = par;
        }

        [MenuItem(AssistHelpers.TUTORIAL_FOLDER + "Create Starting Scene", false, AssistHelpers.TUTORIAL_PRIORITY)]
        public static void CreateEmptyScene()
        {
            //var scene = EditorSceneManager.GetActiveScene();
            var camobj = GameObject.FindGameObjectWithTag("MainCamera");
            if (!camobj)
            {
                var cam = GameObject.FindObjectOfType<Camera>();
                if (cam)
                    camobj = cam.gameObject;
            }

            if (camobj)
            {
                camobj.transform.position = new Vector3(0, 1f, -5f);
                Debug.Log("Assist moved Camera.");
            }

            var par = new GameObject("Boundaries").transform;

            var floormat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Photon/Simple/Example/Materials/SimpleFloor.mat") as Material;
            var wallmat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Photon/Simple/Example/Materials/SimpleWall.mat") as Material;

            var floorscl = new Vector3(2, 10, 10);
            var wallscl = new Vector3(10f, 1, 10f);
            const string FLR_NAME = "Floor Tile ";
            const string WALL_NAME = "Wall ";

            CreatePlane(FLR_NAME + "1", floormat, new Vector3(-4, -1, 0), new Vector3(0, 0, 0), floorscl).transform.parent = par;
            CreatePlane(FLR_NAME + "2", floormat, new Vector3(-2, -1, 0), new Vector3(0, 0, 0), floorscl).transform.parent = par;
            CreatePlane(FLR_NAME + "3", floormat, new Vector3(0, -1, 0), new Vector3(0, 0, 0), floorscl).transform.parent = par;
            CreatePlane(FLR_NAME + "4", floormat, new Vector3(2, -1, 0), new Vector3(0, 0, 0), floorscl).transform.parent = par;
            CreatePlane(FLR_NAME + "5", floormat, new Vector3(4, -1, 0), new Vector3(0, 0, 0), floorscl).transform.parent = par;

            CreatePlane(WALL_NAME + "Back", wallmat, new Vector3(0, -1f, 5), new Vector3(-90, 0, 0), wallscl).transform.parent = par;
            CreatePlane(WALL_NAME + "Right", wallmat, new Vector3(5, -1f, 0), new Vector3(0, 0, 90), wallscl).transform.parent = par;
            CreatePlane(WALL_NAME + "Back", wallmat, new Vector3(-5, -1f, 0), new Vector3(0, 0, -90), wallscl).transform.parent = par;

#if UNITY_2020_1_OR_NEWER
            Lightmapping.lightingSettings = new LightingSettings() { autoGenerate = true };
#endif
        }

        private static GameObject CreatePlane(string name, Material material, Vector3 pos, Vector3 rot, Vector3 scl)
        {
            var go = new GameObject(name);
            go.transform.position = pos;
            go.transform.eulerAngles = rot;

            /// Create a basic ground plane
            var plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
            plane.transform.parent = go.transform;
            plane.transform.localPosition = new Vector3(0, -scl.y * .5f, 0);
            plane.transform.localEulerAngles = new Vector3(0, 0, 0);
            plane.transform.localScale = scl;

            if (material)
                plane.GetComponent<MeshRenderer>().material = material;

            return go;
        }
    }

}

#endif
