using CharacterCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISwitchSelector : Observer
{
    [SerializeField] private Text m_text;
    [SerializeField] private Button m_PrevButton;
    [SerializeField] private Button m_NextButton;

    [SerializeField] public CanvasGroup canvasGroup;
    [HideInInspector] [SerializeField] private string m_SelectedGender = "Male";

    [HideInInspector] [SerializeField] private string m_SelectedItem;
    [HideInInspector] [SerializeField] public string m_SelectedOption;
    [HideInInspector] [SerializeField] public int m_IndexOption = 0;
    [SerializeField] private List<string> m_Options = new List<string>();
    public MeshCombiner meshCombiner;

    public List<string> options
    {
        get
        {
            return this.m_Options;
        }
    }

    public string value
    {
        get
        {
            return this.m_SelectedItem;
        }
        set
        {
            this.SelectOption(value);
        }
    }

    public int selectedOptionIndex
    {
        get
        {
            return this.GetOptionIndex(this.m_SelectedItem);
        }
    }

    [System.Serializable] public class ChangeEvent : UnityEvent<int, string> { }
    public ChangeEvent onChange = new ChangeEvent();

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if(notificationType == NotificationType.GenderChange)
        {
            if ((string)value == "Male") onMaleButtonClick();
            else onFemaleButtonClick();
        }
    }
   
    public void Start()
    {
        foreach(var btn in FindObjectsOfType<GenderChange>())
            btn.RegisterObserver(this); 
        
        canvasGroup = GetComponent<CanvasGroup>();

        if (m_SelectedOption == "Hair") ChangeOptions(meshCombiner.allGender.all_Hair);
        SetOptions(meshCombiner.male);
    }

    public void SetOptions(CharacterObjectGroups character)
    {
        switch (m_SelectedOption)
        {   
            case "Eyebrow":
                ChangeOptions(character.eyebrow);
                m_text.text = "Eyebrow";
                m_SelectedItem = character.eyebrow[0].name;
                break;
            case "Head":
                ChangeOptions(character.headAllElements);
                m_text.text = "Head";
                m_SelectedItem = character.headAllElements[0].name;
                break;
            case "FacialHair":
                ChangeOptions(character.facialHair);
                break;
        }
    }
    private void ChangeOptions(List<GameObject> bodyPart)
    {
        options.Clear();
        int nbOfParts = 0;
        foreach (GameObject go in bodyPart)
        {
            nbOfParts++;
            options.Add("Option " + nbOfParts);
        }
    }

    public void onMaleButtonClick()
    {
        if (m_SelectedGender == "Male") return;

        m_SelectedGender = "Male";
        SetOptions(meshCombiner.male);

        if (m_SelectedOption == "FacialHair")
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void onFemaleButtonClick()
    {
        if (m_SelectedGender == "Female") return;

        m_SelectedGender = "Female";
        SetOptions(meshCombiner.female);

        if(m_SelectedOption == "FacialHair")
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void onPrevButtonClick()
    {
        int prevIndex = this.selectedOptionIndex - 1;

        if (prevIndex < 0)
            prevIndex = this.m_Options.Count - 1;
        if (prevIndex >= this.m_Options.Count)
            prevIndex = 0;

        this.SelectOptionByIndex(prevIndex);
    }
    public void onNextButtonClick()
    {
        int nextIndex = this.selectedOptionIndex + 1;

        if (nextIndex < 0)
            nextIndex = this.m_Options.Count + 1;
        if (nextIndex >= this.m_Options.Count)
            nextIndex = 0;

        this.SelectOptionByIndex(nextIndex);
    }

    public int GetOptionIndex(string optionValue)
    {
        if(this.m_Options != null && this.m_Options.Count > 0 && !string.IsNullOrEmpty(optionValue))
        {
            for (int i = 0; i < this.m_Options.Count; i++)
            {
                if (optionValue.Equals(this.m_Options[i], System.StringComparison.OrdinalIgnoreCase))
                    return i;
            }
        }

        return -1;
    }

    public void SelectOptionByIndex(int index)
    {
        if (index < 0 && index > this.m_Options.Count)
            return;

        string newOption = this.m_Options[index];
        if (!newOption.Equals(this.m_SelectedItem))
        {
            this.m_SelectedItem = newOption;

            this.TriggerChangeEvent();
        }
    }

    public void SelectOption(string optionValue)
    {
        if (string.IsNullOrEmpty(optionValue))
            return;

        int index = this.GetOptionIndex(optionValue);

        if (index < 0 && index > this.m_Options.Count)
            return;

        this.SelectOptionByIndex(index);
    }

    public void AddOption(string optionValue)
    {
        if (this.m_Options != null)
            this.m_Options.Add(optionValue);
    }

    protected virtual void TriggerChangeEvent()
    {
        if (this.m_text != null)
            this.m_text.text = this.m_SelectedItem;

        if (onChange != null)
            onChange.Invoke(this.selectedOptionIndex, this.m_SelectedItem);
    }
}
