using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.Common;
using BattleCoder.GamePlayUi;
using BattleCoder.Map;
using BattleCoder.PlayGame;
using BattleCoder.PlayGame.MultiPlayGame;
using BattleCoder.Sound;
using BattleCoder.StageTransition;
using UnityEngine;
using UnityEngine.Serialization;

//天地創造をする全てを支配する全知全能の神
namespace BattleCoder
{
    public class God : MonoBehaviour
    {
        [SerializeField] BotEntity botEntityPrefab;

        [SerializeField] BotEntity botEntityPrefab2P;

        [SerializeField] TileMapInfoManager tileMapInfoManagerPrefab;
        [SerializeField] CameraFollower cameraFollower;
        [SerializeField] PlayerHpPresenter playerHpPresenter;

        [SerializeField] RunButtonEvent runButtonEvent;
        [SerializeField] ScriptText scriptText;
        [SerializeField] BulletEntity bulletPrefab;
        [SerializeField] ErrorMsg errorMsg;
        [SerializeField] SoundManager soundManager;
        [SerializeField] MeleeAttackEntity meleeAttackPrefab;
        [SerializeField] ConsoleWindow consoleWindow;
        [SerializeField] ProcessScrollViewPresenter processScrollViewPresenter;


        IPlayGame playGame;

        void Awake()
        {
            if (StartGameInfo.StartGameInfo.IsSinglePlay == false)
                playGame = MultiPlayGameFactory.CreateMultiPlayGame(GetPlayGameInitData());
            else
                playGame = new SinglePlayGame(GetPlayGameInitData());
        }

        void Update()
        {
            playGame.Update();

            if (UnityEngine.Input.GetKeyDown(KeyCode.F5))
                if (consoleWindow.isActiveAndEnabled)
                    consoleWindow.Close();
                else
                    consoleWindow.Open();
        }

        private void OnDestroy()
        {
            playGame.Dispose();
        }

        PlayGameInitData GetPlayGameInitData()
        {
            var tileMapInfo = Instantiate(tileMapInfoManagerPrefab).Create(SelectedStageData.GetSelectedStageKind());

            return new PlayGameInitData(
                botEntityPrefab, botEntityPrefab2P,
                cameraFollower,
                playerHpPresenter, tileMapInfo,
                runButtonEvent, scriptText, bulletPrefab,
                errorMsg, soundManager, meleeAttackPrefab,
                processScrollViewPresenter
            );
        }
    }
}