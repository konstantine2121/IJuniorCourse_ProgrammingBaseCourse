using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private EnclosureInfoPrinter _printer;

        private enum GenderType
        {
            Male,
            Female
        }

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();


        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            _zoo = new ZooCreator().Create();
            _printer = new EnclosureInfoPrinter();
        }

        #region Private Classes

        private class EnclosureInfoPrinter
        {
            public void Print(Enclosure enclosure, ConsoleTable table)
            {

            }
        }

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
                    "Лев — вид хищных млекопитающих.\n" +
                    "Наряду с тигром — самая крупная из ныне живущих кошек.", 
                    ""));
                _container.Add(typeof(Macaque), new SpeciesDescription(
                    "Макаки — приматы средней величины с крепким туловищем и сильными конечностями.", 
                    ""));
                _container.Add(typeof(Elephant), new SpeciesDescription(
                    "Слоны — самые крупные наземные животные на Земле.",
                    ""));
                _container.Add(typeof(WhiteOwl), new SpeciesDescription(
                    "Белая сова — самая крупная птица из отряда совообразных в тундре.", 
                    ""));
            }

            public bool Find(Type type, out SpeciesDescription description)
            {
                return _container.TryGetValue(type, out description);
            }
        }

        private class SpeciesDescription
        {
            public SpeciesDescription(string description, string sound)
            {
                Description = description;
                Sound = sound;
            }

            public string Description { get; private set; }
            
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

                var constructor = type.GetConstructors()
                    .First(info =>
                    {
                        var parameters = info.GetParameters();
                        
                        return parameters.Length == 1 
                        && parameters[0].ParameterType.Equals(gender);
                    });

                var animal = constructor.Invoke(new object[] { gender});

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
