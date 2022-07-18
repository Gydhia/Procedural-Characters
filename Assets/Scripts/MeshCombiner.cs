using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharacterCreation
{

    public class MeshCombiner : MonoBehaviour
    {

        public static MeshCombiner Instance;

        [Header("Material")]
        public Material mat;

        [Header("Skin Colors")]
        public Color whiteSkin = new Color(1f, 0.8000001f, 0.682353f);
        public Color brownSkin = new Color(0.8196079f, 0.6352941f, 0.4588236f);
        public Color blackSkin = new Color(0.5647059f, 0.4078432f, 0.3137255f);
        public Color elfSkin = new Color(0.9607844f, 0.7843138f, 0.7294118f);

        [Header("Scar Colors")]
        public Color whiteScar = new Color(0.9294118f, 0.6862745f, 0.5921569f);
        public Color brownScar = new Color(0.6980392f, 0.5450981f, 0.4f);
        public Color blackScar = new Color(0.4235294f, 0.3176471f, 0.282353f);
        public Color elfScar = new Color(0.8745099f, 0.6588235f, 0.6313726f);

        [Header("Stubble Colors")]
        public Color whiteStubble = new Color(0.8039216f, 0.7019608f, 0.6313726f);
        public Color brownStubble = new Color(0.6588235f, 0.572549f, 0.4627451f);
        public Color blackStubble = new Color(0.3882353f, 0.2901961f, 0.2470588f);
        public Color elfStubble = new Color(0.8627452f, 0.7294118f, 0.6862745f);

        [Header("Body Art Colors")]
        public Color[] bodyArt = { new Color(0.0509804f, 0.6745098f, 0.9843138f), new Color(0.7215686f, 0.2666667f, 0.2666667f) };

        [HideInInspector]
        public List<GameObject> enabledObjects = new List<GameObject>();

        // character object lists
        // male list
        [HideInInspector]
        public CharacterObjectGroups male;

        // female list
        [HideInInspector]
        public CharacterObjectGroups female;

        // universal list
        [HideInInspector]
        public CharacterObjectListsAllGender allGender;

        public Gender gender = Gender.Male;
        public Race race;
        public SkinColor skinColor;
        public FacialHair facial;

        public Dictionary<BodyPart, GameObject> equippedRef;

        public int[] equipped;
        private int maxParts = 0;

        private List<GameObject>[,] allObjects;

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }


            equippedRef = new Dictionary<BodyPart, GameObject>();
            maxParts = Enum.GetValues(typeof(BodyPart)).Length;
            equipped = new int[maxParts];

            allObjects = new List<GameObject>[2, maxParts];

            BuildLists();

            for (int i = 0; i < maxParts; i++)
            {
                equipped[i] = -1;
            }

            enabledObjects.Clear();

            //default startup as male
            equipped[(int)BodyPart.HeadAllElements] = 0;
            equipped[(int)BodyPart.Eyebrow] = 2;
            equipped[(int)BodyPart.Torso] = 0;
            equipped[(int)BodyPart.Arm_Upper_Right] = 0;
            equipped[(int)BodyPart.Arm_Upper_Left] = 0;
            equipped[(int)BodyPart.Arm_Lower_Right] = 0;
            equipped[(int)BodyPart.Arm_Lower_Left] = 0;
            equipped[(int)BodyPart.Hand_Right] = 0;
            equipped[(int)BodyPart.Hand_Left] = 0;
            equipped[(int)BodyPart.Hips] = 0;
            equipped[(int)BodyPart.Leg_Right] = 0;
            equipped[(int)BodyPart.Leg_Left] = 0;

            EnableCharacter();
        }

        public void SetEquippedReferences()
        {
            for (int i = 0; i < equipped.Length; i++)
            {
                if (equipped[i] != -1 && allObjects[(int)gender, i].Count > equipped[i])
                    equippedRef[(BodyPart)i] = allObjects[(int)gender, i][equipped[i]];
                else
                    equippedRef[(BodyPart)i] = null;
            }
        }

        private void EnableCharacter()
        {
            //activate the look
            for (int i = 0; i < maxParts; i++)
            {
                ActivateItem(i, equipped[i]);
            }
        }

        private void DisableCharacter()
        {
            //activate the look
            for (int i = 0; i < maxParts; i++)
            {
                DeactivateItem(i, equipped[i]);
            }
        }
        private void DeactivateItem(int itemType, int itemIndex)
        {
            //if we had a previous item 
            if (equipped[itemType] != -1 && equipped[itemType] < allObjects[(int)gender, itemType].Count)
            {
                allObjects[(int)gender, itemType][equipped[itemType]].SetActive(false);
            }
        }

        void ActivateItem(int itemType, int itemIndex)
        {
            if (itemIndex >= allObjects[(int)gender, itemType].Count)
            {
                itemIndex = -1;
            }
            //if we had a previous item 
            if (equipped[itemType] != -1)
            {
                if (equipped[itemType] < allObjects[(int)gender, itemType].Count)
                {
                    allObjects[(int)gender, itemType][equipped[itemType]].SetActive(false);
                }
                if (itemIndex != -1)
                {
                    equipped[itemType] = itemIndex;
                    BuildCharacterBody((BodyPart)itemType, allObjects[(int)gender, itemType][equipped[itemType]].name);
                    allObjects[(int)gender, itemType][equipped[itemType]].SetActive(true);
                }
            }
            else
            {
                if (itemIndex != -1)
                {
                    equipped[itemType] = itemIndex;
                    allObjects[(int)gender, itemType][equipped[itemType]].SetActive(true);
                }
            }
        }

        public void SetHead(int index, string name)
        {
            if (index < allObjects[(int)gender, (int)BodyPart.HeadAllElements].Count)
            {
                ActivateItem((int)BodyPart.HeadAllElements, index);
            }
        }

        public void SetEyebrows(int index, string name)
        {
            if (index < allObjects[(int)gender, (int)BodyPart.Eyebrow].Count)
            {
                ActivateItem((int)BodyPart.Eyebrow, index);
            }
        }

        public void SetHair(int index, string name)
        {
            //handle no hair option
            index--;
            if (index < allObjects[(int)gender, (int)BodyPart.Hairs].Count)
            {
                ActivateItem((int)BodyPart.Hairs, index);
            }
        }

        public void SetFacialHair(int index, string name)
        {
            if (gender == Gender.Female)
                return;
            //handle no hair option
            index--;
            if (index < allObjects[(int)gender, (int)BodyPart.FacialHair].Count)
            {
                ActivateItem((int)BodyPart.FacialHair, index);
            }
        }

        public void SetHairColor(string name, Color color)
        {
            mat.SetColor("_Color_Hair", color);
        }

        public void SetGender(string name)
        {
            Gender newGender;
            //string comparison yes!
            if (name == "Male")
            {
                newGender = Gender.Male;
            }
            else
            {
                newGender = Gender.Female;
            }
            if (newGender != gender)
            {
                DisableCharacter();
                gender = newGender;
                EnableCharacter();
            }
        }
      
        public void SetToMale()
        {
            SetGender("Male");
        }
        public void SetToFemale()
        {
            SetGender("Female");
        }
        private void BuildLists()
        {
            BuildList(male.headAllElements, "Male_Head_All_Elements");
            BuildList(male.eyebrow, "Male_01_Eyebrows");
            BuildList(male.facialHair, "Male_02_FacialHair");
            BuildList(male.torso, "Male_03_Torso");
            BuildList(male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
            BuildList(male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
            BuildList(male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
            BuildList(male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
            BuildList(male.hand_Right, "Male_08_Hand_Right");
            BuildList(male.hand_Left, "Male_09_Hand_Left");
            BuildList(male.hips, "Male_10_Hips");
            BuildList(male.leg_Right, "Male_11_Leg_Right");
            BuildList(male.leg_Left, "Male_12_Leg_Left");

            //add them to the male list
            allObjects[0, (int)BodyPart.HeadAllElements] = male.headAllElements;
            allObjects[0, (int)BodyPart.Eyebrow] = male.eyebrow;
            allObjects[0, (int)BodyPart.FacialHair] = male.facialHair;
            allObjects[0, (int)BodyPart.Torso] = male.torso;
            allObjects[0, (int)BodyPart.Arm_Upper_Right] = male.arm_Upper_Right;
            allObjects[0, (int)BodyPart.Arm_Upper_Left] = male.arm_Upper_Left;
            allObjects[0, (int)BodyPart.Arm_Lower_Right] = male.arm_Lower_Right;
            allObjects[0, (int)BodyPart.Arm_Lower_Left] = male.arm_Lower_Left;
            allObjects[0, (int)BodyPart.Hand_Right] = male.hand_Right;
            allObjects[0, (int)BodyPart.Hand_Left] = male.hand_Left;
            allObjects[0, (int)BodyPart.Hips] = male.hips;
            allObjects[0, (int)BodyPart.Leg_Right] = male.leg_Right;
            allObjects[0, (int)BodyPart.Leg_Left] = male.leg_Left;

            //add common parts to both lists
            allObjects[0, (int)BodyPart.Hairs] = allGender.all_Hair;
            allObjects[1, (int)BodyPart.Hairs] = allGender.all_Hair;
            
            //build out female lists
            BuildList(female.headAllElements, "Female_Head_All_Elements");
            BuildList(female.eyebrow, "Female_01_Eyebrows");
            BuildList(female.facialHair, "Female_02_FacialHair");
            BuildList(female.torso, "Female_03_Torso");
            BuildList(female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
            BuildList(female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
            BuildList(female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
            BuildList(female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
            BuildList(female.hand_Right, "Female_08_Hand_Right");
            BuildList(female.hand_Left, "Female_09_Hand_Left");
            BuildList(female.hips, "Female_10_Hips");
            BuildList(female.leg_Right, "Female_11_Leg_Right");
            BuildList(female.leg_Left, "Female_12_Leg_Left");

            //add them to the female list
            allObjects[1, (int)BodyPart.HeadAllElements] = female.headAllElements;
            allObjects[1, (int)BodyPart.Eyebrow] = female.eyebrow;
            allObjects[1, (int)BodyPart.FacialHair] = female.facialHair;
            allObjects[1, (int)BodyPart.Torso] = female.torso;
            allObjects[1, (int)BodyPart.Arm_Upper_Right] = female.arm_Upper_Right;
            allObjects[1, (int)BodyPart.Arm_Upper_Left] = female.arm_Upper_Left;
            allObjects[1, (int)BodyPart.Arm_Lower_Right] = female.arm_Lower_Right;
            allObjects[1, (int)BodyPart.Arm_Lower_Left] = female.arm_Lower_Left;
            allObjects[1, (int)BodyPart.Hand_Right] = female.hand_Right;
            allObjects[1, (int)BodyPart.Hand_Left] = female.hand_Left;
            allObjects[1, (int)BodyPart.Hips] = female.hips;
            allObjects[1, (int)BodyPart.Leg_Right] = female.leg_Right;
            allObjects[1, (int)BodyPart.Leg_Left] = female.leg_Left;

            // build out all gender lists
            BuildList(allGender.all_Hair, "All_01_Hair");

            // Base datas
            BuildCharacterBody(BodyPart.Hairs, allGender.all_Hair[equipped[(int)BodyPart.HeadAllElements]].name);
            BuildCharacterBody(BodyPart.HeadAllElements, male.headAllElements[equipped[(int)BodyPart.HeadAllElements]].name);
            BuildCharacterBody(BodyPart.Eyebrow, male.eyebrow[equipped[(int)BodyPart.Eyebrow]].name);
            BuildCharacterBody(BodyPart.FacialHair, male.facialHair[equipped[(int)BodyPart.FacialHair]].name);
            BuildCharacterBody(BodyPart.Torso, male.torso[equipped[(int)BodyPart.Torso]].name);
            BuildCharacterBody(BodyPart.Arm_Upper_Right, male.arm_Upper_Right[equipped[(int)BodyPart.Arm_Upper_Right]].name);
            BuildCharacterBody(BodyPart.Arm_Upper_Left, male.arm_Upper_Left[equipped[(int)BodyPart.Arm_Upper_Left]].name);
            BuildCharacterBody(BodyPart.Arm_Lower_Right, male.arm_Lower_Right[equipped[(int)BodyPart.Arm_Lower_Right]].name);
            BuildCharacterBody(BodyPart.Arm_Lower_Left, male.arm_Lower_Left[equipped[(int)BodyPart.Arm_Lower_Left]].name);
            BuildCharacterBody(BodyPart.Hand_Right, male.hand_Right[equipped[(int)BodyPart.Hand_Right]].name);
            BuildCharacterBody(BodyPart.Hand_Left, male.hand_Left[equipped[(int)BodyPart.Hand_Left]].name);
            BuildCharacterBody(BodyPart.Hips, male.hips[equipped[(int)BodyPart.Hips]].name);
            BuildCharacterBody(BodyPart.Leg_Right, male.leg_Right[equipped[(int)BodyPart.Leg_Right]].name);
            BuildCharacterBody(BodyPart.Leg_Left, male.leg_Left[equipped[(int)BodyPart.Leg_Left]].name);
        }
        public void BuildCharacterBody(BodyPart bodyPart, string name, bool isAdded = false)
        {
            name += "_Static";
            if (equippedRef.ContainsKey(bodyPart))
                equippedRef[bodyPart] = Resources.Load("ModularCharacter_StaticParts/" + name, typeof(GameObject)) as GameObject;
            else
                equippedRef.Add(bodyPart, Resources.Load("ModularCharacter_StaticParts/" + name, typeof(GameObject)) as GameObject);
        }

        void BuildList(List<GameObject> targetList, string characterPart)
        {
            Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>();

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

            // clears targeted list of all objects
            targetList.Clear();

            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameobject index i
                GameObject go = targetRoot.GetChild(i).gameObject;

                // disable child object
                go.SetActive(false);

                // add object to the targeted object list
                targetList.Add(go);

                // collect the material for the random character, only if null in the inspector;
                if (!mat)
                {
                    if (go.GetComponent<SkinnedMeshRenderer>())
                        mat = go.GetComponent<SkinnedMeshRenderer>().material;
                }
                else
                {
                    if (go.GetComponent<SkinnedMeshRenderer>())
                    {
                        go.GetComponent<SkinnedMeshRenderer>().material = mat;
                    }
                }
            }
        }

        public void GenerateCharacter()
        {
            CharacterManager.Instance.SetCharacterBody(equippedRef);
            CharacterManager.Instance.pseudo = "aa";

            SceneManager.LoadScene(1);
        }

    }

}