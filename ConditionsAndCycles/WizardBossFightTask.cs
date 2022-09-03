using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Легенда: Вы – теневой маг(можете быть вообще хоть кем) и у вас в арсенале есть несколько заклинаний, которые вы можете использовать против Босса. Вы должны уничтожить босса и только после этого будет вам покой.<br/>
    ///<br/>
    ///Формально: перед вами босс, у которого есть определенное кол-во жизней и определенный ответный урон. У вас есть 4 заклинания для нанесения урона боссу. Программа завершается только после смерти босса или смерти пользователя.<br/>
    ///<br/>
    ///Пример заклинаний<br/>
    ///<br/>
    ///Рашамон – призывает теневого духа для нанесения атаки (Отнимает 100 хп игроку)<br/>
    ///<br/>
    ///Хуганзакура (Может быть выполнен только после призыва теневого духа), наносит 100 ед. урона<br/>
    ///<br/>
    ///Межпространственный разлом – позволяет скрыться в разломе и восстановить 250 хп. Урон босса по вам не проходит<br/>
    ///<br/>
    ///Заклинания должны иметь схожий характер и быть достаточно сложными, они должны иметь какие-то условия выполнения (пример - Хуганзакура). Босс должен иметь возможность убить пользователя.
    /// </summary>
    public class WizardBossFightTask : IRunnable
    {
        private ConsoleRecord _actionsStatusBar;
        
        private Player _player;
        private Boss _boss;
        private int _cursorX = 0;
        private int _cursorY = 0;

        private IEnumerable<AbilityType> _correctInputValues;

        #region Enums

        private enum AbilityStatus
        {
            Unavailable,
            Available,
            Active
        }

        private enum AbilityType
        {
            DemonCall = 1,
            DemonAttack,
            DimensionalRift,
            SkipStep
        }

        #endregion Enums

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();
            UpdateScene();
            
            while (_player.Dead == false && _boss.Dead == false)
            {
                _player.UpdateAbilitiesState();
                UpdateScene();

                var action = InputNextAction();
                _player.PerformAction(action);

                PerformExchangeOfAttacks();
                UpdateScene();
            }

            PrintEndgameInfo();

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private AbilityType InputNextAction()
        {
            var parsed = false;
            AbilityType actionType = AbilityType.SkipStep;

            while (parsed == false)
            {
                SaveCursorPosition();
                var input = Console.ReadKey();
                UpdateScene();
                RestoreCursorPosition();

                if (Enum.TryParse(input.KeyChar.ToString(), out actionType) && _correctInputValues.Contains(actionType))
                {
                    parsed = true;
                }
            }

            return actionType;
        }

        private void PerformExchangeOfAttacks()
        {
            if (_player.Attacking)
            {
                _boss.TakeDamage(_player.DamagePerHit);
            }
            //Проверки на смерть босса нет, т.к обмен ударами происходит одновременно
            if (_player.Invincible == false)
            {
                _player.TakeDamage(_boss.DamagePerHit);
            }
        }

        private void Initialize()
        {
            Console.Clear();

            _correctInputValues = Enum.GetValues(typeof(AbilityType))
                .Cast<AbilityType>();

            _boss = new Boss(100, 0);
            _player = new Player(0, 0);

            _actionsStatusBar = new ConsoleRecord(0, 9);
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

            foreach (var abilityType in _player.Abilities.Keys)
            {
                var ability = _player.Abilities[abilityType];

                if (ability.Status == AbilityStatus.Available)
                {
                    stringBuilder.AppendFormat("{0} {1, 2}, ", ability.Name, (int)abilityType);
                }
            }

            stringBuilder.AppendFormat("Пропустить ход {0}", (int) AbilityType.SkipStep);

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

        private void PrintEndgameInfo()
        {
            Console.WriteLine("\n\n\nБитва окончена!");

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
        }

        #region Private Classes

        #region ConsoleRecord

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

        #endregion ConsoleRecord


        #region Ability

        private interface IReadOnlyAbility
        {
            string Name { get; }

            string Info { get; }

            ConsoleRecord StatusBar { get; }

            AbilityStatus Status { get; }
        }

        private class Ability : IReadOnlyAbility
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

        #endregion Ability


        #region Boss

        private class Boss
        {
            protected int MaxHealth = 1000;
            protected ConsoleRecord NameBar;
            protected ConsoleRecord HealthBar;
            protected ConsoleRecord DamageBar;

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

            public int Health { get; protected set; } = 1000;

            public int DamagePerHit { get; protected set; } = 200;

            public bool Dead
            {
                get { return Health <= 0; }
            }

            public void TakeDamage(int damage)
            {
                if (damage < 0)
                {
                    return;
                }

                Health -= damage;
            }

            public void TakeHeal(int healValue)
            {
                Health += healValue;

                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }
            }

            public virtual void Update()
            {
                HealthBar.Text = "Health: " + Health;
                DamageBar.Text = "Damage: " + DamagePerHit;

                NameBar.Update();
                HealthBar.Update();
                DamageBar.Update();
            }
        }

        #endregion Boss


        #region Player

        private class Player : Boss
        {
            public const int HealedHPValue = 250;

            private readonly Dictionary<AbilityType, Ability> _abilities = new Dictionary<AbilityType, Ability>();

            public Player(int cursorLeft, int cursorTop) : base(cursorLeft, cursorTop)
            {
                NameBar.Text = "Player";
                NameBar.ForegroundColor = ConsoleColor.Cyan;

                HealthBar.ForegroundColor = ConsoleColor.Green;
                DamageBar.ForegroundColor = ConsoleColor.Red;

                InitStartValues();
                InitAbilities(cursorLeft, cursorTop);
                MaxHealth = 700;
            }

            public IReadOnlyDictionary<AbilityType, IReadOnlyAbility> Abilities
            {
                get
                {
                    return (IReadOnlyDictionary<AbilityType, IReadOnlyAbility>)_abilities;
                }
            }

            public bool Attacking
            {
                get { return _abilities[AbilityType.DemonAttack].Status == AbilityStatus.Active; }
            }

            public bool DemonAttackEnabled
            {
                get
                {
                    return _abilities[AbilityType.DemonCall].Status == AbilityStatus.Active
                        && _abilities[AbilityType.DemonAttack].Status == AbilityStatus.Available;
                }
            }

            public bool Invincible
            {
                get
                {
                    return _abilities[AbilityType.DimensionalRift].Status == AbilityStatus.Active;
                }
                private set
                {
                    if (value)
                    {
                        _abilities[AbilityType.DimensionalRift].Status = AbilityStatus.Active;
                    }
                    else
                    {
                        _abilities[AbilityType.DimensionalRift].Status = AbilityStatus.Available;
                    }
                }
            }

            public void UpdateAbilitiesState()
            {
                if (Invincible)
                {
                    Invincible = false;
                }

                if (Attacking)
                {
                    ResetDemonAbility();
                }
            }

            public void ResetDemonAbility()
            {
                _abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;
            }

            public void PerformAction(AbilityType actionType)
            {
                switch (actionType)
                {
                    case AbilityType.SkipStep:
                        ResetDemonAbility();
                        break;

                    case AbilityType.DemonCall:

                        if (_abilities[AbilityType.DemonCall].Status == AbilityStatus.Active)
                        {
                            ResetDemonAbility();
                        }
                        else
                        {
                            _abilities[AbilityType.DemonCall].Status = AbilityStatus.Active;
                            _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Available;
                            
                            TakeDamage(DamagePerHit);
                        }
                        break;

                    case AbilityType.DemonAttack:

                        if (DemonAttackEnabled)
                        {
                            _abilities[AbilityType.DemonCall].Status = AbilityStatus.Available;
                            _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Active;
                        }
                        break;

                    case AbilityType.DimensionalRift:
                        _abilities[AbilityType.DemonCall].Status = AbilityStatus.Unavailable;
                        _abilities[AbilityType.DemonAttack].Status = AbilityStatus.Unavailable;

                        Invincible = true;
                        TakeHeal(HealedHPValue);
                        break;
                }
            }

            public override void Update()
            {
                base.Update();

                foreach (var ability in _abilities.Values)
                {
                    ability.StatusBar.Update();
                }
            }

            private void InitAbilities(int cursorLeft, int cursorTop)
            {
                var verticalOffset = 4;

                var demonCall = new Ability(
                    "Рашамон",
                    "Призывает демона (Отнимает " + DamagePerHit + " хп игроку)",
                    cursorLeft,
                    cursorTop + verticalOffset
                    );
                demonCall.StatusBar.Text = demonCall.Name + ": " + demonCall.Info;
                demonCall.Status = AbilityStatus.Available;

                verticalOffset++;

                var demonAttack = new Ability(
                    "Хуганзакура",
                    "Демон наносит " + DamagePerHit + " ед. урона врагу и испаряется.",
                    cursorLeft,
                    cursorTop + verticalOffset
                    );
                demonAttack.StatusBar.Text = demonAttack.Name + ": " + demonAttack.Info;
                demonAttack.Status = AbilityStatus.Unavailable;

                verticalOffset++;

                var dimensionalRift = new Ability(
                    "Разлом",
                    "Позволяет скрыться в разломе и восстановить " + HealedHPValue + " хп.",
                    cursorLeft,
                    cursorTop + verticalOffset
                    );
                dimensionalRift.StatusBar.Text = dimensionalRift.Name + ": " + dimensionalRift.Info;
                dimensionalRift.Status = AbilityStatus.Available;

                _abilities.Add(AbilityType.DemonCall, demonCall);
                _abilities.Add(AbilityType.DemonAttack, demonAttack);
                _abilities.Add(AbilityType.DimensionalRift, dimensionalRift);
            }

            private void InitStartValues()
            {
                Health = 600;
                DamagePerHit = 100;
            }
        }

        #endregion Player


        #endregion Private Classes
    }
}
