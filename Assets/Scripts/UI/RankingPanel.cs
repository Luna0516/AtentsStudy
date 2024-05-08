using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RankingPanel : MonoBehaviour
{
    /// <summary>
    /// 패널에 붙어있는 랭킹라인들
    /// </summary>
    RankingLine[] rankingLines;

    /// <summary>
    /// 랭킹 기록된 이름들 (1 ~ 3)
    /// </summary>
    private string[] rankerNames;

    /// <summary>
    /// 랭킹 기록된 점수들 (1 ~ 3)
    /// </summary>
    private int[] highScores;

    /// <summary>
    /// 최대 랭크 갯수
    /// </summary>
    private const int rankCount = 3;

    /// <summary>
    /// 랭킹이 갱신된 인덱스
    /// </summary>
    private int updatedIndex = -1;

    /// <summary>
    /// JsonUtility 에서 사용할 폴더 경로
    /// </summary>
    private string path;

    /// <summary>
    /// JsonUtility 에서 사용할 파일 경로 ( 전체 경로 )
    /// </summary>
    private string fullPath;

    private void Awake()
    {
        // 랭킹라인 찾아놓기
        rankingLines = GetComponentsInChildren<RankingLine>();

        // 배열 할당 (3등까지)
        highScores = new int[rankCount];
        rankerNames = new string[rankCount];

        // 경로 구해 놓기
        path = $"{Application.dataPath}/Save/";
        fullPath = $"{path}Save.json";
    }

    private void Start()
    {
        // 데이터 로딩 해놓기 => 초기화도 한다
        LoadRankingData();

        if (GameManager.Inst != null)
        {
            GameManager.Inst.onGameEnd += (_) =>
            {
                int newScore = GameManager.Inst.Score;
                RankingUpdate(newScore);
                RefreshRankingLines();
            };        
        }
    }

    /// <summary>
    /// 랭킹 데이터 불러오기
    /// </summary>
    /// <returns>불러오기 성공 여부</returns>
    private bool LoadRankingData()
    {
        // 파일 및 폴더 있는지 확인
        bool result = Directory.Exists(path) && File.Exists(fullPath);

        if(result)
        {
            // 파일에 있는 텍스트 가져오기
            string json = File.ReadAllText(fullPath);

            // json형식으로 된 문자열을 파싱해서 SaveData형식으로 저장
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            rankerNames = loadedData.rankerNames;
            highScores = loadedData.scores;
        }
        else
        {
            // 디폴트값 세팅
            SetDefaultData();
        }

        RefreshRankingLines();

        return result;
    }

    /// <summary>
    /// 랭킹을 초기값으로 설정하는 함수
    /// </summary>
    private void SetDefaultData()
    {
        for (int i = 0; i < rankCount; i++)
        {
            rankerNames[i] = "Player";

            int score = 0;

            highScores[i] = score;

            // 랭킹라인들에 랭커의 이름과 점수 세팅
            rankingLines[i].SetData(rankerNames[i], highScores[i]);
        }

        SaveRankingData();
    }

    /// <summary>
    /// 현재 설정된 데이터 값에 맞게 UI 갱신
    /// </summary>
    private void RefreshRankingLines()
    {
        for (int i = 0; i < rankCount; i++)
        {
            if(updatedIndex == i)
            {
                rankingLines[i].SetData(rankerNames[i], highScores[i], true);
            }
            else
            {
                rankingLines[i].SetData(rankerNames[i], highScores[i]);
            }
        }

        // 인덱스 위치 초기화
        updatedIndex = -1;
    }

    /// <summary>
    /// 랭킹 업데이트 하는 함수
    /// </summary>
    /// <param name="score">새 점수</param>
    private void RankingUpdate(int score)
    {
        // 1~3등 까지
        for (int i = 0; i < rankCount; i++)
        {
            // 각 점수가 갱신된 점수보다 낮으면 뒤로 밀리기
            if (highScores[i] >= score) { continue; }

            // 뒤로 밀기
            for (int j = rankCount - 1; j > i; j--)
            {
                highScores[j] = highScores[j - 1];
                rankerNames[j] = rankerNames[j - 1];
            }

            highScores[i] = score;
            rankerNames[i] = GameManager.Inst.PlayerName;

            rankingLines[i].SetData(rankerNames[i], highScores[i], true);

            // 랭킹 위치 위치 기록
            updatedIndex = i;

            SaveRankingData();

            break;
        }
    }

    /// <summary>
    /// 파일을 저장하는 함수
    /// </summary>
    private void SaveRankingData()
    {
        // PlayerPrefs // JsonUtility
        SaveData saveData = new SaveData();

        // 데이터 복사
        saveData.rankerNames = rankerNames;
        saveData.scores = highScores;

        string json = JsonUtility.ToJson(saveData);

        // 폴더가 없으면
        if(!Directory.Exists(path))
        {
            // 경로에 폴더 만들기
            Directory.CreateDirectory(path);
        }

        // 최종 경로에 있는 파일에 모든 텍스트 저장
        File.WriteAllText(fullPath, json);
    }
}
