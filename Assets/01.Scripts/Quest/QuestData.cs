using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string questTitle = "���ο� ����Ʈ";

    [TextArea(2, 4)]
    public string description = "����Ʈ ������ �Է��ϼ���";
    public Sprite questIcon;

    [Header("����Ʈ ����")]
    public QuestType questType;
    public int targetAmount = 1;

    [Header("��� ����Ʈ�� (Delivery)")]
    public Vector3 deliveryPosition;
    public float deliveryRedius = 3f;

    [Header("����/��ȣ�ۿ� ����Ʈ��")]
    public string targetTag = "";

    [Header("����")]
    public int experienceReward = 100;
    public string rewardMessage = "����Ʈ �Ϸ�";

    [Header("����Ʈ ����")]
    public QuestData nextQuest;

    //��Ÿ�� ������(������� ����)
    [System.NonSerialized] public int currentProgress = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompleted = false;

    public void Initialize()
    {
        currentProgress = 0;
        isActive = false;
        isCompleted = false;
    }

    public bool IsComplete()
    {
        switch(questType)
        {
            case QuestType.Delivery:
                return currentProgress >= 1;

            case QuestType.Collect:
            case QuestType.Interact:
                return currentProgress >= targetAmount;

            default:
                return false;
        }
    }

    public float GetProgressPercentage()
    {
        if(targetAmount <= 0) return 0;
        return Mathf.Clamp01((float)currentProgress / targetAmount);
    }

    public string GetProgressText()
    {
        switch(questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "��޿Ϸ�" : "�������� �̵��ϼ���";
                
            case QuestType.Collect:
            case QuestType.Interact:
                return $"{currentProgress} / {targetAmount}";

            default:
                return "";
        }
    }
}
