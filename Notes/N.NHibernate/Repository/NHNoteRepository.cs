using N.DB.Models;
using N.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace N.NHibernate.Repository
{
    public class NHNoteRepository : NHBaseRepository<User>, INoteRepository
    {
        IUserRepository UserRepository;

        public NHNoteRepository()
        {
            UserRepository = new NHUserRepository();
        }

        /// <summary>
        /// Get note by id
        /// </summary>
        /// <param name="Id">Note id</param>
        /// <returns>Note object</returns>
        public Note GetNoteById(long Id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                using (session.BeginTransaction())
                {
                    var note = session.QueryOver<Note>()
                        .And(u => u.Id == Id)
                        .SingleOrDefault();

                    return note;
                }
            }
        }

        /// <summary>
        /// Get list public notes without my notes
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List notes</returns>
        public IList<Note> GetAllListNotesPublic(long userId)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var notes = session.QueryOver<Note>()
                .And(u => u.User.Id != userId)
                .And(u => u.Flag == true)
                .List();

                return notes;
            }
        }

        /// <summary>
        /// Get my notes by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List notes</returns>
        public IList<Note> GetAllListMyNotes(long userId)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var notes = session.QueryOver<Note>()
                .And(u => u.User.Id == userId)
                .List();

                return notes;
            }
        }

        /// <summary>
        /// Save or update note
        /// </summary>
        /// <param name="entity">Note model</param>
        public void Save(Note entity)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var user = UserRepository.Load(entity.User.Id);

                entity.User = user;
                entity.Date = DateTime.Now;

                if (entity != null)
                {
                    session.SaveOrUpdate(entity);
                }

                session.Flush();
            }
        }

        /// <summary>
        /// Delete note by id
        /// </summary>
        /// <param name="id">Note id</param>
        public override void Delete(long id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            using (session.BeginTransaction())
            {
                var entity = GetNoteById(id);

                if (entity != null)
                {
                    session.Delete(entity);
                }

                session.Flush();
            }
        }

        Note IEntityRepository<Note>.Create()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Note> IEntityRepository<Note>.GetAll()
        {
            throw new NotImplementedException();
        }

        Note IEntityRepository<Note>.Load(long id)
        {
            throw new NotImplementedException();
        }
    }
}
