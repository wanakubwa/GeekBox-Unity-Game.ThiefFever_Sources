using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GeekBox.LeaderboardsSystem
{
    [CreateAssetMenu(fileName = "LeaderboardDatabase.asset", menuName = "GeekBox/LeaderboardDatabase")]
    public class LeaderboardDatabase : ScriptableObject
    {
        #region Fields

        private static LeaderboardDatabase instance;

        [SerializeField]
        private TextAsset nicknamesCSV;

        [Space]
        [SerializeField]
        private List<ScoreRecord> records = new List<ScoreRecord>();

        #endregion

        #region Propeties


        public static LeaderboardDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<LeaderboardDatabase>("GameLeaderboards/LeaderboardDatabase");
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public List<ScoreRecord> Records {
            get => records; 
        }

        #endregion

        #region Methods

#if UNITY_EDITOR

        [Button]
        private void GenerateLeaderboardRecords(float minScore, float maxScore)
        {
            Records.Clear();
            string[] records = nicknamesCSV.text.Split('\n');

            GaussianRandom random = new GaussianRandom();

            for (int i = 0; i < records.Length; i++)
            {
                EditorUtility.DisplayProgressBar("Leader board calculation", "Calculating scores...", i / records.Length);

                if (string.IsNullOrEmpty(records[i]) == false)
                {
                    ScoreRecord record = new ScoreRecord(Mathf.RoundToInt(random.NextGaussian(minScore, maxScore, 0, 0.2f)), records[i]);
                    Records.Add(record);
                }
            }

            EditorUtility.ClearProgressBar();

            Records.Sort();
            Records.Reverse();
            SaveThisAsset();
        }

        public void SaveThisAsset()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

#endif

        #endregion

        #region Enums

        #endregion
    }
}

