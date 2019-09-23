using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly AppDbContext _context;

        public CollectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Collection Add(Collection collection)
        {                       

            _context.Collections.Add(collection);
            _context.SaveChanges();

            _context.Collections.Add(collection);
            _context.SaveChanges();
            return collection;
        }



        //public Collection Delete(int id)
        //{
           
        //}

        public IEnumerable<Collection> GetAllCollections()
        {
            return _context.Collections;
        }

        public Collection GetCollection(int Id)
        {
           return _context.Collections.Find(Id);
        }

        public Collection Update(Collection collectionChanges)
        {
            var collection = _context.Collections.Attach(collectionChanges);
            collection.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return collectionChanges;
        }
    }
}
