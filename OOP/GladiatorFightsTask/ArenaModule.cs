using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Controllers;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask
{
    /// <summary>
    /// ///Задача<br/>
    ///<br/>
    ///Создать 5 бойцов, пользователь выбирает 2 бойцов и они сражаются друг с другом до смерти. У каждого бойца могут быть свои статы.<br/>
    ///Каждый игрок должен иметь особую способность для атаки, которая свойственна только его классу!<br/>
    ///Если можно выбрать одинаковых бойцов, то это не должна быть одна и та же ссылка на одного бойца, чтобы он не атаковал сам себя.<br/>
    ///Пример, что может быть уникальное у бойцов. Кто-то каждый 3 удар наносит удвоенный урон, другой имеет 30% увернуться от полученного урона, кто-то при получении урона немного себя лечит. Будут новые поля у наследников. У кого-то может быть мана и это только его особенность. 
    /// </summary>
    class ArenaModule : IRunnable
    {
        ArenaModuleController _controller;

        #region IRunnable Implementation

        public void Run()
        {
            _controller.RunGameCycle();
        }

        #endregion IRunnable Implementation

        public ArenaModule(ArenaModuleController controller)
        {
            _controller = controller;
        }
    }
}
