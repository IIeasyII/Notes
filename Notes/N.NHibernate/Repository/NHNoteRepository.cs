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

        public IList<Note> GetAllListNotesPublic(long userId)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var notes = session.QueryOver<Note>()
                .And(u => u.User.Id != userId)
                .And(u => u.Flag == true)
                .List();

            NHibernateHelper.CloseSession();

            return notes;
        }

        public IList<Note> GetAllListMyNotes(long userId)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var notes = session.QueryOver<Note>()
                .And(u => u.User.Id == userId)
                .List();

            NHibernateHelper.CloseSession();

            return notes;
        }

        public void Save(Note entity)
        {
            throw new NotImplementedException();
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
