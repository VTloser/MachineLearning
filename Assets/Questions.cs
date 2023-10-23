using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;
public class Questions : MonoBehaviour
{
    private Question question;

    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Text title;
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Button checkButton;
    
    public void Init(Question question)
    {
        this.question = question;

        if (question.multipleChoice)
            toggleGroup.allowSwitchOff = false;
        else
            toggleGroup.allowSwitchOff = true;

        toggleGroup = this.GetComponent<ToggleGroup>();
        toggles = this.GetComponentsInChildren<Toggle>(true);

        title.text = question.title;
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
            toggles[i].GetComponentInChildren<Text>().text = question.options[i];
        }
        
        checkButton.onClick.AddListener(() =>
        {
            List<int> checklist = new List<int>();
            for (int i = 1; i <= toggles.Length; i++)
            {
                if (toggles[i].isOn)
                    checklist.Add(i);
            }

            Debug.Log(question.Check(checklist.ToArray()));
        });

    }
    
    
}

public class Question
{
    /// <summary> 题目 </summary>
    public string title;

    /// <summary> 是否多选 </summary>
    public bool multipleChoice;

    /// <summary> 选项   </summary>
    public string[] options;

    /// <summary> 正确答案   </summary>
    public int[] answer;

    /// <summary> 选错事件 </summary>
    public Action errorEvents;

    /// <summary> 正确事件 </summary>
    public Action correctEvents;

    /// <summary> 选项事件 </summary>
    public Action[] optionsEvents;
    

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="options"></param>
    /// <param name="answer"></param>
    /// <param name="multipleChoice"></param>
    /// <param name="errorEvents"></param>
    /// <param name="correctEvents"></param>
    /// <param name="optionsEvents"></param>
    public Question(string title, string[] options, bool multipleChoice = false,
        Action errorEvents = null,
        Action correctEvents = null, Action[] optionsEvents = null, params int[] answer)
    {
        this.title = title;
        this.options = options;
        this.answer = answer;

        this.multipleChoice = multipleChoice;
        this.errorEvents = errorEvents;
        this.correctEvents = correctEvents;
        this.optionsEvents = optionsEvents;
    }

    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="num"></param>
    public string Check(params int[] num)
    {
        if (!multipleChoice)
        {
            if (num.Length != answer.Length || num[0] != answer[0])
            {
                errorEvents?.Invoke();
                return $"选择错误|正确选项为{OptionConversion(answer[0])}";
            }
            else
            {
                correctEvents?.Invoke();
                return "选择正确";
            }
        }
        else
        {
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] != answer[i])
                {
                    string temp = "选择错误|正确选项为";
                    for (int j = 0; j < answer.Length; j++)
                    {
                        temp += OptionConversion(answer[j]);
                    }
                    errorEvents?.Invoke();
                    return temp;
                }
            }
            correctEvents?.Invoke();
            return "选择正确";
        }
    }

    private string OptionConversion(int num) => num switch
    {
        1 => "A",
        2 => "B",
        3 => "C",
        4 => "D",
        _ => $"未注册此类型{num}",
    };
}