using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    ///Вы задаете вопросы пользователю, по типу "как вас зовут", "какой ваш знак зодиака" и тд, после чего, по данным, которые он ввел, формируете небольшой текст о пользователе.<br/>
    ///<br/>
    ///"Вас зовут Алексей, вам 21 год, вы водолей и работаете на заводе." 
    /// </summary>
    class SurveyTask : IRunnable
    {
        public void Run()
        {
            const string QuestionName = "Как вас зовут?";
            const string QuestionOld = "Сколько вам лет?";
            const string QuestionZodaik = "Какой ваш знак зодиака?";
            const string QuestionWork = "Где вы работаете?";

            string name = string.Empty;
            string old = string.Empty;
            string work = string.Empty;
            string zodiak = string.Empty;

            name = GetAnswer(QuestionName);
            old = GetAnswer(QuestionOld);
            work= GetAnswer(QuestionWork);
            zodiak = GetAnswer(QuestionZodaik);

            string formattedInfo = $"Вас зовут {name}, вам {old} год, вы {zodiak} и работаете на {work}.";

            Console.WriteLine(formattedInfo);
        }

        private string GetAnswer(string question)
        {
            string answer = string.Empty;
            while (string.IsNullOrEmpty(answer))
            {
                Console.WriteLine(question);
                answer = Console.ReadLine();

                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("Ответ слишком короткий. Попробуйте еще раз.");
                }
            }

            return answer;
        }
    }
}
