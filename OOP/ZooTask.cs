using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    /// ///Задача<br/>
    ///<br/>
    ///Пользователь запускает приложение и перед ним находится меню, <br/>
    ///в котором он может выбрать, к какому вольеру подойти. <br/>
    ///При приближении к вольеру, пользователю выводится информация о том: <br/>
    ///что это за вольер, <br/>
    ///сколько животных там обитает, <br/>
    ///их пол <br/>
    ///и какой звук издает животное.<br/>
    ///Вольеров в зоопарке может быть много, <br/>
    ///в решении нужно создать минимум 4 вольера.
    /// </summary>
    public class ZooTask : IRunnable
    {
        private Zoo _zoo;        
        private Dictionary<int, Enclosure> _enclosuresDictionary;
        private int _enclosureIndex;
        private VisitorState _state;
        private SoundPlayer _player;

        private enum GenderType
        {
            Male,
            Female
        }

        private enum VisitorState
        {
            LookingAtTheMainMenu,
            LookingAtTheEnclosure
        }

        private enum EnclosureStateActions
        {
            ReturnToMenu = 1,
            PlaySound
        }

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            var visitorTired = false;

            while(visitorTired == false)
            {
                if (_state == VisitorState.LookingAtTheMainMenu)
                {
                    PerformMenuState();
                }

                if (_state == VisitorState.LookingAtTheEnclosure)
                {
                    PerformNearEnclosureState();
                }
            }
        }

        #endregion IRunnable Implementation

        private void PerformMenuState()
        {
            const string CageFormat = "{0,6}  {1}";
            Console.Clear();
            ConsoleOutputMethods.Info("Добро пожаловать в зоопарк!");
            Console.WriteLine(CageFormat, "Клетка", "Описание.");
            foreach (var pair in _enclosuresDictionary)
            {
                Console.WriteLine(CageFormat, pair.Key, pair.Value.Description.Content);
            }

            Console.WriteLine();

            bool correctIndex = false;

            int index = 0;

            while(correctIndex == false)
            {
                index = ConsoleInputMethods.ReadPositiveInteger("Введите номер клетки: ");

                if (_enclosuresDictionary.ContainsKey(index))
                {
                    correctIndex = true;
                    _enclosureIndex = index;
                }
                else
                {
                    ConsoleOutputMethods.Warning("Такой клетки в зоопарке нет.");
                }
            }

            _state = VisitorState.LookingAtTheEnclosure;
        }

        private void PerformNearEnclosureState()
        {            
            Console.Clear();

            var enclosure = _enclosuresDictionary[_enclosureIndex];
            var numberOfMale = enclosure.Animals.Sum(animal => animal.Gender == GenderType.Male ? 1 : 0);
            var numberOfFemale = enclosure.Animals.Count - numberOfMale;

            ConsoleOutputMethods.Info("Описание клетки.");
            Console.WriteLine(enclosure.Description.Content);
            Console.WriteLine($"Всего особей: {enclosure.Animals.Count}\t\tСамцов: {numberOfMale}\tСамок: {numberOfFemale}");
            Console.WriteLine();

            var commandInfo =$"{(int)EnclosureStateActions.ReturnToMenu} - вернуться в меню, {(int)EnclosureStateActions.PlaySound} - послушать звуки животных:";

            var returnToMenu = false;

            while(returnToMenu == false)
            {
                var action = ConsoleInputMethods.ReadPositiveInteger(commandInfo);
                switch((EnclosureStateActions)action)
                {
                    case EnclosureStateActions.ReturnToMenu:
                        returnToMenu = true;
                        break;
                    case EnclosureStateActions.PlaySound:
                        _player.Play(enclosure.Description.Sound);
                        break;

                    default:
                        ConsoleOutputMethods.Warning("Такой команды нет в списке.");
                        break;
                }
            }

            _state = VisitorState.LookingAtTheMainMenu;
        }

        private void Initialize()
        {
            _state = VisitorState.LookingAtTheMainMenu;
            _zoo = new ZooCreator().Create();            

            int cageIndex = 1;
            _enclosuresDictionary = new Dictionary<int, Enclosure>();

            foreach(var cage in _zoo.Enclosures)
            {
                _enclosuresDictionary.Add(cageIndex, cage);
                cageIndex++;
            }

            _enclosureIndex = _enclosuresDictionary.Keys.First();
            _player = new SoundPlayer();
        }

        #region Private Classes

        private class Zoo
        {
            private readonly List<Enclosure> _enclosures;

            public Zoo(IEnumerable<Enclosure> enclosures)
            {
                _enclosures = new List<Enclosure>();
                _enclosures.AddRange(enclosures);
            }

            public IReadOnlyList<Enclosure> Enclosures => _enclosures;
        }

        private class Enclosure
        {

            private readonly List<Animal> _animals;

            public Enclosure(IEnumerable<Animal> animals, SpeciesDescription speciesDescription)
            {
                _animals = new List<Animal>();
                _animals.AddRange(animals);

                Description = speciesDescription;
            }

            public SpeciesDescription Description { get; private set; }

            public IReadOnlyList<Animal> Animals => _animals;
        }

        private class SoundPlayer
        {
            private readonly MediaPlayer _player = new MediaPlayer();
            private const string Folder = "sounds";

            public void Play(string fileName)
            {
                _player.Stop();

                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), Folder, fileName);

                if (File.Exists(fullFilePath))
                {
                    _player.Open(new Uri(fullFilePath));
                    _player.Play();
                }
            }

            public void Stop()
            {
                _player.Stop();
            }
        }

        private class SpeciesDescriptionContainer
        {
            private readonly Dictionary<Type, SpeciesDescription> _container;

            public SpeciesDescriptionContainer()
            {
                _container = new Dictionary<Type, SpeciesDescription>();
                Initialize();
            }

            void Initialize()
            {
                _container.Add(typeof(Lion), new SpeciesDescription(
                    "Лев — вид хищных млекопитающих. Одна из самых крупная из ныне живущих кошек.",
                    "lion.mp3"));
                _container.Add(typeof(Macaque), new SpeciesDescription(
                    "Макаки — приматы средней величины с крепким туловищем и сильными конечностями.",
                    "macaque.mp3"));
                _container.Add(typeof(Elephant), new SpeciesDescription(
                    "Слоны — самые крупные наземные животные на Земле.",
                    "elephant.mp3"));
                _container.Add(typeof(WhiteOwl), new SpeciesDescription(
                    "Белая сова — самая крупная птица из отряда совообразных в тундре.",
                    "owl.mp3"));
            }

            public bool Find(Type type, out SpeciesDescription description)
            {
                return _container.TryGetValue(type, out description);
            }
        }

        private class SpeciesDescription
        {
            public SpeciesDescription(string content, string sound)
            {
                Content = content;
                Sound = sound;
            }

            public string Content { get; private set; }
            
            public string Sound { get; private set; }
        }

        #region Creators

        private interface ICreator<T>
        {
            T Create();
        }

        private class RandomСontainer
        {
            protected readonly Random Rand = new Random();
        }

        private class ZooCreator : RandomСontainer, ICreator<Zoo>
        {
            private readonly SpeciesDescriptionContainer _descriptionContainer = new SpeciesDescriptionContainer();

            public Zoo Create()
            {
                var lionCageCreator = new EnclosureCreator<Lion>(_descriptionContainer);
                var macaqueCageCreator = new EnclosureCreator<Macaque>(_descriptionContainer);
                var elephantCageCreator = new EnclosureCreator<Elephant>(_descriptionContainer);
                var whiteOwlCageCreator = new EnclosureCreator<WhiteOwl>(_descriptionContainer);

                var cages = new List<Enclosure>();

                cages.Add(lionCageCreator.Create());
                cages.Add(macaqueCageCreator.Create());
                cages.Add(elephantCageCreator.Create());
                cages.Add(whiteOwlCageCreator.Create());

                return new Zoo(cages);
            }
        }

        private class EnclosureCreator<T> : RandomСontainer, ICreator<Enclosure>
            where T : Animal
        {
            private const int MinNumberOfAnimals = 1;
            private const int MaxNumberOfAnimals = 21;

            private readonly AnimalCreator<T> _creator = new AnimalCreator<T>();
            private readonly SpeciesDescriptionContainer _descriptionContainer;

            public EnclosureCreator(SpeciesDescriptionContainer descriptionContainer)
            {
                _descriptionContainer = descriptionContainer;
            }

            public Enclosure Create()
            {
                var animals = new List<T>();

                int numberOfAnimals = Rand.Next(MinNumberOfAnimals, MaxNumberOfAnimals);
                for(int i =0; i< numberOfAnimals; i++)
                {
                    animals.Add(_creator.Create());
                }

                _descriptionContainer.Find(typeof(T), out SpeciesDescription description);

                return new Enclosure(animals, description);
            }
        }

        private class AnimalCreator<T> : RandomСontainer, ICreator<T>
            where T : Animal
        {
            public T Create()
            {
                var genderTypes = Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToArray();
                var gender = genderTypes[Rand.Next(genderTypes.Length)];

                var type = typeof(T);

                var constructors = type.GetConstructors();

                var constructor = constructors.First(info =>
                {
                    var parameters = info.GetParameters();

                    return parameters.Length == 1
                    && parameters[0].ParameterType.Equals(gender.GetType());
                });

                var animal = constructor.Invoke(new object[] { gender });

                return animal as T;
            }
        }

        #endregion Creators

        #region Animals

        private class Animal
        {
            public GenderType Gender { get; private set; }

            public Animal(GenderType gender)
            {
                Gender = gender;
            }
        }

        private class Lion : Animal
        {
            public Lion(GenderType gender) : base(gender)
            {

            }
        }

        private class Macaque : Animal
        {
            public Macaque(GenderType gender) : base(gender)
            {

            }
        }

        private class Elephant : Animal
        {
            public Elephant(GenderType gender) : base(gender)
            {

            }
        }

        private class WhiteOwl : Animal
        {
            public WhiteOwl(GenderType gender) : base(gender)
            {

            }
        }

        #endregion Animals

        #endregion Private Classes
    }
}
