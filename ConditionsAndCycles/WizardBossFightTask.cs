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
                    _player.ResetDemonAbility();
                }

                if (_player.Attacking)
                {
                    _player.ResetDemonAbility();
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
            _actionsStatusBar.ForegroundColor = ConsoleColor.Green;

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
                var ability = _player._abilities[abilityType];

                if (ability.Status == Player.AbilityStatus.Available)
                {
                    stringBuilder.AppendFormat("{0} {1, 2}, ", ability.Name, (int)abilityType);
                }
            }

            stringBuilder.AppendFormat("Пропустить ход {0}", (int) Player.AbilityType.SkipStep);

            _actionsStatusBar.Text = stringBuilder.ToString();

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
            public const int HealedHPValue = 250;

            private readonly Dictionary<AbilityType, Ability> _abilities = new Dictionary<AbilityType, Ability>();
            
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
                Text = "Player";
                ForegroundColor = ConsoleColor.Cyan;

                HealthBar.ForegroundColor = ConsoleColor.Green;
                DamageBar.ForegroundColor = ConsoleColor.Red;

                InitStartValues();
                InitAbilities();
            }

            public bool Attacking
            {
                get { return _abilities[AbilityType.DemonAttack].Status == Player.AbilityStatus.Active; }
            }

            public bool Invincible
            {
                get 
                { 
                    return _abilities[AbilityType.DimensionalRift].Status == AbilityStatus.Active; 
                }
                set
                {
                    if (value)
                    {
                        _abilities[AbilityType.DimensionalRift].Status = Player.AbilityStatus.Active;                        
                    }
                    else
                    {
                        _abilities[AbilityType.DimensionalRift].Status = Player.AbilityStatus.Available;
                    }
                }
            }

            public void ResetDemonAbility()
            {
               _abilities[Player.AbilityType.DemonCall].Status = Player.AbilityStatus.Available;
               _abilities[Player.AbilityType.DemonAttack].Status = Player.AbilityStatus.Unavailable;
            }

            public void PerformAction(AbilityType actionType)
            {
                switch (actionType)
                {
                    case AbilityType.SkipStep:
                        _abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                        _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        break;

                    case AbilityType.DemonCall:
                        if (_abilities[AbilityType.DemonCall].Status == AbilityStatus.Active)
                        {
                            _abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                            _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        }
                        else
                        {
                            _abilities[AbilityType.DemonCall].Status = AbilityStatus.Active;
                            _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Available;

                            health -= damagePerHit;
                        }
                        break;

                    case AbilityType.DemonAttack:
                        if (_abilities[AbilityType.DemonCall].Status == AbilityStatus.Active 
                            && _abilities[AbilityType.DemonAttack].Status == AbilityStatus.Available)
                        {
                            _abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                            _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Active;
                        }
                        break;

                    case AbilityType.DimensionalRift:
                        _abilities[AbilityType.DemonCall].Status = AbilityStatus.Unavailable;
                        _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
                        Invincible = true;
                        health += HealedHPValue;
                        if (health > MaxHealth)
                        {
                            health = MaxHealth;
                        }
                        break;
                }
            }

            public override void Update()
            {
                base.Update();

                foreach (var ability in _abilities.Values)
                {
                    ability._statusBar.Update();
                }
            }

            private void InitAbilities()
            {
                var verticalOffset = 4;

                var demonCall = new Ability();
                var demonAttack = new Ability();
                var dimensionalRift = new Ability();

                demonCall.Name = "Рашамон";
                demonCall.Info = "Призывает демона (Отнимает "+ damagePerHit + " хп игроку)";                
                demonCall._statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                demonCall._statusBar.text = demonCall.Name + ": " + demonCall.Info;
                demonCall.Status = AbilityStatus.Available;

                verticalOffset++;

                demonAttack.Name = "Хуганзакура";
                demonAttack.Info = "Демон наносит "+ damagePerHit + " ед. урона врагу и испаряется.";                
                demonAttack._statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                demonAttack._statusBar.text = demonAttack.Name + ": " + demonAttack.Info;
                demonAttack.Status = AbilityStatus.Unavailable;

                verticalOffset++;

                dimensionalRift.Name = "Разлом";
                dimensionalRift.Info = "Позволяет скрыться в разломе и восстановить "+ HealedHPValue + " хп.";
                dimensionalRift._statusBar = new ConsoleRecord(CursorLeft, CursorTop + verticalOffset);
                dimensionalRift._statusBar.text = dimensionalRift.Name + ": " + dimensionalRift.Info;
                dimensionalRift.Status = AbilityStatus.Available;

                _abilities.Add(AbilityType.DemonCall, demonCall);
                _abilities.Add(AbilityType.DemonAttack, demonAttack);
                _abilities.Add(AbilityType.DimensionalRift, dimensionalRift);
            }

            private void InitStartValues()
            {
                health = 600;
                damagePerHit = 100;
            }

            public class Ability
            {
                private readonly ConsoleRecord _statusBar;
                private AbilityStatus _status;

                public Ability(string name, string info, int cursorLeft, int cursorTop)
                {
                    Name = name;
                    Info = info;

                    _statusBar = new ConsoleRecord(cursorLeft, cursorTop);
                }

                public string Name { get; private set; }

                public string Info { get; private set; }

                public ConsoleRecord StatusBar 
                { 
                    get 
                    { 
                        return _statusBar; 
                    } 
                }

                public AbilityStatus Status
                {
                    get
                    {
                        return _status;
                    }
                    set
                    {
                        _status = value;

                        if (_statusBar != null)
                        {
                            switch (value)
                            {
                                case AbilityStatus.Unavailable:
                                    _statusBar.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case AbilityStatus.Available:
                                    _statusBar.ForegroundColor = ConsoleColor.White;
                                    break;
                                case AbilityStatus.Active:
                                    _statusBar.ForegroundColor = ConsoleColor.Green;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private class Boss
        {
            public int health = 1000;            
            public int damagePerHit = 200;

            protected ConsoleRecord HealthBar;
            protected ConsoleRecord DamageBar;
            protected ConsoleRecord NameBar;

            public Boss(int cursorLeft, int cursorTop)
            {
                int verticalOffset = 0;

                NameBar = new ConsoleRecord(cursorLeft, cursorTop + verticalOffset);
                verticalOffset++;

                HealthBar = new ConsoleRecord(cursorLeft, cursorTop+verticalOffset);
                verticalOffset++;

                DamageBar = new ConsoleRecord(cursorLeft, cursorTop+verticalOffset);

                NameBar.Text = "Boss";
                NameBar.ForegroundColor = ConsoleColor.DarkRed;
                HealthBar.ForegroundColor = ConsoleColor.Green;
                DamageBar.ForegroundColor = ConsoleColor.Red;
            }

            public bool Dead
            {
                get { return health <= 0; }
            }

            public virtual void Update()
            {
                HealthBar.Text = "Health: " + health;
                DamageBar.Text = "Damage: " + damagePerHit;

                NameBar.Update();
                HealthBar.Update();
                DamageBar.Update();
            }
        }

        private class ConsoleRecord
        {
            public ConsoleRecord(int cursorLeft, int cursorTop)
            {
                CursorLeft = cursorLeft;
                CursorTop = cursorTop;
            }

            public string Text { get; set; }
            
            public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
          
            public int CursorLeft { get; private set; }

            public int CursorTop { get; private set; }

            public virtual void Update()
            {
                ConsoleColor tempColor = Console.ForegroundColor;
                Console.ForegroundColor = ForegroundColor;

                Console.SetCursorPosition(CursorLeft, CursorTop);
                Console.Write(Text);

                Console.ForegroundColor = tempColor;
            }
        }

        #endregion Private Classes
    }
}
