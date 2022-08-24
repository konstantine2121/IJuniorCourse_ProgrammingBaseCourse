using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    class WizardBossFightTask : IRunnable
    {
        private ConsoleRecord _actionsStatusBar;
        
        private Player _player;
        private Boss _boss;

        private int _cursorX = 0;
        private int _cursorY = 0;

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            var correctInputValues = Enum.GetValues(typeof(Player.AbilityType))
                .Cast<Player.AbilityType>();

            UpdateScene();
            
            while (_player.Dead == false && _boss.Dead == false)
            {
                if (_player.Invincible)
                {
                    _player.Invincible = false;
                    _player.abilities[Player.AbilityType.DemonCall].Status = Player.AbilityStatus.Available;
                    _player.abilities[Player.AbilityType.DemonAttack].Status = Player.AbilityStatus.Unavailable;
                }

                if (_player.Attacking)
                {
                    _player.abilities[Player.AbilityType.DemonCall].Status = Player.AbilityStatus.Available;
                    _player.abilities[Player.AbilityType.DemonAttack].Status = Player.AbilityStatus.Unavailable;                    
                }

                UpdateScene();

                var parsed = false;
                Player.AbilityType actionType = Player.AbilityType.SkipStep;

                while (parsed == false)
                {
                    SaveCursorPosition();
                    var input = Console.ReadKey();
                    UpdateScene();
                    RestoreCursorPosition();

                    if (Enum.TryParse(input.KeyChar.ToString(), out actionType) && correctInputValues.Contains(actionType))
                    {
                        parsed = true;
                    }
                }

                _player.PerformAction(actionType);

                if (_player.Attacking)
                {
                    _boss.health -= _player.damagePerHit;
                }

                if (_player.Invincible == false)//Проверки на смерть босса нет, т.к обмен ударами происходит одновременно
                {
                    _player.health -= _boss.damagePerHit;
                }

                UpdateScene();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Битва окончена!");

            if (_player.Dead && _boss.Dead)
            {
                ConsoleOutputMethods.Warning("На этом поле боя нету победителей!");
            }
            else if (_player.Dead)
            {
                ConsoleOutputMethods.Warning("Поражение!", ConsoleColor.Red);
            }
            else
            {
                ConsoleOutputMethods.Info("Победа!");
            }

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            Console.Clear();

            var horisontalOffset = 0;
            var horisontalBossOffset = 100;
            var playerVerticalOffset = 0;
            var bossVerticalOffset = 0;
            var actionsBarVeticalOffset = 9;

            _boss = new Boss(horisontalBossOffset, bossVerticalOffset);
            _player = new Player(horisontalOffset, playerVerticalOffset);

            _actionsStatusBar = new ConsoleRecord(horisontalOffset, actionsBarVeticalOffset);
            _actionsStatusBar.foregroundColor = ConsoleColor.Green;

            Console.CursorVisible = false;
        }

        private void UpdateScene()
        {
            Console.Clear();

            _boss.Update();
            _player.Update();
            UpdateActionsBar();
        }


        private void UpdateActionsBar()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Доступные действия: ");

            foreach (var abilityType in _player.abilities.Keys)
            {
                var ability = _player.abilities[abilityType];

                if (ability.Status == Player.AbilityStatus.Available)
                {
                    stringBuilder.AppendFormat("{0} {1, 2}, ", ability.Name, (int)abilityType);
                }
            }

            stringBuilder.AppendFormat("Пропустить ход {0}", (int) Player.AbilityType.SkipStep);

            _actionsStatusBar.text = stringBuilder.ToString();

            _actionsStatusBar.Update();
        }

        private void SaveCursorPosition()
        {
            _cursorX = Console.CursorLeft;
            _cursorY = Console.CursorTop;
        }

        private void RestoreCursorPosition()
        {
            Console.CursorLeft = _cursorX;
            Console.CursorTop = _cursorY;
        }

        #region Private Classes

        private class Player : Boss
        {
            public const int MaxHealth = 700;

            public readonly Dictionary<AbilityType, Ability> abilities = new Dictionary<AbilityType, Ability>();
            

            public enum AbilityStatus
            {
                Unavailable,                
                Available,
                Active
            }

            public enum AbilityType
            {
                DemonCall = 1,
                DemonAttack,
                DimensionalRift,
                SkipStep
            }

            public Player(int cursorLeft, int cursorTop) : base(cursorLeft, cursorTop)
            {
                text = "Player";
                foregroundColor = ConsoleColor.Cyan;

                _healthBar.foregroundColor = ConsoleColor.Green;
                _damageBar.foregroundColor = ConsoleColor.Red;

                InitAbilities();
                InitStartValues();
            }

            public bool Attacking
            {
                get { return abilities[AbilityType.DemonAttack].Status == Player.AbilityStatus.Active; }
            }

            public bool Invincible
            {
                get 
                { 
                    return abilities[AbilityType.DimensionalRift].Status == AbilityStatus.Active; 
                }
                set
                {
                    if (value)
                    {
                        abilities[AbilityType.DimensionalRift].Status = Player.AbilityStatus.Active;                        
                    }
                    else
                    {
                        abilities[AbilityType.DimensionalRift].Status = Player.AbilityStatus.Available;
                    }
                }
            }

            public void PerformAction(AbilityType actionType)
            {
                switch (actionType)
                {
                    case AbilityType.SkipStep:
                        abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                        abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        break;

                    case AbilityType.DemonCall:
                        if (abilities[AbilityType.DemonCall].Status == AbilityStatus.Active)
                        {
                            abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                            abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        }
                        else
                        {
                            abilities[AbilityType.DemonCall].Status = AbilityStatus.Active;
                            abilities[AbilityType.DemonAttack].Status = AbilityStatus.Available;
                        }
                        break;

                    case AbilityType.DemonAttack:
                        if (abilities[AbilityType.DemonCall].Status == AbilityStatus.Active 
                            && abilities[AbilityType.DemonAttack].Status == AbilityStatus.Available)
                        {
                            abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                            abilities[AbilityType.DemonAttack].Status = AbilityStatus.Active;
                        }
                        break;

                    case AbilityType.DimensionalRift:
                        abilities[AbilityType.DemonCall].Status = AbilityStatus.Unavailable;
                        abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        Invincible = true;
                        health += 250;
                        if (health > MaxHealth)
                        {
                            health = MaxHealth;
                        }
                        break;

                    default:
                        break;
                }
            }

            public override void Update()
            {
                base.Update();

                foreach (var ability in abilities.Values)
                {
                    ability.statusBar.Update();
                }
            }

            private void InitAbilities()
            {
                var verticalOffset = 4;

                var demonCall = new Ability();
                var demonAttack = new Ability();
                var dimensionalRift = new Ability();

                demonCall.Name = "Рашамон";
                demonCall.Info = "Призывает демона (Отнимает 100 хп игроку)";                
                demonCall.statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                demonCall.statusBar.text = demonCall.Name + ": " + demonCall.Info;
                demonCall.Status = AbilityStatus.Available;

                verticalOffset++;

                demonAttack.Name = "Хуганзакура";
                demonAttack.Info = "Демон наносит 100 ед. урона врагу и испаряется.";                
                demonAttack.statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                demonAttack.statusBar.text = demonAttack.Name + ": " + demonAttack.Info;
                demonAttack.Status = AbilityStatus.Unavailable;

                verticalOffset++;

                dimensionalRift.Name = "Разлом";
                dimensionalRift.Info = "Позволяет скрыться в разломе и восстановить 250 хп.";                
                dimensionalRift.statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                dimensionalRift.statusBar.text = dimensionalRift.Name + ": " + dimensionalRift.Info;
                dimensionalRift.Status = AbilityStatus.Available;

                abilities.Add(AbilityType.DemonCall, demonCall);
                abilities.Add(AbilityType.DemonAttack, demonAttack);
                abilities.Add(AbilityType.DimensionalRift, dimensionalRift);
            }

            private void InitStartValues()
            {
                health = 500;
                damagePerHit = 100;
            }

            public class Ability
            {
                public ConsoleRecord statusBar;

                private AbilityStatus _status;

                public string Name { get; set; }

                public string Info { get; set; }

                public AbilityStatus Status
                {
                    get
                    {
                        return _status;
                    }
                    set
                    {
                        _status = value;

                        if (statusBar != null)
                        {
                            switch (value)
                            {
                                case AbilityStatus.Unavailable:
                                    statusBar.foregroundColor = ConsoleColor.Red;
                                    break;
                                case AbilityStatus.Available:
                                    statusBar.foregroundColor = ConsoleColor.White;
                                    break;
                                case AbilityStatus.Active:
                                    statusBar.foregroundColor = ConsoleColor.Green;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private class Boss : ConsoleRecord
        {
            public int health = 1000;            
            public int damagePerHit = 200;

            protected ConsoleRecord _healthBar;
            protected ConsoleRecord _damageBar;

            public Boss(int cursorLeft, int cursorTop) : base (cursorLeft, cursorTop)
            {
                text = "Boss";
                foregroundColor = ConsoleColor.DarkRed;

                var verticalOffset = 1;
                _healthBar = new ConsoleRecord(
                    cursorLeft,
                    cursorTop+verticalOffset);
                
                verticalOffset++;

                _damageBar = new ConsoleRecord(
                    cursorLeft, 
                    cursorTop+verticalOffset);

                _healthBar.foregroundColor = ConsoleColor.Green;
                _damageBar.foregroundColor = ConsoleColor.Red;
            }

            public bool Dead
            {
                get { return health <= 0; }
            }

            public override void Update()
            {
                base.Update();

                _healthBar.text = "Health: " + health;
                _damageBar.text = "Damage: " + damagePerHit;

                _healthBar.Update();
                _damageBar.Update();
            }
        }

        private class ConsoleRecord
        {
            public string text;

            public ConsoleColor foregroundColor = ConsoleColor.White;

            public ConsoleRecord(int cursorLeft, int cursorTop)
            {
                CursorLeft = cursorLeft;
                CursorTop = cursorTop;
            }
          
            public int CursorLeft { get; private set; }

            public int CursorTop { get; private set; }

            public virtual void Update()
            {
                ConsoleColor tempColor = Console.ForegroundColor;
                Console.ForegroundColor = foregroundColor;

                Console.SetCursorPosition(CursorLeft, CursorTop);
                Console.Write(text);

                Console.ForegroundColor = tempColor;
            }
        }

        #endregion Private Classes
    }
}
