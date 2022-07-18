using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Character Datas
    public GameObject CharacterContainer;
    public GameObject InstanciatedCharacterContainer;
    [SerializeField]
    public CharacterBody charBody;
    public GameObject rootComponent;
    public string pseudo;
    public Material BodyMaterial;
    public List<string> UsedAssets;
    #endregion

    public static CharacterManager Instance;
    private void Awake()
    {
        if (CharacterManager.Instance != null)
        {
            if (CharacterManager.Instance != this)
            {
                DestroyImmediate(this.gameObject, false);
                return;
            }

        }

        CharacterManager.Instance = this;

        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void SetCharacterBody(Dictionary<BodyPart, GameObject> equippedRef)
    {
        for(int i = 0; i < charBody.CharBody.Length; i++)
        {
            charBody.CharBody[i] = equippedRef[(BodyPart)i];
        }
    }

    public void AssemblateCharacter()
    {
        InstanciatedCharacterContainer = Instantiate(CharacterContainer);
        InstanciatedCharacterContainer.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        InstanciatedCharacterContainer.transform.position = new Vector3(0, 0.5f, 0);

        UsedAssets = new List<string>();
        foreach (GameObject go in charBody.CharBody)
            if(go != null)
                UsedAssets.Add(go.name.Replace("_Static", ""));

        List<Transform> transformsToDelete = new List<Transform>();
        Transform assetsRoot = null;
        foreach (Transform child in InstanciatedCharacterContainer.transform)
        {
            if (child.gameObject.name == "Root")
            {
                rootComponent = child.gameObject;
            }
            else if (child.gameObject.name == "Modular_Characters")
            {
                assetsRoot = child;
            }
        }

        AssignUnusedAssets(assetsRoot, ref transformsToDelete);

        for (int i = 0; i < transformsToDelete.Count; i++)
        {
            Debug.Log(transformsToDelete[i].name);
            Destroy(transformsToDelete[i].gameObject);
        }

        //for (int i = 0; i < charBody.CharBody.Length; i++)
        //{
        //    AssemblateCharacterPart(((BodyPart)i).ToString(), charBody.CharBody[i]);
        //}
    }

    public void AssignUnusedAssets(Transform asset, ref List<Transform> unusedAssets)
    {
        foreach (Transform child in asset)
        {
            if (child.childCount == 0 && !UsedAssets.Contains(child.name))
                unusedAssets.Add(child);
            else
                AssignUnusedAssets(child, ref unusedAssets);
        }
    }

    public void AssemblateCharacterPart(string characterPart, GameObject bodyPart)
    {
        
        Transform[] rootTransform = InstanciatedCharacterContainer.GetComponentsInChildren<Transform>();

        // declare target root transform
        Transform targetRoot = null;

        // find character parts parent object in the scene
        foreach (Transform t in rootTransform)
        {
            if (t.gameObject.name == characterPart)
            {
                targetRoot = t;
                break;
            }
        }
        if(targetRoot != null)
        {
            GameObject tmp = Instantiate(bodyPart);
            Mesh mesh = tmp.GetComponent<MeshFilter>().sharedMesh;
            SkinnedMeshRenderer smr = tmp.AddComponent<SkinnedMeshRenderer>();
            smr.sharedMesh = mesh;

            // Find the root bone that correspond to the BodyPart
            
            BodyPart bp = (BodyPart)System.Enum.Parse(typeof(BodyPart), characterPart);
            RootBones rootName = (RootBones)(int)bp;

            if (characterPart == BodyPart.Hips.ToString())
                rootName = RootBones.Hips_R;

            Transform rootBone = null;
            foreach(Transform t in rootTransform)
            {
                if (t.name == rootName.ToString())
                {
                    rootBone = t;
                    break;
                }
            }


            smr.rootBone = rootBone;
            
            tmp.transform.parent = targetRoot;
            tmp.GetComponent<Renderer>().material = BodyMaterial;
        }
        //if (!mat)
        //{
        //    if (go.getcomponent<skinnedmeshrenderer>())
        //        mat = go.getcomponent<skinnedmeshrenderer>().material;
        //}
        //else
        //{
        //    if (go.getcomponent<skinnedmeshrenderer>())
        //    {
        //        go.getcomponent<skinnedmeshrenderer>().material = mat;
        //    }
        //}
    }
}


