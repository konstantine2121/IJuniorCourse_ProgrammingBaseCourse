﻿using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    /// На экране, в специальной зоне, выводятся картинки, по 3 в ряд. Всего у пользователя в альбоме 52 картинки. Код должен вывести, сколько полностью заполненных рядов можно будет вывести, и сколько картинок будет сверх меры.<br/>
    ///<br/>
    ///В качестве решения ожидаются объявленные переменные с необходимыми значениями и, основываясь на значениях переменных, вывод необходимых данных. 
    /// </summary>
    class ImagesInRow : IRunnable
    {
        public void Run()
        {
            const int ImagesInRow = 3;
            const int TotalImages = 52;

            int fullFilledRows = TotalImages / ImagesInRow;
            int restImages = TotalImages % ImagesInRow;

            Console.WriteLine($"Возможное количество картинок в одной строке: {ImagesInRow}.");
            Console.WriteLine($"Всего картинок: {TotalImages}.");
            Console.WriteLine();
            Console.WriteLine($"Полностью заполненых строк с картинками: {fullFilledRows}.");
            Console.WriteLine($"Картинок в последней строке: {restImages}");
        }

    }
}
