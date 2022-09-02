using System;
using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Interfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Database
{    
    public class DatabaseGeneric<T> where T : IClonable<T>, IIdContainer
    {
        public const int NotFound = -1;

        protected readonly List<T> Records = new List<T>();

        /// <summary>
        /// Вставить данные. Если найдена строка с таким же id, то данные в этой строке будут обновлены.
        /// </summary>
        /// <param name="record">Запись.</param>
        /// <returns>Количество измененных записей.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int Insert(T record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            string guid = Guid.TryParse(record.Id, out Guid parsedGuid) == false
                ? Guid.NewGuid().ToString("N")
                : record.Id;

            record.Id = guid;

            int indexToUpate = Records.FindIndex(element => element.Id == record.Id);

            if (indexToUpate == NotFound)
            {
                Records.Add(record);
            }
            else
            {
                Records[indexToUpate] = record;
            }

            return 1;
        }

        /// <summary>
        /// Удалить строку.
        /// </summary>
        /// <param name="recordId">ID записи.</param>
        /// <returns>Количество измененных записей.</returns>
        public int Delete(string recordId)
        {
            var recordToRemove = Records.Find(record => record.Id == recordId);

            if (recordToRemove != null)
            {
                Records.Remove(recordToRemove);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Обновить данные в строке.
        /// </summary>
        /// <param name="recordId">ID записи.</param>
        /// <returns>Количество измененных записей.</returns>
        public int Update(T record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            int indexToUpate = Records.FindIndex(element => element.Id == record.Id);

            if (indexToUpate != NotFound)
            {
                Records[indexToUpate] = record;
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Получить копию всех записей из БД.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<T> SelectAllRecords()
        {
            //Представим, что это датасет. Датасет по умолчанию создает копии записей из БД.
            var result = new List<T>();

            result = Records
                .Select(record => record.Clone())
                .ToList();

            return result;
        }
    }
}
