using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class BattleSimulate : EditorWindow
{
    private Vector2 scrollPosition;
    private bool isSimulating = false;
    public BattleTeam leftTeam;
    public BattleTeam rightTeam;

    [MenuItem("Tools/战斗模拟器")]
    public static void ShowWindow()
    {
        GetWindow<BattleSimulate>("BattleSimulate");
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("战斗模拟器", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        DrawControlButtons();
        if (GUILayout.Button("重置", GUILayout.Height(30)))
        {
            isSimulating = false;
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawControlButtons()
    {
        EditorGUILayout.BeginHorizontal();

        GUI.enabled = !isSimulating;
        if (GUILayout.Button("开始战斗", GUILayout.Height(30)))
        {
            StartBattle();
        }
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
    }

    private void StartBattle()
    {
        if(!Application.isPlaying)
        {
            EditorUtility.DisplayDialog("提示", "当前非运行中，请在运行模式下使用战斗模拟", "确定");
            return;
        }
        isSimulating = true;
        leftTeam = new BattleTeam(0,new List<BattleUnit> { new BattleUnit(0, (int)UnitTeamType.OWNER) });
        rightTeam = new BattleTeam(1, new List<BattleUnit> { new BattleUnit(300002, (int)UnitTeamType.ENEMY) });
        BattleProxy.EntryBattle(leftTeam, rightTeam, BattleType.Normal);
    }
}