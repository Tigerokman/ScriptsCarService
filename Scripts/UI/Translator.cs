using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{
    private static int _languageId;
    private static List<TranslatableText> _listID = new List<TranslatableText>();
    private static string _language = "Language";
    private static string _en = "EN_Language";
    private static string _ru = "RU_Language";
    private static string _menuFont = "MenuFont";

    private static string[,] LineText =
    {
        // "", //

        #region ����������
        {
            "Start", //0
            "Upgrade", //1
            "Sell", //2
            "Drop", //3
            "Take", //4
            "Door", //5
            "Plastic", //6
            "Steel", //7
            "Material", //8
            "Engine", //9
            "Wheel", //10
            "Trade", //11
            "Speed", //12
            "BagSize", //13
            "Build", //14
            "Congratulations!!!", //15
            "Score:", //16
            "Menu", //17
            "Total value:", //18
            "Trash", //19
            "Collect manufactured parts and make something new out of them.", //20
            "Build new recycling buildings and upgrade old ones.", //21
            "Sell parts and earn money and points.", //22
            "Upgrade your character to work faster.", //23
            "Build cars, so you will earn more money.", //24
            "1 time per game, you can watch ads and double your points for as much as 3 minutes!!!", //25
            "Earn as many points as you can and place among the best of the best!", //26
            "You can end the game at any time and save your score for the leaderboard.", //27
            "Finish the game?", //28
            "Yes", //29
            "No", //30
            "Log in to see the leaderboard." //31
        },
        #endregion

        #region �������
        {
            "�����", //0
            "��������", //1
            "�������", //2
            "������", //3
            "�����", //4
            "�����", //5
            "�������", //6
            "�����", //7
            "��������", //8
            "���������", //9
            "������", //10
            "��������", //11
            "��������", //12
            "�����������", //13
            "���������", //14
            "�����������!!!", //15
            "����:", //16
            "����", //17
            "C��������:", //18
            "�����", //19
            "������� �������������� ������ � ����� �� ��� ���-�� �����.", //20
            "����� ����� ������ �� ����������� � ������� ������.", //21
            "�������� ������ � ����������� ������ � ����.", //22
            "������� ������ ���������, ����� ������� ��������.", //23
            "������� ������, ��� �� ����������� ������ �����.", //24
            "1 ��� �� ����, ������ ���������� ������� � ������� ���� �� ����� 3 ������!!!", //25
            "��������� ��� ����� ������ ����� � ����� ����� ����� ������ �� ������!", //26
            "�� ������ � ����� ������ ��������� ���� � ��������� ���� ��������� ��� ������� �������.", //27
            "��������� ����?", //28
            "��", //29
            "���", //30
            "������������� ����� ������� ������� �������." //31
        },
        #endregion
    };

    static public void SelectLanguage(int id)
    {
        _languageId = id;
        UpdateAllText();
    }

    static public string GetText(int textKey)
    {
        return LineText[_languageId, textKey];
    }

    static public int AddText(TranslatableText idText)
    {
        _listID.Add(idText);
        return _listID.Count - 1;
    }

    static public void Delete(TranslatableText idText)
    {
        _listID.Remove(idText);
    }

    static public void UpdateAllText()
    {
        for (int i = 0; i < _listID.Count; i++)
        {
            UpdateCurrentText(i);
        }
    }

    static public void UpdateCurrentText(int id)
    {
        _listID[id].UIText.text = LineText[_languageId, _listID[id].TextID];

        Debug.Log(PlayerPrefs.GetInt(_language));

        if (PlayerPrefs.GetInt(_language) == 1)
        {
            if (_listID[id].IsClassic == false)
                _listID[id].UIText.font = Resources.Load<TMP_FontAsset>(_menuFont);
            else
            {
                _listID[id].UIText.font = Resources.Load<TMP_FontAsset>(_ru);
                _listID[id].UIText.fontStyle = FontStyles.Normal;
            }

        }
        else
        {
            if (_listID[id].IsClassic == false)
                _listID[id].UIText.font = Resources.Load<TMP_FontAsset>(_menuFont);
            else
            {
                _listID[id].UIText.font = Resources.Load<TMP_FontAsset>(_en);
                _listID[id].UIText.fontStyle = FontStyles.Bold;
            }
        }
    }
}
