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

        //public void DeleteNoteById()

        public Note GetNoteById(long Id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var note = session.QueryOver<Note>()
                .And(u => u.Id == Id)
                .SingleOrDefault();

            NHibernateHelper.CloseSession();
            
            return note;
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
            var session = NHibernateHelper.GetCurrentSession();
            
            if (entity.Id > 0)
            {
                session.CreateSQLQuery("UPDATE [Note] SET [Name] = :Name, [Content] = :Content, [Flag] = :Flag, [TagList] = :TagList, [Date] = :Date, [UserId] = :UserId, [File] = :File WHERE [Id] = :Id")
                    .SetInt64("Id", entity.Id)
                    .SetString("Name", entity.Name)
                    .SetString("Content", entity.Content)
                    .SetBoolean("Flag", false)
                    .SetString("TagList", entity.TagList)
                    .SetDateTime("Date", DateTime.Now)
                    .SetInt64("UserId", entity.User.Id)
                    .SetString("File", entity.File)
                    .ExecuteUpdate();
            }
            else
            {
                session.CreateSQLQuery("INSERT INTO [Note] ([Name], [Content], [Flag], [TagList], [Date], [UserId], [File]) VALUES (:Name, :Content, :Flag, :TagList, :Date, :UserId, :File)")
                    .SetString("Name", entity.Name)
                    .SetString("Content", entity.Content)
                    .SetBoolean("Flag", false)
                    .SetString("TagList", entity.TagList)
                    .SetDateTime("Date", DateTime.Now)
                    .SetInt64("UserId", entity.User.Id)
                    .SetString("File", entity.File)
                    .ExecuteUpdate();
            }

            NHibernateHelper.CloseSession();
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
