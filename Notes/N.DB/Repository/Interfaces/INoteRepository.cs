using N.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.DB.Repository.Interfaces
{
    public interface INoteRepository : IEntityRepository<Note>
    {
        /// <summary>
        /// Найти пользователя по логину
        /// </summary>
        /// <returns>Пользователя</returns>
        IList<Note> GetAllListNotesPublic(long userId);

        IList<Note> GetAllListMyNotes(long userId);

        Note GetNoteById(long Id);
    }
}
