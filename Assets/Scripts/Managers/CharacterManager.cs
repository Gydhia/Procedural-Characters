using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Character Datas
    public GameObject CharacterContainer;
    private GameObject InstanciatedCharacterContainer;
    [SerializeField]
    public CharacterBody charBody;
    public GameObject rootComponent;
    public string pseudo;
    #endregion

    public static CharacterManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
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

        foreach (Transform child in InstanciatedCharacterContainer.transform)
        {
            if (child.gameObject.name == "Root")
            {
                rootComponent = child.gameObject;
                break;
            }
        }

        for (int i = 0; i < charBody.CharBody.Length; i++)
        {
            AssemblateCharacterPart(((BodyPart)i).ToString(), charBody.CharBody[i]);
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
            smr.rootBone = GameObject.Find(rootName.ToString()).transform;

            tmp.transform.parent = targetRoot;
            tmp.GetComponent<Renderer>().material = Resources.Load("ModularShader/FantasyHero", typeof(Material)) as Material;
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


