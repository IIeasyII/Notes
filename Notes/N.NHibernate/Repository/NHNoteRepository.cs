using N.DB.Models;
using N.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.NHibernate.Repository
{
    public class NHNoteRepository : NHBaseRepository<User>, INoteRepository
    {
        IUserRepository UserRepository;

        public NHNoteRepository()
        {
            UserRepository = new NHUserRepository();
        }

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
