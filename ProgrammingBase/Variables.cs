using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    /// Попрактикуйтесь в создании переменных, объявить 10 переменных разных типов.<br/>
    /// Напоминание: переменные именуются с маленькой буквы, если название состоит из нескольких слов, то комбинируем их следующим образом - названиеПеременной.<br/>
    /// Также имя всегда должно отражать суть того, что хранит переменная.
    /// </summary>
    class Variables : IRunnable
    {
        public void Run()
        {
            string playerName = "Tom";
            int jumpCounter = 0;
            uint hitpoints = 100;
            byte rageLevel = 0;
            short itemsCount = 0;
            bool isAlive = true;
            long totalScore = 0;
            float maxSpeed = 45f;
            double currentSpeed = 0;
            decimal money = 100;            
        }
    }
}
