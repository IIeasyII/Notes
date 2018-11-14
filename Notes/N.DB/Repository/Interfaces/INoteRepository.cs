using N.DB.Models;
using System.Collections.Generic;

namespace N.DB.Repository.Interfaces
{
    public interface INoteRepository : IEntityRepository<Note>
    {
        /// <summary>
        /// Find public notes without current user
        /// </summary>
        /// <returns>List notes</returns>
        IList<Note> GetAllListNotesPublic(long userId);

        /// <summary>
        /// Get my notes by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List notes</returns>
        IList<Note> GetAllListMyNotes(long userId);

        /// <summary>
        /// Get note by id
        /// </summary>
        /// <param name="Id">Note id</param>
        /// <returns>Note object</returns>
        Note GetNoteById(long Id);
    }
}